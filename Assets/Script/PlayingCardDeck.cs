using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Script
{
    public class PlayingCardDeck
    {
        private List<PlayingCard> cards;
        private List<bool> geted;
        public PlayingCardDeck()
        {
            foreach (var shapeName in Enum.GetValues(typeof(PlayingCardShape)))
            {
                for (int i = 1; i <= 13; i++)
                {
                    cards.Add(new PlayingCard(new CardContext{Shape = (PlayingCardShape)shapeName,Number = i}));
                }
            }
        }

        public Tuple<PlayingCard,bool> DrawCard()
        {
            List<int> avaliable = GetAvaliable();
            if (avaliable.Count == 0)
            {
                return new(null,false);
            }
            var random = Random.Range(0, avaliable.Count);
            geted[avaliable[random]] = false;
            return new (cards[avaliable[random]],true);
        }

        List<int> GetAvaliable()
        {
            List<int> avaliable = new List<int>();
            var index = 0;
            foreach (var b in geted)
            {
                if (b)
                {
                    avaliable.Add(index);
                }
                index++;
            }

            return avaliable;
        }
    }
}