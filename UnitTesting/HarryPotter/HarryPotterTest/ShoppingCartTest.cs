namespace HarryPotterLibrary
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetNumberOfDifferentBooksTest()
        {
            var books = new Dictionary<BookType, int>
            {
                {BookType.PhilosophersStone, 1},
                {BookType.ChamberOfSecrets, 2},
                {BookType.PrisonerOfAzkaban, 0},
                {BookType.GobletOfFire, 0},
                {BookType.OrderOfPhoenix, 3},
            };

            var potterBooks = new ShoppingCart(books);
            var price = potterBooks.GetNumberOfDifferentBooks();

            Assert.That(price, Is.EqualTo(3));
        }

        [Test]
        public void BuyingOneBookShouldReturnNoDiscountInPrice()
        {
            var books = new Dictionary<BookType, int>
            {
                {BookType.PhilosophersStone, 1},
                {BookType.ChamberOfSecrets, 0},
                {BookType.PrisonerOfAzkaban, 0},
                {BookType.GobletOfFire, 0},
                {BookType.OrderOfPhoenix, 0},
            };

            var potterBooks = new ShoppingCart(books);
            var price = potterBooks.GetPrice();

            Assert.That(price, Is.EqualTo(8));
        }

        [Test]
        public void BuyingNothingShouldReturnCero()
        {
            var books = new Dictionary<BookType, int>
            {
                {BookType.PhilosophersStone, 0},
                {BookType.ChamberOfSecrets, 0},
                {BookType.PrisonerOfAzkaban, 0},
                {BookType.GobletOfFire, 0},
                {BookType.OrderOfPhoenix, 0},
            };

            var potterBooks = new ShoppingCart(books);
            var price = potterBooks.GetPrice();

            Assert.That(price, Is.EqualTo(0));
        }

        [Test]
        public void BuyingTwoOrMoreBooksThatAreTheSame()
        {
            var books = new Dictionary<BookType, int>
            {
                {BookType.PhilosophersStone, 2},
                {BookType.ChamberOfSecrets, 0},
                {BookType.PrisonerOfAzkaban, 0},
                {BookType.GobletOfFire, 0},
                {BookType.OrderOfPhoenix, 0},
            };

            var potterBooks = new ShoppingCart(books);
            var price = potterBooks.GetPrice();

            Assert.That(price, Is.EqualTo(16));
        }
    }
}