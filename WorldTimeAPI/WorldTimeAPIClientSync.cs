using System.Collections.Generic;
using System.Net;

namespace WorldTimeAPI
{
    public sealed partial class WorldTimeAPIClient
    {
        /// <summary>
        /// Returns the current time based on your public IP.
        /// </summary>
        public WorldTime GetTime()
        {
            return SyncHelper.TryInvokeMethodSync(GetTimeAsync);
        }

        /// <summary>
        /// Returns the current time based on a string that represents an IP or time zone.
        /// </summary>
        public WorldTime GetTime(string input)
        {
            return SyncHelper.TryInvokeMethodSync(() => GetTimeAsync(input));
        }

        /// <summary>
        /// Returns the current time for a timezone in asynchronous operation.
        /// </summary>
        public WorldTime GetTime(TimeZone timezone)
        {
            return SyncHelper.TryInvokeMethodSync(() => GetTimeAsync(timezone));
        }

        // <summary>
        /// Returns the current time based on the IP of the request.
        /// </summary>
        public WorldTime GetTime(IPAddress ip)
        {
            return SyncHelper.TryInvokeMethodSync(() => GetTimeAsync(ip));
        }

        /// <summary>
        /// Returns a listing of all timezones.
        /// </summary>
        public List<TimeZone> GetTimeZones()
        {
            return SyncHelper.TryInvokeMethodSync(GetTimeZonesAsync);
        }

        /// <summary>
        /// Returns a listing of all timezones available for that area.
        /// </summary>
        public List<TimeZone> GetTimeZones(string area)
        {
            return SyncHelper.TryInvokeMethodSync(() => GetTimeZonesAsync(area));
        }
    }
}