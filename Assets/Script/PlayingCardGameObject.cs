using System.Collections.Generic;
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
        private CardStack stack;
        private List<Collider2D> touchingObjects;

        private void Awake()
        {
            touchingObjects = new List<Collider2D>();
            stack = null;
            _cardStack = null;
            _shapeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _numberSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
            _next = null;
        }

        private void OnMouseDown()
        {
            var now = _head;
            if (now == null)
            {
                Debug.Log("null hean");
                return;
            }

            if (now == this)
            {
                stack.SetHead(null);
                SetHead(null);
                return;
            }

            while (!(now.GetNext() == null || now.GetNext() == this)) now = now.GetNext();

            now.SetNext(null);
            SetHead(null);
        }

        private void OnMouseDrag()
        {
            var cardPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cardPosition.z = transform.position.z;
            SetBunchCardPosition(this, cardPosition);
        }

        private void OnMouseUp()
        {
            PlayingCardGameObject now;
            if (touchingObjects.Count != 0)
                now = touchingObjects[^1].GetComponent<PlayingCardGameObject>();
            else
                now = stack.GetHead();

            if (now == null)
            {
                var position = GetStack().transform.position;
                GetStack().SetHead(this);
                SetHead(this);
                SetBunchCardPosition(this, position);
            }
            else
            {
                while (now.GetNext() != null) now = now.GetNext();
                now.SetNext(this);
                SetHead(now.GetHead());
                SetBunchCardPosition(now);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            var head = other.GetComponent<PlayingCardGameObject>().GetHead();
            if (!touchingObjects.Contains(other) && head != GetHead())
                touchingObjects.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!touchingObjects.Contains(other)) return;
            touchingObjects.Remove(other);
        }

        private CardStack GetStack()
        {
            return stack;
        }

        private void SetBunchCardPosition(PlayingCardGameObject head, Vector3 headPosition)
        {
            head.transform.position = headPosition;

            SetBunchCardPosition(head);
        }

        private void SetBunchCardPosition(PlayingCardGameObject head)
        {
            Vector3 position;
            var now = head;
            while (now.GetNext() != null)
            {
                position = now.GetNextPosition();
                now = now.GetNext();
                now.transform.position = position;
            }
        }

        private Vector3 GetNextPosition()
        {
            return transform.position + Vector3.down;
        }


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
                _head = this;
            else
                _head = head;

            return this;
        }

        public PlayingCardGameObject GetHead()
        {
            return _head;
        }

        public PlayingCardGameObject SetStack(CardStack cardStack)
        {
            stack = cardStack;
            return this;
        }
    }
}