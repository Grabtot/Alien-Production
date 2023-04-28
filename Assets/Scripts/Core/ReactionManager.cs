using System;
using DanPie.Framework.AudioManagement;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlienProduction.Core
{
    public class ReactionManager : MonoBehaviour
    {
        [Serializable]
        public struct ReactionData
        {
            public Color Color;
            public AudioClipDataProvider SoundProvider;
        }

        [SerializeField] private Image _reactionView;
        [SerializeField] private float _reactionEffectSpeed;
        [SerializeField] private ReactionData _positiveReactionData;
        [SerializeField] private ReactionData _negativeReactionData;
        [SerializeField] private AudioSourcesManager _audioSourcesManager;

        private Color _startColor;

        public void Awake()
        {
            _startColor = _reactionView.color;
        }

        public void ReactPositive()
        {
            React(_positiveReactionData);
        }

        public void ReactNegative()
        {
            React(_negativeReactionData);
        }


        private void React(ReactionData reactionData)
        {
            _reactionView.color = _startColor;

            _audioSourcesManager.GetAudioSourceController()
                .Play(reactionData.SoundProvider.GetClipData());

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_reactionView.DOColor(reactionData.Color, _reactionEffectSpeed / 4));
            sequence.AppendInterval(_reactionEffectSpeed / 4);
            sequence.Append(_reactionView.DOColor(_startColor, _reactionEffectSpeed / 2));
            sequence.Play();
            sequence.OnComplete(() => _reactionView.color = _startColor);
        }
    }
}
