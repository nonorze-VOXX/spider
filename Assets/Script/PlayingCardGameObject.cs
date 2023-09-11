using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class PlayingCardGameObject : MonoBehaviour
    {
        public PlayingCardSprite playingCardSprite;
        private SpriteRenderer _backSprite;
        private CardStack _cardStack;
        private SpriteRenderer _emptySprite;
        private GameManager _gameManager;
        private PlayingCardGameObject _head;
        private bool _isSlot;
        private PlayingCardGameObject _next;
        private SpriteRenderer _numberSprite;
        private PlayingCard _playingCard;
        private SpriteRenderer _shapeSprite;
        private List<Collider2D> _touchingObjects;
        private bool waiting;

        private void Awake()
        {
            waiting = false;
            _isSlot = false;
            _touchingObjects = new List<Collider2D>();
            _cardStack = null;
            _shapeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _numberSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
            _backSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
            _emptySprite = transform.GetChild(3).GetComponent<SpriteRenderer>();
            _next = null;
        }

        private void OnMouseDown()
        {
            if (waiting) return;

            if (!_isSlot) GetStack().Disconnect(this);
        }

        private void OnMouseDrag()
        {
            if (waiting) return;
            if (!_isSlot)
            {
                var cardPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                cardPosition.z = transform.position.z;
                SetBunchCardPosition(this, cardPosition);
            }
        }

        private void OnMouseUp()
        {
            if (waiting)
            {
                _gameManager.FaCard();
                return;
            }

            if (!_isSlot)
            {
                CardStack stack;
                if (_touchingObjects.Count == 0)
                    stack = GetStack();
                else
                    stack = _touchingObjects[^1].gameObject.transform.GetComponent<PlayingCardGameObject>().GetStack();

                var preTail = stack.GetTail();
                stack.Connect(this);
                SetBunchCardPosition(preTail);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            print(other.transform.name);
            var stack = other.GetComponent<PlayingCardGameObject>().GetStack();
            if (!_touchingObjects.Contains(other) && stack != GetStack())
                _touchingObjects.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_touchingObjects.Contains(other)) return;
            _touchingObjects.Remove(other);
        }

        public void SetGameManager(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public PlayingCardGameObject SetWaiting(bool b)
        {
            waiting = b;
            return this;
        }

        private CardStack GetStack()
        {
            return _cardStack;
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
            if (pc == null)
            {
                _numberSprite.gameObject.SetActive(false);
                _shapeSprite.gameObject.SetActive(false);
                _emptySprite.gameObject.SetActive(false);
                _backSprite.gameObject.SetActive(true);
                _backSprite.sprite = playingCardSprite.back[0];
                return;
            }

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
            _head = head;

            return this;
        }

        public PlayingCardGameObject GetHead()
        {
            return _head;
        }

        public PlayingCardGameObject SetStack(CardStack cardStack)
        {
            _cardStack = cardStack;
            return this;
        }

        public PlayingCardGameObject SetSlot(bool b)
        {
            _isSlot = b;
            return this;
        }
    }
}