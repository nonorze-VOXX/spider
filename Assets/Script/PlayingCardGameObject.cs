using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class PlayingCardGameObject : MonoBehaviour
    {
        [FormerlySerializedAs("cardSprite")] public PlayingCardSprite playingCardSprite;
        private CardStack _cardStack;
        private SpriteRenderer _numberSprite;
        private PlayingCard _playingCard;
        private SpriteRenderer _shapeSprite;
        private bool canPutDown;
        private Vector2 pastPosition;
        private Vector2 putDownPosition;
        private CardStack targetStack;

        private void Awake()
        {
            targetStack = null;
            _cardStack = null;
            canPutDown = true;
            _shapeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _numberSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
        }

        private void OnMouseDown()
        {
            pastPosition = transform.position;
        }

        private void OnMouseDrag()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }

        private void OnMouseUp()
        {
            if (canPutDown)
            {
                 targetStack.PutInCard(this);
            }
            else
            {
                transform.position = pastPosition;
            }

            canPutDown = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.transform.CompareTag("PlayingCard"))
            {
                canPutDown = true;
                putDownPosition = other.transform.GetComponent<PlayingCardGameObject>().GetStackPosition();
                targetStack = other.transform.GetComponent<PlayingCardGameObject>().GetStack();
            }
        }

        private CardStack GetStack()
        {
            return _cardStack;
        }


        private void ChangeCardOutlook(PlayingCard pc)
        {
            //todo change picture
            _numberSprite.sprite = playingCardSprite.numbers[pc.GetNumber()];
            _shapeSprite.sprite = playingCardSprite.shapes[(int)pc.GetShape()];
        }

        public PlayingCardGameObject SetStack(CardStack cs)
        {
            _cardStack = cs;
            return this;
        }

        public Vector2 GetStackPosition()
        {
            return _cardStack.GetNextPosition();
        }

        public PlayingCardGameObject SetPlayingCard(PlayingCard pc)
        {
            _playingCard = pc;
            ChangeCardOutlook(pc);
            return this;
        }
    }
}