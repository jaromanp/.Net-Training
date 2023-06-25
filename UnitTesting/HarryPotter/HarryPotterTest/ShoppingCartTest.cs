namespace HarryPotterLibrary
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BuyingOneBookShouldReturnNoDiscountInPrice()
        {
            var books = new Dictionary<BookType, int>
            {
                {BookType.PhilosophersStone, 1},
                {BookType.ChamberOfSecrets, 1},
                {BookType.PrisonerOfAzkaban, 1},
                {BookType.GobletOfFire, 1},
                {BookType.GobletOfFire, 1},
            }

            var potterBooks = new ShoppingBookCart(books);
            var price = potterBooks.GetPrice();

            Assert.That(price, Is.EqualTo(8));
        }
    }
}