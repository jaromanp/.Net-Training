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
                if (_rolls[frameIndex] == 10) //This would be a strike
                {
                    score += 10 + _rolls[frameIndex + 1] + _rolls[frameIndex + 2];
                    frameIndex++;
                }
                else if (isSpare(frameIndex))
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

        private Boolean isSpare(int frameIndex)
        {
            return _rolls[frameIndex] + _rolls[frameIndex + 1] == 10;
        }

        private int sumOfBallsInFrame(int frameIndex)
        {
            return _rolls[frameIndex] + _rolls[frameIndex];
        }

        private int spareBonus(int frameIndex)
        {
            return _rolls[frameIndex + 2];
        }

        private int strikeBonus(int frameIndex)
        {
            return _rolls[frameIndex + 1] + _rolls[frameIndex + 2];
        }
    }
}