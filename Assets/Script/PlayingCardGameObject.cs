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
        private CardStack targetStack;
        private List<Collider2D> touchingObjects;

        private void Awake()
        {
            touchingObjects = new List<Collider2D>();
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
            SetBunchCardPosition(this, cardPosition);
        }

        private void OnMouseUp()
        {
            if (touchingObjects.Count != 0)
            {
                var now = touchingObjects[^1].GetComponent<PlayingCardGameObject>();
                while (now.GetNext() != null) now = now.GetNext();
                SetBunchCardPosition(this, now.GetNextPosition());
            }
        }


        // private CardStack GetStack()
        // {
        //     return _cardStack;
        // }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var head = other.GetComponent<PlayingCardGameObject>().GetHead();
            if (!touchingObjects.Contains(other) && head != GetHead())
                touchingObjects.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (touchingObjects.Contains(other))
            {
                Debug.Log(other.name);
                touchingObjects.Remove(other);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            // if (other.GetComponent<PlayingCardGameObject>().GetHead() != GetHead()) Debug.Log(other.name);
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
    }
}