using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    internal enum GameFlow
    {
        INIT,
        PLAYING
    }

    public class GameManager : MonoBehaviour
    {
        public GameObject cardStackPrefab;
        private PlayingCardDeck _cardDecks;
        private List<CardStack> _cardStacks;
        private GameFlow _gameFlow;
        private List<List<PlayingCard>> playingCards;

        private void Awake()
        {
            _gameFlow = GameFlow.INIT;
            _cardDecks = new PlayingCardDeck().MergeDeck(new PlayingCardDeck());
            playingCards = new List<List<PlayingCard>>
            {
                new(), new(), new(), new(),
                new(), new(), new(), new(), new(), new()
            };
            _cardStacks = new List<CardStack>();
            for (var i = 0; i < 54; i++)
            {
                var (result, empty) = _cardDecks.DrawCard();
                // print(result.GetNumber());
                if (empty)
                    print("empty");
                else
                    playingCards[i % 10].Add(result);
                print(playingCards[i % 10][playingCards[i % 10].Count - 1].GetNumber());
            }

            var index = 0;
            foreach (var playingCard in playingCards)
            {
                var stack = Instantiate(cardStackPrefab, transform).GetComponent<CardStack>()
                    .SetPlayingcards(playingCard);
                _cardStacks.Add(stack);
                var position = stack.transform.position;
                position.x = stack.transform.position.x + index * 5;
                stack.transform.position = position;

                index++;
            }
        }

        private void Update()
        {
            switch (_gameFlow)
            {
                case GameFlow.INIT:
                    _gameFlow = GameFlow.PLAYING;
                    break;
                case GameFlow.PLAYING:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}