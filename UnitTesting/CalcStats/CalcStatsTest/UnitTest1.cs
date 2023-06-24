namespace CalcStats
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new int[] { 6, 9, 15, -2, 92, 11 }, -2)]
        public void ShouldReturnMinimunValue(int[] numbers, int expected)
        {
            CalcStat stat = new CalcStat(numbers);

            int minValue = stat.MinValue();

            Assert.That(minValue == expected);
        }
    }
}