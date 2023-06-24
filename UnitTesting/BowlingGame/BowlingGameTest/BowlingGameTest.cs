namespace BowlingGameTest
{
    public class Tests
    {
        private Game g;

        private void rollMany(int shoots, int pins)
        {
            for (int i = 0; i < shoots; i++)
            {
                g.roll(pins);
            }
        }

        private void rollSpare()
        {
            g.roll(5);
            g.roll(5);
        }

        private void rollStrike()
        {
            g.roll(10);
        }

        [SetUp]
        public void Setup()
        {
            g = new Game();
        }

        [TestCase(20, 0)]
        public void GutterInAllShootsReturnNoScore(int shoots, int pins)
        {
            rollMany(shoots, pins);
            Assert.That(g.score(), Is.EqualTo(0));
        }

        [TestCase(20, 1, 20)]
        public void AllShootsOnePinReturnNumberOfShootsAsScore(int shoots, int pins, int expectedScore)
        {
            rollMany(shoots, pins);
            Assert.That(g.score(), Is.EqualTo(expectedScore));
        }

        [TestCase(17, 0, 16)]
        public void OneSpareShouldReturnTheExtraScore(int shoots, int pins, int expectedScore)
        {
            rollSpare();
            g.roll(3);
            rollMany(shoots, pins);
            Assert.That(g.score(), Is.EqualTo(expectedScore));
        }

        [TestCase(16, 0, 24)]
        public void OneStrikeReturnCorrectScore(int shoots, int pins, int expectedScore)
        {
            rollStrike();
            g.roll(3);
            g.roll(4);
            rollMany(shoots, pins);
            Assert.That(g.score(), Is.EqualTo(expectedScore));
        }

        [TestCase(12, 10, 300)]
        public void PerfectGameTest(int shoots, int pins, int expectedScore)
        {
            rollMany(shoots, pins);
            Assert.That(g.score(), Is.EqualTo(expectedScore));
        }

    }
}