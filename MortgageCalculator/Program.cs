using System.Xml.Serialization;

namespace MortgageCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool runAgain = true;

            while (runAgain)
            {
                /*var monthlyIncome = 10416.66;
                var marketValue = 356500;
                var purchasePrice = 375000;
                var hoaFee = 500;
                var downPayment = 100000;
                var monthlyPayments = 12;
                var interest = 5.75/100;
                var numberOfYears = 30;*/

            Console.WriteLine(new string('*', 50));
            Console.WriteLine("Welcome to the Mortage Approval Tool");
            Console.WriteLine(new string('*', 50));

                var monthlyIncome = GetValidDouble("Enter total monthly income: ");
                var purchasePrice = GetValidDouble("Enter the purchase price for the house: ");
                var marketValue = GetValidDouble("Enter the market value for the house: ");
                double downPayment = GetValidDouble("Enter the down payment for the loan: ");
                var interest = GetValidDouble("Enter the interest rate for the loan: ") / 100;
                var monthlyPayments = GetValidInt("Enter number of monthly payments: ");
                var numberOfYears = GetValidInt("Enter term in years [15 or 30] ");
                var hoaFee = GetValidDouble("Enter annual HOA fee: ");

                var calc1 = new MortgageCalculator(monthlyIncome, marketValue, purchasePrice, hoaFee, downPayment, monthlyPayments, interest, numberOfYears);
                var approval = calc1.ApprovalRecommendation();

                Console.WriteLine(calc1.ToString());

                runAgain = RunAgain();
            }

            
        }

        static bool RunAgain()
        {
            bool isValidInput = false;
            bool runAgain = false;

            while (!isValidInput)
            {
                Console.Write("Do you want to run the numbers again? Y/N -> ");
                string choice = Console.ReadLine();

                if (choice.ToLower().StartsWith('y'))
                {
                    isValidInput = true;
                    runAgain = true;
                }
                else if (choice.ToLower().StartsWith('n'))
                {
                    Console.WriteLine("Goodbye!");
                    isValidInput = true;
                    runAgain = false;
                }
                else
                {
                    Console.WriteLine("Invalid input, try again");
                }

            }
            return runAgain;
        }

        static int GetValidInt(string message)
        {
            bool isValid = false;
            int number = 0;
            string input;

            while (!isValid)
            {
                Console.Write(message);
                input = Console.ReadLine();

                isValid = Int32.TryParse(input, out number);

                if (isValid)
                {
                    if(ConfirmInput(number) == false)
                    {
                        isValid = false;
                    }    
                }
                if (!isValid)
                {
                    Console.WriteLine("Please make another choice.");
                }
            }
            return number;
        }

        static double GetValidDouble(string message)
        {
            bool isValid = false;
            double number = 0;
            string input;

            while (!isValid)
            {
                Console.Write(message);
                input = Console.ReadLine();

                isValid = Double.TryParse(input, out number);

                if (isValid)
                {
                    if (ConfirmDoubleInput(number) == false)
                    {
                        isValid = false;
                    }
                }
                if (!isValid)
                {
                    Console.WriteLine("Please make another choice.");
                }
            }
            return number;
        }
        static bool ConfirmDoubleInput(double number)
        {
            string input;
            bool confirmed = false;
            bool validChoice = false;

            while (!validChoice)
            {
                Console.Write($"You entered {number}. Is that correct? Y/N -> ");
                input = Console.ReadLine();

                if (input.ToLower().StartsWith('y'))
                {
                    validChoice = true;
                    confirmed = true;
                }
                else if (input.ToLower().StartsWith('n'))
                {
                    validChoice = true;
                    confirmed = false;
                }
                else
                {
                    validChoice = false;
                    Console.WriteLine("Invalid entry, please choose Y or N");
                }
            }
            return confirmed;
        }

        static bool ConfirmInput(int number)
        {
            string input;
            bool confirmed = false;
            bool validChoice = false;

            while (!validChoice)
            {
                Console.Write($"You entered {number}. Is that correct? Y/N -> ");
                input = Console.ReadLine();

                if (input.ToLower().StartsWith('y'))
                {
                    validChoice = true;
                    confirmed = true;
                }
                else if (input.ToLower().StartsWith('n'))
                {
                    validChoice = true;
                    confirmed = false;
                }
                else
                {
                    validChoice = false;
                    Console.WriteLine("Invalid entry, please choose Y or N");
                }
            }
            return confirmed;
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("1. Create a new loan");
            Console.WriteLine("");
        }
    }
}