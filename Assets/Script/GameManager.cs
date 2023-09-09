using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        public GameObject cardStackPrefab;
        public GameObject cardPrefab;
        private PlayingCardDeck _cardDecks;
        private List<CardStack> _cardStacks;
        private List<PlayingCardGameObject> _playingCardGameObjects;
        private Queue<PlayingCardGameObject> playingCardGameObjectQueue;

        private void Awake()
        {
            playingCardGameObjectQueue = new Queue<PlayingCardGameObject>();
            _playingCardGameObjects = new List<PlayingCardGameObject>();
            _cardDecks = new PlayingCardDeck().MergeDeck(new PlayingCardDeck());
            for (var i = 0; i < 104; i++)
            {
                var (pc, empty) = _cardDecks.DrawCard();
                if (empty) Debug.Log(i);
                var cardGO = Instantiate(cardPrefab).GetComponent<PlayingCardGameObject>().SetPlayingCard(pc);
                playingCardGameObjectQueue.Enqueue(cardGO);
                _playingCardGameObjects.Add(cardGO);
            }

            _cardStacks = new List<CardStack>();
            for (var i = 0; i < 10; i++)
            {
                var c = Instantiate(cardStackPrefab).GetComponent<CardStack>();
                c.transform.position = transform.position + Vector3.right * 5 * i;
                _cardStacks.Add(c);
            }

            for (var i = 0; i < 54; i++)
                _cardStacks[i % 10].Add(playingCardGameObjectQueue.Dequeue().SetHead(_cardStacks[i % 10].GetHead()));
        }

        private void Update()
        {
        }
    }
}