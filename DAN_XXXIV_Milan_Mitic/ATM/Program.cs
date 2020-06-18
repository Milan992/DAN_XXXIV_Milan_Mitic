using System;
using System.Threading;

namespace ATM
{
    class Program
    {
        // amount of the money left in the bank
        static int moneyLeft = 10000;
        static readonly object l = new object();

        static void Main(string[] args)
        {
            string numberOfClientsOne = "";
            string numberOfClientsTwo = "";

            int numberOfClientsOneInt;
            int numberOfClientsTwoInt;

            // ask a client to enter number of clients waiting in the line
            while (!int.TryParse(numberOfClientsOne, out numberOfClientsOneInt) && numberOfClientsOne != "#")
            {
                Console.WriteLine("Press '#' if you want to quit. Please enter only numbers.\nPlease enter number of clients for the first ATM:");
                numberOfClientsOne = Console.ReadLine();
            }
            while (!int.TryParse(numberOfClientsTwo, out numberOfClientsTwoInt) && numberOfClientsTwo != "#" && numberOfClientsOne != "#")
            {
                Console.WriteLine("Press '#' if you want to quit. Please enter only numbers.\nPlease enter number of clients for the second ATM:");
                numberOfClientsTwo = Console.ReadLine();
            }

            Thread[] arrayOne = new Thread[numberOfClientsOneInt];
            Thread[] arrayTwo = new Thread[numberOfClientsTwoInt];

            // creating threads for each client
            for (int i = 0; i < arrayOne.Length; i++)
            {
                Thread t = new Thread(() => WithdrawMoney());
                t.Name = "FirstLineClient" + Convert.ToString(i);
                arrayOne[i] = t;
            }

            for (int i = 0; i < arrayTwo.Length; i++)
            {
                Thread t = new Thread(() => WithdrawMoney());
                t.Name = "SecondLineClient" + Convert.ToString(i);
                arrayTwo[i] = t;
            }

            for (int i = 0; i < arrayOne.Length; i++)
            {
                arrayOne[i].Start();
                arrayOne[i].Join();
            }

            for (int i = 0; i < arrayTwo.Length; i++)
            {
                arrayTwo[i].Start();
                arrayTwo[i].Join();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Generates a random number and tries to 
        /// </summary>
        private static void WithdrawMoney()
        {
            Random random = new Random();
            int amountToWithdraw = random.Next(100, 10001);

            lock (l)
            {
                //   Thread.Sleep(500);
                Console.WriteLine("------------------------------------");
                Console.WriteLine("{0} tries to withdraw {1} dinars\n", Thread.CurrentThread.Name, amountToWithdraw);

                if (amountToWithdraw < moneyLeft)
                {
                    moneyLeft = moneyLeft - amountToWithdraw;
                    Console.WriteLine("{0}'s withdrawed {1} dinars\n", Thread.CurrentThread.Name, amountToWithdraw);
                    Console.WriteLine("Amount of money left in the ATM is:{0}\n", moneyLeft);
                }
                else
                {
                    Console.WriteLine("Amount of {0} you asked to withdraw is not available\n", amountToWithdraw);
                }
                Console.WriteLine("------------------------------------");
            }
        }
    }
}
