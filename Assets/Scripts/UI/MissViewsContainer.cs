using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlienProduction.UI
{
    public class MissViewsContainer : MonoBehaviour
    {
        [SerializeField] private MissView _missViewPrefab;

        private List<MissView> _missViews = new List<MissView>();

        public void Initialize(int missViewCount)
        {
            foreach (var missView in _missViews)
            {
                Destroy(missView.gameObject);
            }

            _missViews.Clear();

            for (int i = 0; i < missViewCount; i++)
            {
                _missViews.Add(Instantiate(_missViewPrefab, transform));
                _missViews[i].SetAvailableStyle();
            }
        }

        public void ApplyUnavaliableMissesNumber(int unavaliableMissesNumber)
        {
            if (_missViews.Count < unavaliableMissesNumber)
            {
                throw new ArgumentException($"The container does not " +
                    $"have that many {nameof(MissView)} elements, " +
                    $"you can create the required number using the {nameof(Initialize)} method!");
            }

            for (int i = 0; i < unavaliableMissesNumber; i++)
            {
                _missViews[i].SetUnavailableStyle();
            }
        }
    }
}
