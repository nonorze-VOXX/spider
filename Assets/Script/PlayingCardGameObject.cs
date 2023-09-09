using UnityEngine;

namespace Script
{
    public class PlayingCardGameObject : MonoBehaviour
    {
        public PlayingCardSprite playingCardSprite;
        private CardStack _cardStack;
        private PlayingCardGameObject _head;
        private PlayingCardGameObject _next;
        private SpriteRenderer _numberSprite;
        private PlayingCard _playingCard;
        private SpriteRenderer _shapeSprite;
        private CardStack targetStack;

        private void Awake()
        {
            targetStack = null;
            _cardStack = null;
            _shapeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _numberSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
            _next = null;
        }

        private void Start()
        {
        }

        private void OnMouseDrag()
        {
            var cardPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cardPosition.z = transform.position.z;
            transform.position = cardPosition;
            var now = this;
            while (now.GetNext() != null)
            {
                now = now.GetNext();
                cardPosition += Vector3.down;
                now.transform.position = cardPosition;
            }
        }


        // private CardStack GetStack()
        // {
        //     return _cardStack;
        // }


        private void ChangeCardOutlook(PlayingCard pc)
        {
            //todo change picture
            _numberSprite.sprite = playingCardSprite.numbers[pc.GetNumber()];
            _shapeSprite.sprite = playingCardSprite.shapes[(int)pc.GetShape()];
        }

        public PlayingCardGameObject SetPlayingCard(PlayingCard pc)
        {
            _playingCard = pc;
            ChangeCardOutlook(pc);
            return this;
        }

        public void SetNext(PlayingCardGameObject next)
        {
            _next = next;
        }

        public PlayingCardGameObject GetNext()
        {
            return _next;
        }

        public PlayingCardGameObject SetHead(PlayingCardGameObject head)
        {
            if (head == null)
                head = this;
            else
                _head = head;

            return this;
        }

        public PlayingCardGameObject GetHead()
        {
            return _head;
        }
    }
}