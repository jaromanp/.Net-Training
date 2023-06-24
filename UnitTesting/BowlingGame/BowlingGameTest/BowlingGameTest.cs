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

        [TestCase(20,1)]
        public void AllShootsOnePinReturnNumberOfShootsAsScore(int shoots, int pins)
        {
            rollMany(shoots,pins);
            Assert.That(g.score(), Is.EqualTo(20));
        }

        [Test]
        public void OneSpareShouldReturnTheExtraScore()
        {
            g.roll(5);
            g.roll(5);
            g.roll(3);
            rollMany(17, 0);
            Assert.That(g.score(), Is.EqualTo(16));
        }
    }
}