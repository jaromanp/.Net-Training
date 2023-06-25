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

        [Test]
        public void BuyingTwoBooksOfOneTypeAndOneOfOther()
        {
            var books = new Dictionary<BookType, int>
            {
                {BookType.PhilosophersStone, 2},
                {BookType.ChamberOfSecrets, 1},
                {BookType.PrisonerOfAzkaban, 0},
                {BookType.GobletOfFire, 0},
                {BookType.OrderOfPhoenix, 0},
            };

            var potterBooks = new ShoppingCart(books);
            double price = potterBooks.GetPrice();

            double expected = (16 * (1 - 0.05)) + 8;

            Assert.That(price, Is.EqualTo(expected));
        }

        [Test]
        public void BuyingThreeDifferentBooks()
        {
            var books = new Dictionary<BookType, int>
            {
                {BookType.PhilosophersStone, 1},
                {BookType.ChamberOfSecrets, 1},
                {BookType.PrisonerOfAzkaban, 1},
                {BookType.GobletOfFire, 0},
                {BookType.OrderOfPhoenix, 0},
            };

            var potterBooks = new ShoppingCart(books);
            double price = potterBooks.GetPrice();

            double expected = 24 * (1 - 0.1);

            Assert.That(price, Is.EqualTo(expected));
        }

        [TestCase(1, 0)]
        [TestCase(2, 0.05)]
        [TestCase(3, 0.1)]
        [TestCase(4, 0.2)]
        [TestCase(5, 0.25)]
        public void ReturningAllDiscountsForDifferentTypesOfBooks(int nTypes, double expectedDiscount)
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
            var discount = potterBooks.GetDiscount(nTypes);

            Assert.That(discount, Is.EqualTo(expectedDiscount));
        }
    }
}