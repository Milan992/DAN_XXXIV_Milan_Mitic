using System;
using System.Threading;

namespace ATM
{
    class Program
    {
        // amount of the money left in the bank
        static int moneyLeft = 10000;

        //object for lock
        static readonly object l = new object();

        static void Main(string[] args)
        {
            string numberOfClientsOne = "";
            string numberOfClientsTwo = "";

            uint numberOfClientsOneInt;
            uint numberOfClientsTwoInt;

            // ask a client to enter number of clients waiting in the line
            while (!uint.TryParse(numberOfClientsOne, out numberOfClientsOneInt) && numberOfClientsOne != "#")
            {
                Console.WriteLine("Press '#' if you want to quit. Please enter only numbers.\nPlease enter number of clients for the first ATM:");
                numberOfClientsOne = Console.ReadLine();
            }
            while (!uint.TryParse(numberOfClientsTwo, out numberOfClientsTwoInt) && numberOfClientsTwo != "#" && numberOfClientsOne != "#")
            {
                Console.WriteLine("Press '#' if you want to quit. Please enter only numbers.\nPlease enter number of clients for the second ATM:");
                numberOfClientsTwo = Console.ReadLine();
            }

            Thread[] array = new Thread[numberOfClientsOneInt + numberOfClientsTwoInt];

            if (numberOfClientsOneInt <= numberOfClientsTwoInt)
            {
                // creating threads for each client
                int counter = 0;
                for (int i = 0; counter < numberOfClientsOneInt; i = i + 2)
                {
                    Thread t = new Thread(() => WithdrawMoney());
                    t.Name = "FirstLineClient" + Convert.ToString(counter + 1);

                    array[i] = t;
                    counter++;
                }

                int name = 0;
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        Thread t = new Thread(() => WithdrawMoney());
                        t.Name = "SecondLineClient" + Convert.ToString(name + 1);
                        array[i] = t;
                        name++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                // creating threads for each client
                int counter = 0;
                for (int i = 0; counter < numberOfClientsTwoInt; i = i + 2)
                {
                    Thread t = new Thread(() => WithdrawMoney());
                    t.Name = "SecondLineClient" + Convert.ToString(counter + 1);

                    array[i] = t;
                    counter++;
                }

                int name = 0;
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        Thread t = new Thread(() => WithdrawMoney());
                        t.Name = "FirstLineClient" + Convert.ToString(name + 1);
                        array[i] = t;
                        name++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                try
                {
                    array[i].Start();
                    array[i].Join();
                }
                catch
                {
                    Console.WriteLine("\t\nNumber of clients must be above 0.\n");
                }
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
                Thread.Sleep(20);
                Console.WriteLine("------------------------------------");
                Console.WriteLine("{0} tries to withdraw {1}\n", Thread.CurrentThread.Name, amountToWithdraw);

                if (amountToWithdraw < moneyLeft)
                {
                    moneyLeft = moneyLeft - amountToWithdraw;
                    Console.WriteLine("{0} withdrawed {1}\n", Thread.CurrentThread.Name, amountToWithdraw);
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
