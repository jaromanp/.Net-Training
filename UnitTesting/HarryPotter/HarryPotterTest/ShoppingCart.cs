namespace HarryPotterLibrary
{
    internal class ShoppingCart
    {
        private readonly Dictionary<BookType, int> _books;
        private readonly double[] _discounts = { 0, 0.05, 0.1, 0.2, 0.25 };

        public ShoppingCart(Dictionary<BookType, int> books)
        {
            _books = books;
        }

        public double GetPrice()
        {
            double price = 0;
            int differentBooks = GetNumberOfDifferentBooks();

            foreach (var book in _books.Keys.ToList())
            {
                if (_books[book] > 0)
                {
                    price += _books[book] * 8 * (1 - GetDiscount(differentBooks));
                }
            }
            return price;
        }

        public double GetDiscount(int differentBooks)
        {
            return _discounts[differentBooks - 1];
        }

        public int GetNumberOfDifferentBooks()
        {
            return _books.Count(b => b.Value > 0);
        }
    }

}