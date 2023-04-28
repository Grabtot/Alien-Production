using System.Collections;
using DanPie.Framework.AudioManagement;
using UnityEngine;

namespace AlienProduction.Tests
{
    public class MissTest : MonoBehaviour
    {
        [SerializeField] private AudioClipDataProvider _shootSoundProvider;
        [SerializeField] private AudioSourcesManager _sourceProvider;
        [SerializeField] private MissManager _missManager;

        protected void Awake()
        {
            _missManager.NoMoreMisses += () => Debug.Log("NO MORE MISSES!!!");
        }

        protected void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && _missManager.IsHaveAvailableMisses())
            {
                _sourceProvider.GetAudioSourceController()
                    .Play(_shootSoundProvider.GetClipData());
                _missManager.RegisterMiss();
            }
        }
        
    }
}
