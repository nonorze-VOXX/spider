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
        private bool _cardStateUpdated;
        private List<PlayingCardGameObject> _playingCardGameObjects;
        private Queue<Queue<PlayingCardGameObject>> playingCardGameObjectListQueue;
        private Queue<PlayingCardGameObject> playingCardGameObjectQueue;

        private void Awake()
        {
            playingCardGameObjectListQueue = new Queue<Queue<PlayingCardGameObject>>();
            playingCardGameObjectQueue = new Queue<PlayingCardGameObject>();
            _playingCardGameObjects = new List<PlayingCardGameObject>();
            _cardDecks = new PlayingCardDeck().MergeDeck(new PlayingCardDeck());
            // _cardDecks.Shuffle();
            for (var i = 0; i < 104; i++)
            {
                var (pc, empty) = _cardDecks.DrawCard();
                if (empty) Debug.Log(i);
                var go = Instantiate(cardPrefab);
                var cardGO = go.GetComponent<PlayingCardGameObject>().SetPlayingCard(pc);
                cardGO.transform.name = pc.GetNumber() + pc.GetShape().ToString();
                cardGO.SetGameManager(this);
                playingCardGameObjectQueue.Enqueue(cardGO);
                _playingCardGameObjects.Add(cardGO);
            }

            _cardStacks = new List<CardStack>();
            for (var i = 0; i < 10; i++)
            {
                var c = Instantiate(cardStackPrefab).GetComponent<CardStack>();
                c.transform.position = transform.position + Vector3.right * 5 * i;
                _cardStacks.Add(c);
                var slot = Instantiate(cardPrefab).GetComponent<PlayingCardGameObject>();
                slot = slot.SetSlot(true);
                slot.SetHead(null);
                slot.SetPlayingCard(null);
                slot.SetStack(c);
                slot.transform.name = "slot" + i;
                c.AddTailSetPosition(slot);
            }

            for (var i = 0; i < 54; i++)
            {
                var stack = _cardStacks[i % 10];
                stack.AddTailSetPosition(playingCardGameObjectQueue.Dequeue().SetHead(stack.GetHead()).SetStack(stack));
            }

            while (playingCardGameObjectQueue.Count != 0)
            {
                var pcList = new Queue<PlayingCardGameObject>();
                for (var i = 0; i < 10; i++)
                {
                    var pc = playingCardGameObjectQueue.Dequeue();
                    pc.SetWaiting(true);
                    pcList.Enqueue(pc);
                }

                playingCardGameObjectListQueue.Enqueue(pcList);
            }

            var position = new Vector3(0, 4, 0);
            foreach (var i in playingCardGameObjectListQueue)
            {
                foreach (var j in i) j.transform.position = position;
                position += Vector3.right;
            }

            _cardStateUpdated = false;
        }

        private void Update()
        {
            if (!_cardStateUpdated)
            {
                print("update card");
                UpdateCardState(_cardStacks);
                _cardStateUpdated = true;
            }
        }

        public void SetCardStateUpdated(bool updated)
        {
            _cardStateUpdated = updated;
        }

        private void UpdateCardState(List<CardStack> cardStacks)
        {
            foreach (var cardStack in cardStacks) cardStack.GetTail().OpenCard();
            CollectCheck(cardStacks);
        }

        private void CollectCheck(List<CardStack> cardStacks)
        {
            foreach (var stack in cardStacks)
            {
                var now = stack.GetHead();
                var continuous = false;
                while (now.GetNext() != null)
                {
                    if (!now.GetOpen())
                    {
                    }
                    else if (now.GetNumber() == 13 || continuous)
                    {
                        Debug.Log(now.GetNumber());
                        continuous = true;
                        continuous = continuous && now.CanTotalConnect(now.GetNext());
                        if (now.GetNext().GetNumber() == 1 && continuous) Debug.Log("find");
                    }

                    now = now.GetNext();
                }
            }
        }

        public void FaCard()
        {
            var cardList = playingCardGameObjectListQueue.Dequeue();
            for (var i = 0; i < 10; i++)
            {
                var stack = _cardStacks[i % 10];
                var card = cardList.Dequeue();
                card.SetHead(stack.GetHead());
                card.SetStack(stack);
                card.SetWaiting(false);
                stack.AddTailSetPosition(card);
            }
        }
    }
}