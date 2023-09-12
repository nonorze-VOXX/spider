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
        private bool _open;

        public PlayingCard(CardContext context)
        {
            _open = false;
            _context = context;
        }

        public PlayingCard SetOpen(bool open)
        {
            _open = open;
            return this;
        }

        public bool GetOpen()
        {
            return _open;
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