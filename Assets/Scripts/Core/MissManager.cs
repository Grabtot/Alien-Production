using System;
using AlienProduction.UI;
using UnityEngine;

public class MissManager : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _maxMissesNumber = 6;
    [SerializeField] private MissViewsContainer _missViewsContainer;

    public event Action NoMoreMisses;

    private int _currentMissesCount = 0;

    public void RegisterMiss()
    {
        _currentMissesCount++;

        if (_currentMissesCount - 1 < _maxMissesNumber)
        {
            _missViewsContainer.ApplyUnavaliableMissesNumber(_currentMissesCount);
        }

        if (_currentMissesCount == _maxMissesNumber)
        {
            NoMoreMisses?.Invoke();
        }
    }

    public bool IsHaveAvailableMisses()
        => _currentMissesCount < _maxMissesNumber;

    protected void Awake()
    {
        _missViewsContainer.Initialize(_maxMissesNumber);
    }
}
