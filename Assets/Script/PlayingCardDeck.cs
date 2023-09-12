using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Script
{
    public class PlayingCardDeck
    {
        private readonly List<bool> avalible;
        private List<PlayingCard> cards;
        private int drawCounter;

        public PlayingCardDeck()
        {
            drawCounter = 0;
            cards = new List<PlayingCard>();
            avalible = new List<bool>();
            foreach (var shapeName in Enum.GetValues(typeof(PlayingCardShape)))
                for (var i = 1; i <= 13; i++)
                {
                    // cards.Add(new PlayingCard(new CardContext { Shape = PlayingCardShape.SPADE, Number = i }));
                    cards.Add(new PlayingCard(new CardContext { Shape = (PlayingCardShape)shapeName, Number = i }));
                    avalible.Add(true);
                }
        }

        public List<PlayingCard> GetCards()
        {
            return cards;
        }

        /// <summary>
        ///     draw card,
        ///     if card is run out, bool will true
        /// </summary>
        /// <returns></returns>
        private Tuple<PlayingCard, bool> RandomDrawCard()
        {
            var avaliable = GetAvaliable();
            if (avaliable.Count == 0) return new Tuple<PlayingCard, bool>(null, true);
            var random = Random.Range(0, avaliable.Count);
            avalible[avaliable[random]] = false;
            return new Tuple<PlayingCard, bool>(cards[avaliable[random]], false);
        }

        public void Shuffle()
        {
            List<PlayingCard> newCards = new();
            for (var i = 0; i < cards.Count; i++) newCards.Add(RandomDrawCard().Item1);

            drawCounter = 0;
            cards = newCards;
        }

        public Tuple<PlayingCard, bool> DrawCard()
        {
            if (drawCounter == cards.Count) return new Tuple<PlayingCard, bool>(null, true);
            return new Tuple<PlayingCard, bool>(cards[drawCounter++], false);
        }

        private List<int> GetAvaliable()
        {
            var avaliable = new List<int>();
            var index = 0;
            foreach (var b in avalible)
            {
                if (b) avaliable.Add(index);
                index++;
            }

            return avaliable;
        }

        public PlayingCardDeck MergeDeck(PlayingCardDeck playingCardDeck)
        {
            foreach (var card in playingCardDeck.cards) cards.Add(card);

            foreach (var get in playingCardDeck.avalible) avalible.Add(get);
            return this;
        }
    }
}