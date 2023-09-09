using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Script
{
    public class PlayingCardDeck
    {
        private readonly List<bool> avalible;
        private readonly List<PlayingCard> cards;

        public PlayingCardDeck()
        {
            cards = new List<PlayingCard>();
            avalible = new List<bool>();
            foreach (var shapeName in Enum.GetValues(typeof(PlayingCardShape)))
                for (var i = 1; i <= 13; i++)
                {
                    cards.Add(new PlayingCard(new CardContext { Shape = PlayingCardShape.SPADE, Number = i }));
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
        public Tuple<PlayingCard, bool> DrawCard()
        {
            var avaliable = GetAvaliable();
            if (avaliable.Count == 0) return new Tuple<PlayingCard, bool>(null, true);
            var random = Random.Range(0, avaliable.Count);
            avalible[avaliable[random]] = false;
            return new Tuple<PlayingCard, bool>(cards[avaliable[random]], false);
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