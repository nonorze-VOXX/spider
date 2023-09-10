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
                var go = Instantiate(cardPrefab);
                var cardGO = go.GetComponent<PlayingCardGameObject>().SetPlayingCard(pc);
                cardGO.transform.name = pc.GetNumber() + pc.GetShape().ToString();
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
            {
                var stack = _cardStacks[i % 10];
                stack.Add(playingCardGameObjectQueue.Dequeue().SetHead(stack.GetHead()).SetStack(stack));
            }
        }

        private void Update()
        {
        }
    }
}