using System;
using UnityEngine;

namespace Script
{
    public enum PlayingCardShape
    {
        CLUB,
        DIAMOND,
        HEART,
        SPADE
    }
    public struct CardContext
    {
        public PlayingCardShape Shape;
        public int Number;
    }
    public class PlayingCard 
    {
        // public bool open;
        private CardContext _context;

        public PlayingCard(CardContext context)
        {
            _context = context;
        }
    }
    
}