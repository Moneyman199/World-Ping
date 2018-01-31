using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace OSRSWorldPing
{
    class PingMain
    {

        static List<MultiList> bestTime = new List<MultiList>();

        static void Main(string[] args)
        {
            Begin();
        }

        //instead of testing best of 5, get the average
        //Read input... (take input ToUpper
            //"All" returns all worlds, no order
            //"Order" returns all worlds, order by ping
            //"Min" returns top 5 worlds with lowest ping
            //"Min x" returns top x worlds with lowest ping (maybe just x... check if TryParse (int)


        static void Begin()
        {
            int maxPing = MaxAcceptPing();

            bestTime.Clear();

            List<string> serverList = new List<string>();
            for (int i = 1; i <= 99; i++)
            {
                serverList.Add("oldschool" + i + ".runescape.com");
            }

            foreach (string server in serverList)
            {
                GetPing(server);
            }

            Console.WriteLine("BELOW SERVERS USING MIN PING OF " + maxPing + " ms"); 
            foreach (var item in bestTime) 
            {
                if (item.ResponseTime <= maxPing)
                {
                    Console.WriteLine("Server: " + item.Server + "\nTime: " + item.ResponseTime + " ms");
                    Console.WriteLine(" ");
                }
            }

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Press any key to do another search, or close the window to exit...");
            Console.ReadKey();
            Begin();
        }

        static int MaxAcceptPing()
        {
            int a;
            Console.WriteLine("Type in the maximum acceptable ping to search for. Type 999 for all worlds ping...");

            if (int.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Working on getting the list ready, please wait...");
                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine("Please enter a whole, non-negative number. Press any key to try again...");
                Console.ReadKey();
                Console.Clear();
                MaxAcceptPing();
            }

            return a;
        }

        static void GetPing(string IP)
        {
            long minResponse = 5000; //placeholder

            for (int i = 1; i <= 1; i++)
            {
                try
                {
                    long responseTime;
                    Ping myPing = new Ping();
                    PingReply reply = myPing.Send(IP, 1000); //1 second timeout
                    responseTime = reply.RoundtripTime;

                    if (responseTime < minResponse && responseTime != 0)
                    {
                        minResponse = responseTime;
                    }
                }
                catch
                {
                    continue;
                }
            }

            if (minResponse != 5000 || minResponse != 0)
            {
                bestTime.Add(new MultiList { Server = IP, ResponseTime = minResponse });
            }
        }
    }
}
