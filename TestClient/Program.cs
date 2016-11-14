using ConcertService;
using ConcertService.Models;
using ConcertService.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {

            //Szinkron lekérdezés tesztelése
            Test();

            //Aszinkron lekérdezés tesztelése
            TestAsync().Wait();
        }

        public async static Task TestAsync()
        {
            RestHelper helper = new RestHelper();
            
            Task<List<Event>> t = helper.GetEventsAsync("Skrillex");

            Console.WriteLine("Kérés feldolgozása...");

            List<Event> events = await t;

            foreach (var e in events)
            {
                Console.WriteLine(e.Title);
            }
            
            Console.ReadKey();
           
        }

        public static void Test()
        {
            RestHelper helper = new RestHelper();

            Console.WriteLine("Kérés feldolgozása...");

            List<Event> events = helper.GetEvents("Skrillex");

            foreach (var e in events)
            {
                Console.WriteLine(e.Title);
            }

            Console.ReadKey();

        }

    }
}
