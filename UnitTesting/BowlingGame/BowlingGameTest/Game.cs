namespace BowlingGameTest
{
    public class Game
    {
        private int _score = 0;
        public Game()
        {
        }

        public void roll(int pins)
        {
            _score += pins;
        }

        public int score()
        {
            return _score;
        }
    }
}