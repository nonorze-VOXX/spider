using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class CardStack : MonoBehaviour
    {
        public GameObject cardsPrefab;
        private List<PlayingCardGameObject> _playingCardGameObjects;

        private void Start()
        {
        }

        public CardStack GeneratePlayingCardGameObject(List<PlayingCard> _playingCards)
        {
            _playingCardGameObjects = new List<PlayingCardGameObject>();
            if (_playingCards == null)
            {
                print("card stack is null");
            }
            else
            {
                var index = 0;
                foreach (var pc in _playingCards)
                {
                    print("PC " + pc.GetNumber());
                    var c = Instantiate(cardsPrefab, GetCardPosition(transform.position, index), transform.rotation,
                            transform).GetComponent<PlayingCardGameObject>()
                        .SetPlayingCard(pc).SetStack(this);
                    _playingCardGameObjects.Add(c);
                    index++;
                }
            }

            return this;
        }

        private Vector3 GetCardPosition(Vector3 position, int index)
        {
            return new Vector3(position.x, position.y - index * 1, position.z);
        }


        public Vector2 GetNextPosition()
        {
            return GetCardPosition(transform.position, _playingCardGameObjects.Count);
        }

        public void PutInCard(PlayingCardGameObject playingCardGameObject)
        {
            playingCardGameObject.transform.position = GetNextPosition();
            _playingCardGameObjects.Add(playingCardGameObject);
        }
    }
}