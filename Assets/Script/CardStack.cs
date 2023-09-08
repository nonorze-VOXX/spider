using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class CardStack : MonoBehaviour
    {
        public GameObject cardsPrefab;
        private List<PlayingCardGameObject> _playingCardGameObjects;
        private List<PlayingCard> _playingCards;

        private void Start()
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
                        .SetPlayingCard(pc);
                    _playingCardGameObjects.Add(c);
                    index++;
                }
            }
        }

        private Vector3 GetCardPosition(Vector3 position, int index)
        {
            return new Vector3(position.x, position.y + index * 10, position.z);
        }

        public CardStack SetPlayingcards(List<PlayingCard> playingCards)
        {
            print("playing" + playingCards.Count);
            _playingCards = playingCards;
            return this;
        }
    }
}