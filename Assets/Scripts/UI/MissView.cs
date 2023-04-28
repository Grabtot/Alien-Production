using UnityEngine;
using UnityEngine.UI;

namespace AlienProduction.UI
{
    public class MissView : MonoBehaviour
    {
        [SerializeField] private Sprite _availableMissTexture;
        [SerializeField] private Sprite _unavailableMissTexture;

        [SerializeField] private Image _missImage;

        public void SetAvailableStyle()
        {
            _missImage.sprite = _availableMissTexture;
        }

        public void SetUnavailableStyle()
        {
            _missImage.sprite = _unavailableMissTexture;
        }
    }
}
