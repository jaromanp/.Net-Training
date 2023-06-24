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

        public int MaxValue()
        {
            int maxValue = this.numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (this.numbers[i] > maxValue)
                {
                    maxValue = this.numbers[i];
                }
            }
            return maxValue;
        }

        public int Length()
        {
            return this.numbers.Length;
        }

        public double Average()
        {
            int sum = 0;
            int length = this.numbers.Length;
            for(int i=0; i<this.numbers.Length; i++)
            {
                sum += this.numbers[i];
            }
            double average = sum / (double)length;
            return average;
        }
    }
}