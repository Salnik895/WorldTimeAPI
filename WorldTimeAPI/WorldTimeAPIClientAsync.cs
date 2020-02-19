using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WorldTimeAPI
{
    public sealed partial class WorldTimeAPIClient
    {
        /// <summary>
        /// Returns the current time based on your public IP in asynchronous operation.
        /// </summary>
        public Task<WorldTime> GetTimeAsync()
        {
            return GetTimeAsync(default(IPAddress));
        }

        /// <summary>
        /// Returns the current time based on a string that represents an IP or time zone in asynchronous operation.
        /// </summary>
        public Task<WorldTime> GetTimeAsync(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (IPAddress.TryParse(input, out IPAddress ip))
            {
                return GetTimeAsync(ip);
            }
            else if (TimeZone.TryParse(input, out TimeZone timezone))
            {
                return GetTimeAsync(timezone);
            }
            else
            {
                throw new ArgumentException("The input string does not represent a valid IP-address or TimeZone instance.", nameof(input));
            }
        }

        /// <summary>
        /// Returns the current time for a timezone in asynchronous operation.
        /// </summary>
        public Task<WorldTime> GetTimeAsync(TimeZone timezone)
        {
            return GetTimeBaseAsync("timezone", timezone);
        }

        /// <summary>
        /// Returns the current time based on the IP of the request in asynchronous operation.
        /// </summary>
        public Task<WorldTime> GetTimeAsync(IPAddress ip)
        {
            return GetTimeBaseAsync("ip", ip);
        }

        /// <summary>
        /// Returns a listing of all timezones in asynchronous operation.
        /// </summary>
        public Task<List<TimeZone>> GetTimeZonesAsync()
        {
            return GetTimeZonesBaseAsync();
        }

        /// <summary>
        /// Returns a listing of all timezones available for that area in asynchronous operation.
        /// </summary>
        public Task<List<TimeZone>> GetTimeZonesAsync(string area)
        {
            return GetTimeZonesBaseAsync(area);
        }
    }
}