namespace BowlingGameTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void testGutterGame()
        {
            Game g = new Game();
            for(int i = 0; i < 20; i++) {
                g.roll(0);
            }
            Assert.That(0, g.score());
        }
    }
}