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
        public void testInitialGame(int shoots, int pins)
        {            
            rollMany(shoots, pins);
            Assert.That(g.score(), Is.EqualTo(0));
        }

        [TestCase(20,1)]
        public void testAllRolls(int shoots, int pins)
        {
            rollMany(shoots,pins);
            Assert.That(g.score(), Is.EqualTo(20));
        }
    }
}