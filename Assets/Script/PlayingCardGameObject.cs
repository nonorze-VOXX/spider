using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class PlayingCardGameObject : MonoBehaviour
    {
        [FormerlySerializedAs("cardSprite")] public PlayingCardSprite playingCardSprite;
        private SpriteRenderer _numberSprite;
        private PlayingCard _playingCard;
        private SpriteRenderer _shapeSprite;

        private void Awake()
        {
            _shapeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _numberSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
        }

        private void ChangeCardOutlook(PlayingCard pc)
        {
            //todo change picture
            print(pc);
            _numberSprite.sprite = playingCardSprite.numbers[pc.GetNumber()];
            _shapeSprite.sprite = playingCardSprite.shapes[(int)pc.GetShape()];
        }

        public PlayingCardGameObject SetPlayingCard(PlayingCard pc)
        {
            print(pc);
            _playingCard = pc;
            ChangeCardOutlook(pc);
            return this;
        }
    }
}