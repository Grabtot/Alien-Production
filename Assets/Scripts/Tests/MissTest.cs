using DanPie.Framework.AudioManagement;
using UnityEngine;
using UnityEngine.Events;

namespace AlienProduction.Tests
{
    public class MissTest : MonoBehaviour
    {
        [SerializeField] private AudioClipDataProvider _shootSoundProvider;
        [SerializeField] private AudioSourcesManager _sourceProvider;

        public UnityEvent Missed;

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _sourceProvider.GetAudioSourceController()
                    .Play(_shootSoundProvider.GetClipData());

                Missed?.Invoke();
            }
        }
    }
}
