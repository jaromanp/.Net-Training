namespace BowlingGameTest
{
    public class Tests
    {
        private Game g;

        [SetUp]
        public void Setup()
        {
            g = new Game();
        }

        [Test]
        public void testInitialGame()
        {            
            for (int i = 0; i < 20; i++)
            {
                g.roll(0);
            }
            Assert.That(g.score(), Is.EqualTo(0));
        }

        [Test]
        public void testAllRolls()
        {
            for (int i = 0; i < 20; i++)
            {
                g.roll(1);
            }
            Assert.That(g.score(), Is.EqualTo(20));
        }
    }
}