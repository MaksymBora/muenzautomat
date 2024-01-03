namespace muenzautomat
{
    public interface IChangeCalculator
    {
        Dictionary<Coin, int> GetChange(int euros, int cent);
    }

    public class ChangeCalculator : IChangeCalculator
    {
        private const int COUNT_OF_CENT_IN_EURO = 100;
        public static IChangeCalculator GetInsance() => new ChangeCalculator();

        private ChangeCalculator() { }

        public Dictionary<Coin, int> GetChange(int euros, int cent)
        {
            ValidateMoney(euros, cent);

            Dictionary<Coin, int> coins = new Dictionary<Coin, int>();

            int totalAmount = euros * COUNT_OF_CENT_IN_EURO + cent;

            Array enumValues = Enum.GetValues(typeof(Coin));
            Array.Reverse(enumValues);
            
            foreach (Coin coin in enumValues)
            {
                if (totalAmount >= (int)coin)
                {
                    coins[coin] = totalAmount / (int)coin;
                    totalAmount  %= (int)coin;
                }
            }

            return coins;
        }

        private void ValidateMoney(int euros, int cent)
        {
            if (euros < 0)
                throw new ArgumentException($"Invalid parameter {nameof(euros)}, it should be more or equal 0.");

            if (cent < 0)
                throw new ArgumentException($"Invalid parameter {nameof(cent)}, it should be more or equal 0.");
        }
    }
}