using System;
using UnityEngine;

namespace Script
{
    public enum CardShape
    {
        CLUB,
        DIAMOND,
        HEART,
        SPADE
    }
    public struct CardContext
    {
        public CardShape Shape;
        public int Number;
    }
    public class Card : MonoBehaviour
    {
        public bool open;
        public CardContext context;

        private void OnMouseOver()
        {
            // transform.localScale = transform.localScale * 2;
        }

        private void OnMouseEnter()
        {
            transform.localScale = transform.localScale * 2;
        }
        private void OnMouseExit()
        {
            transform.localScale = transform.localScale / 2;
        }
    }
    
}