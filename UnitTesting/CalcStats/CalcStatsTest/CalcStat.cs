namespace CalcStats
{
    internal class CalcStat
    {
        private int[] numbers;

        public CalcStat(int[] numbers)
        {
            this.numbers = numbers;
        }

        public int MinValue()
        {
            int minValue = this.numbers[0];
            for(int i=1; i<numbers.Length; i++)
            {
                if (this.numbers[i] < minValue)
                {
                    minValue = this.numbers[i];
                } 
            }
            return minValue;
        }
    }
}