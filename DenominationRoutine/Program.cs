namespace DenominationRoutine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> cartridges = new List<int>() { 100, 50, 10 };
            List<int> reversedCartridges = new List<int>() { 100, 10, 50 };
            List<int> payouts = new List<int>() { 30, 60, 80, 140, 230, 370, 610, 980 };
            foreach (var payout in payouts)
            {
                List<string> stringBuilder = new List<string>();
                stringBuilder.AddRange(FindPossibleCombinations(payout, cartridges, stringBuilder));
                stringBuilder.AddRange(FindPossibleCombinations(payout, reversedCartridges, stringBuilder));

                Console.WriteLine($"Payout: {payout}");
                Console.WriteLine(string.Join("\n", stringBuilder.Distinct()));
                Console.WriteLine("");
            }
        }

        private static List<string> FindPossibleCombinations(int payout, List<int> cartridges, List<string> stringBuilder, string currentMessage = "", bool useCurrentMessage = false)
        {
            foreach (var cartridge in cartridges)
            {
                if (payout < cartridge)
                    continue;

                int division = payout / cartridge;
                int rest = payout - (division * cartridge);
                bool isFirstMethodFiring = true;

                if (rest > 0)
                {
                    if (string.IsNullOrWhiteSpace(currentMessage))
                        currentMessage += $"{division} x {cartridge} EUR";
                    else
                        currentMessage += $" + {division} x {cartridge} EUR";

                    FindPossibleCombinations(rest, cartridges, stringBuilder, currentMessage, true);
                    isFirstMethodFiring = false;
                }

                if (!string.IsNullOrWhiteSpace(currentMessage) && useCurrentMessage && isFirstMethodFiring)
                {
                    stringBuilder.Add($"{currentMessage} + {division} x {cartridge} EUR");
                    useCurrentMessage = false;
                    break;
                }

                else if (isFirstMethodFiring && !useCurrentMessage)
                {
                    stringBuilder.Add($"{division} x {cartridge} EUR");
                }

                currentMessage = "";
            }

            return stringBuilder;
        }
    }
}