namespace CalcStats
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new int[] { 6, 9, 15, -2, 92, 11 }, -2)]
        [TestCase(new int[] { 3, 1, 20, 100, 5, 25 }, 1)]
        public void ShouldReturnMinimumValue(int[] numbers, int expected)
        {
            CalcStat stat = new CalcStat(numbers);

            int minValue = stat.MinValue();

            Assert.That(minValue == expected);
        }

        [TestCase(new int[] { 3, 11, 99, -2, 88, 45 }, 99)]
        [TestCase(new int[] { -32, 43, 20, -101}, 43)]
        public void ShouldReturnMaximumValue(int[] numbers, int expected)
        {
            CalcStat stat = new CalcStat(numbers);

            int maxValue = stat.MaxValue();

            Assert.That(maxValue == expected);
        }
    }
}