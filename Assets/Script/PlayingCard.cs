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
        private readonly CardContext _context;

        public PlayingCard(CardContext context)
        {
            _context = context;
        }

        public int GetNumber()
        {
            return _context.Number;
        }

        public PlayingCardShape GetShape()
        {
            return _context.Shape;
        }

        public CardContext GetContext()
        {
            return _context;
        }
    }
}