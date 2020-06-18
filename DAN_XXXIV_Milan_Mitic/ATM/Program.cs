using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
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

            // creating threads for each client
            for (int i = 0; i < (numberOfClientsOneInt); i++)
            {
                Thread t = new Thread(() => WithdrawMoney());
                t.Name = "FirstLineClient" + Convert.ToString(i);
            }

            for (int i = 0; i < (numberOfClientsTwoInt); i++)
            {
                Thread t = new Thread(() => WithdrawMoney());
                t.Name = "SecondLineClient" + Convert.ToString(i);
            }

            // amount of the money left in the bank
            int MoneyLeft = 10000;
        }

        private static void WithdrawMoney()
        {
        }
    }
}
