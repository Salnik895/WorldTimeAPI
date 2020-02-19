using System;
using System.Collections.Generic;
using System.Net;

namespace WorldTimeAPI.Showcase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WorldTimeAPIClient client = new WorldTimeAPIClient())
            {
                List<TimeZone> timezones1 = client.GetTimeZones();
                Console.WriteLine("Request a list of valid timezones:");
                Console.WriteLine(string.Join(Environment.NewLine, timezones1));
                Console.WriteLine();

                List<TimeZone> timezones2 = client.GetTimeZones("Europe");
                Console.WriteLine("Request a list of valid locations for an area:");
                Console.WriteLine(string.Join(Environment.NewLine, timezones2));
                Console.WriteLine();
                
                WorldTime time1 = client.GetTime("Europe/London");                
                Console.WriteLine("Request the current time for a timezone (by string):");
                Console.WriteLine(time1);
                Console.WriteLine();

                WorldTime time2 = client.GetTime(new TimeZone("America", "Argentina", "Salta"));
                Console.WriteLine("Request the current time for a timezone (by WorldTimeAPI.TimeZone instance):");
                Console.WriteLine(time2);
                Console.WriteLine();

                WorldTime time3 = client.GetTime();
                Console.WriteLine("Request the current time based on your public IP:");
                Console.WriteLine(time3);
                Console.WriteLine();

                WorldTime time4 = client.GetTime("8.8.8.8");
                Console.WriteLine("Request the current time for a specific IP (by string):");
                Console.WriteLine(time4);
                Console.WriteLine();

                WorldTime time5 = client.GetTime(IPAddress.Parse("8.8.8.4"));
                Console.WriteLine("Request the current time for a specific IP (by System.Net.IPAddress instance):");
                Console.WriteLine(time5);
                Console.WriteLine();
            }
        }
    }
}
