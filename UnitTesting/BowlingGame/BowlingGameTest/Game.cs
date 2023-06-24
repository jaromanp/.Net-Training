namespace BowlingGameTest
{
    public class Game
    {
        private int _score = 0;
        private int[] _rolls = new int[21];
        private int _currentRoll = 0;

        public Game()
        {
        }

        public void roll(int pins)
        {
            _score += pins;
            _rolls[++_currentRoll] = pins;
        }

        public int score()
        {
            int score = 0;
            int i = 0;
            for (int frame = 0; frame < 10; frame++) 
            { 
                score += _rolls[i] + _rolls[i+1];
                i += 2;
            }
            return score;
        }
    }
}