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

            Thread[] array = new Thread[numberOfClientsOneInt + numberOfClientsTwoInt];
        //    Thread[] arrayTwo = new Thread[numberOfClientsTwoInt];

            // creating threads for each client
            for (int i = 0; i < array.Length; i = i + 2)
            {
                Thread t = new Thread(() => WithdrawMoney());
                t.Name = "FirstLineClient" + Convert.ToString(i);
                array[i] = t;
            }

            for (int i = 1; i < array.Length; i = i + 2)
            {
                Thread t = new Thread(() => WithdrawMoney());
                t.Name = "SecondLineClient" + Convert.ToString(i);
                array[i] = t;
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i].Start();
                array[i].Join();
            }

            //for (int i = 0; i < arrayTwo.Length; i++)
            //{
            //    arrayTwo[i].Start();
            //    arrayTwo[i].Join();
            //}

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
                Thread.Sleep(500);
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
