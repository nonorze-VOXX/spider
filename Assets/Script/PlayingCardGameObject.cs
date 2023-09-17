using System.Collections.Generic;
using TMPro;
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
        private TextMeshPro _numberText;
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
            _numberText = transform.GetChild(1).GetComponent<TextMeshPro>();
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
            if (!GetOpen()) return;
            if (waiting) return;
            if (_isSlot) return;
            if (DownIsConnection())
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
                _gameManager.SetCardStateUpdated(false);
                return;
            }

            if (!_isSlot)
            {
                CardStack stack;
                if (_touchingObjects.Count == 0)
                {
                    stack = GetStack();
                }
                else
                {
                    stack = _touchingObjects[^1].gameObject.transform.GetComponent<PlayingCardGameObject>().GetStack();
                    if (stack.GetTail().CanNumberConnect(this))
                        _gameManager.SetCardStateUpdated(false);
                    else
                        stack = GetStack();
                }

                var preTail = stack.GetTail();
                stack.Connect(this);
                SetBunchCardPosition(preTail);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            var stack = other.GetComponent<PlayingCardGameObject>().GetStack();
            if (!_touchingObjects.Contains(other) && stack != GetStack())
                _touchingObjects.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_touchingObjects.Contains(other)) return;
            _touchingObjects.Remove(other);
        }

        private bool DownIsConnection()
        {
            var now = this;
            while (now.GetNext() != null)
            {
                if (!now.IsSameShape(now.GetNext()) || !now.IsNumberConnectDown(now.GetNext()))
                    return false;
                now = now.GetNext();
            }

            return true;
        }

        private bool IsNumberConnectDown(PlayingCardGameObject now)
        {
            return now.GetPlayingCard().GetNumber() + 1 == GetPlayingCard().GetNumber();
        }

        private bool IsSameShape(PlayingCardGameObject now)
        {
            return now.GetPlayingCard().GetShape() == GetPlayingCard().GetShape();
        }

        private bool CanNumberConnect(PlayingCardGameObject playingCardGameObject)
        {
            return GetPlayingCard().GetNumber() - 1 ==
                   playingCardGameObject.GetPlayingCard().GetNumber();
        }

        public bool CanTotalConnect(PlayingCardGameObject playingCardGameObject)
        {
            return GetPlayingCard().GetNumber() - 1 ==
                   playingCardGameObject.GetPlayingCard().GetNumber() &&
                   GetPlayingCard().GetShape() == playingCardGameObject.GetPlayingCard().GetShape();
        }

        private PlayingCard GetPlayingCard()
        {
            return _playingCard;
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

        public Vector3 GetNextPosition()
        {
            return transform.position + Vector3.down + new Vector3(0, 0, -1);
        }


        private void ChangeCardOutlook(PlayingCard pc)
        {
            if (pc == null || !pc.GetOpen())
            {
                _numberText.gameObject.SetActive(false);
                _shapeSprite.gameObject.SetActive(false);
                _emptySprite.gameObject.SetActive(false);
                _backSprite.gameObject.SetActive(true);
                if (pc == null)
                    _backSprite.sprite = playingCardSprite.back[0];
                else
                    _backSprite.sprite = playingCardSprite.back[1];
                return;
            }

            if (pc.GetOpen())
            {
                _numberText.gameObject.SetActive(true);
                _shapeSprite.gameObject.SetActive(true);
                _emptySprite.gameObject.SetActive(true);
                _backSprite.gameObject.SetActive(false);
                _numberText.text = pc.GetNumber().ToString();
                _shapeSprite.sprite = playingCardSprite.shapes[(int)pc.GetShape()];
            }
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

        public void OpenCard()
        {
            _playingCard.SetOpen(true);
            ChangeCardOutlook(_playingCard);
        }

        public int GetNumber()
        {
            return GetPlayingCard().GetNumber();
        }

        public bool GetOpen()
        {
            if (_isSlot) return false;
            return GetPlayingCard().GetOpen();
        }
    }
}