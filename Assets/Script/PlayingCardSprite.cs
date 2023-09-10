using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    [CreateAssetMenu(fileName = "PlayingCardSprite", menuName = "Card/PlayingCardSprite", order = 0)]
    public class PlayingCardSprite : ScriptableObject
    {
        public List<Sprite> numbers;
        public List<Sprite> shapes;
        public List<Sprite> back;
    }
}