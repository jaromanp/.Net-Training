namespace BowlingGameTest
{
    public class Game
    {
        private int[] _rolls = new int[21];
        private int _currentRoll = 0;

        public Game()
        {
        }

        public void roll(int pins)
        {
            _rolls[_currentRoll++] = pins;
        }

        public int score()
        {
            int score = 0;
            int frameIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (_rolls[frameIndex] + _rolls[frameIndex + 1] == 10) //This would be a spare
                {
                    score += 10 + _rolls[frameIndex + 2];
                    frameIndex += 2;
                }
                else
                {
                    score += _rolls[frameIndex] + _rolls[frameIndex + 1];
                    frameIndex += 2;
                }
            }
            return score;
        }        
    }
}