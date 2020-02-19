using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WorldTimeAPI
{
    /// <summary>
    /// Represents the current time in, and related data about, a time zone.
    /// </summary>
    [DebuggerDisplay("{ToString().Replace(System.Environment.NewLine, \", \")}")]
    public struct WorldTime : IEquatable<WorldTime>
    {
        /// <summary>
        /// The current week number.
        /// </summary>
        public int WeekNumber { get; }

        /// <summary>
        /// The offset from UTC.
        /// </summary>
        public TimeSpan UtcOffset { get; }

        /// <summary>
        /// The current date/time in UTC.
        /// </summary>
        public DateTimeOffset UtcDateTime { get; }

        /// <summary>
        /// Number of seconds since the Epoch.
        /// </summary>
        public int UnixTime { get; }

        /// <summary>
        /// Time zone in 'Area/Location' or 'Area/Location/Region' format.
        /// </summary>
        public TimeZone Timezone { get; }

        /// <summary>
        /// The difference in seconds between the current local time and the time in UTC, excluding any daylight saving difference.
        /// </summary>
        public int RawOffset { get; }

        /// <summary>
        /// An ISO8601-valid string representing the datetime when daylight savings will end for this time zone.
        /// </summary>
        public string DstUntil { get; }

        /// <summary>
        /// The difference in seconds between the current local time and daylight saving time for the location.
        /// </summary>
        public int DstOffset { get; }

        /// <summary>
        /// An ISO8601-valid string representing the datetime when daylight savings  started for this time zone.
        /// </summary>
        public string DstFrom { get; }

        /// <summary>
        /// Flag indicating whether the local time is in daylight savings.
        /// </summary>
        public bool Dst { get; }

        /// <summary>
        /// Ordinal date of the current year.
        /// </summary>
        public int DayOfYear { get; }

        /// <summary>
        /// Current day number of the week, where sunday is 0.
        /// </summary>
        public int DayOfWeek { get; }

        /// <summary>
        /// The current, local date/time.
        /// </summary>
        public DateTimeOffset DateTime { get; }

        /// <summary>
        /// The IP of the client making the request.
        /// </summary>
        public IPAddress ClientIP { get; }

        /// <summary>
        /// The abbreviated name of the timezone.
        /// </summary>
        public string Abbreviation { get; }

        internal WorldTime(string json)
        {
            WeekNumber = default;
            UtcOffset = default;
            UtcDateTime = default;
            UnixTime = default;
            Timezone = default;
            RawOffset = default;
            DstUntil = default;
            DstOffset = default;
            DstFrom = default;
            Dst = default;
            DayOfYear = default;
            DayOfWeek = default;
            DateTime = default;
            ClientIP = default;
            Abbreviation = default;

            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            Utf8JsonReader reader = new Utf8JsonReader(data, isFinalBlock: true, state: default);

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                        {
                            string propertyName = reader.GetString();
                            if (reader.Read())
                            {
                                switch (propertyName)
                                {
                                    case "week_number":
                                        WeekNumber = reader.GetInt32();
                                        break;
                                    case "utc_offset":
                                        UtcOffset = DateTimeOffset.ParseExact(reader.GetString(), "FFFK", CultureInfo.InvariantCulture).Offset;
                                        break;
                                    case "utc_datetime":
                                        UtcDateTime = reader.GetDateTimeOffset();
                                        break;
                                    case "unixtime":
                                        UnixTime = reader.GetInt32();
                                        break;
                                    case "timezone":
                                        Timezone = TimeZone.TryParse(reader.GetString(), out TimeZone tz) ? tz : default;
                                        break;
                                    case "raw_offset":
                                        RawOffset = reader.GetInt32();
                                        break;
                                    case "dst_until":
                                        DstUntil = reader.GetString();
                                        break;
                                    case "dst_offset":
                                        DstOffset = reader.GetInt32();
                                        break;
                                    case "dst_from":
                                        DstFrom = reader.GetString();
                                        break;
                                    case "dst":
                                        Dst = reader.GetBoolean();
                                        break;
                                    case "day_of_year":
                                        DayOfYear = reader.GetInt32();
                                        break;
                                    case "day_of_week":
                                        DayOfWeek = reader.GetInt32();
                                        break;
                                    case "datetime":
                                        DateTime = reader.GetDateTimeOffset();
                                        break;
                                    case "client_ip":
                                        ClientIP = IPAddress.TryParse(reader.GetString(), out IPAddress ip) ? ip : default;
                                        break;
                                    case "abbreviation":
                                        Abbreviation = reader.GetString();
                                        break;
                                }
                            }

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance in txt request format.
        /// </summary>
        public override string ToString()
        {
            return "abbreviation: " + Abbreviation + Environment.NewLine
                 + "client_ip: " + ClientIP + Environment.NewLine
                 + "datetime: " + DateTime.ToString("o", CultureInfo.InvariantCulture) + Environment.NewLine
                 + "day_of_week: " + DayOfWeek + Environment.NewLine
                 + "day_of_year: " + DayOfYear + Environment.NewLine
                 + "dst: " + Dst.ToString().ToLower() + Environment.NewLine
                 + "dst_from: " + DstFrom + Environment.NewLine
                 + "dst_offset: " + DstOffset + Environment.NewLine
                 + "dst_until: " + DstUntil + Environment.NewLine
                 + "raw_offset: " + RawOffset + Environment.NewLine
                 + "timezone: " + Timezone.ToString() + Environment.NewLine
                 + "unixtime: " + UnixTime + Environment.NewLine
                 + "utc_datetime: " + UtcDateTime.ToString("o", CultureInfo.InvariantCulture) + Environment.NewLine
                 + "utc_offset: " + string.Format("{0:+00;-00}:{1:00}", UtcOffset.Hours, UtcOffset.Minutes) + Environment.NewLine
                 + "week_number: " + WeekNumber;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + WeekNumber.GetHashCode();
                hash = hash * 23 + UtcOffset.GetHashCode();
                hash = hash * 23 + UtcDateTime.GetHashCode();
                hash = hash * 23 + UnixTime.GetHashCode();
                hash = hash * 23 + Timezone.GetHashCode();
                hash = hash * 23 + RawOffset.GetHashCode();
                hash = DstUntil != null ? hash * 23 + DstUntil.GetHashCode() : hash;
                hash = hash * 23 + DstOffset.GetHashCode();
                hash = DstFrom != null ? hash * 23 + DstFrom.GetHashCode() : hash;
                hash = hash * 23 + Dst.GetHashCode();
                hash = hash * 23 + DayOfYear.GetHashCode();
                hash = hash * 23 + DayOfWeek.GetHashCode();
                hash = hash * 23 + DateTime.GetHashCode();
                hash = ClientIP != null ? hash * 23 + ClientIP.GetHashCode() : hash;
                hash = Abbreviation != null ? hash * 23 + Abbreviation.GetHashCode() : hash;
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is WorldTime wt)
                return this.Equals(wt);
            else
                return false;
        }

        public bool Equals(WorldTime other)
        {
            return this == other;
        }

        public static bool operator ==(WorldTime wt1, WorldTime wt2)
        {
            return wt1.WeekNumber == wt2.WeekNumber
                && wt1.UtcOffset == wt2.UtcOffset
                && wt1.UtcDateTime == wt2.UtcDateTime
                && wt1.UnixTime == wt2.UnixTime
                && wt1.Timezone == wt1.Timezone
                && wt1.RawOffset == wt1.RawOffset
                && wt1.DstUntil == wt2.DstUntil
                && wt1.DstOffset == wt1.DstOffset
                && wt1.DstFrom == wt2.DstFrom
                && wt1.Dst == wt2.Dst
                && wt1.DayOfYear == wt2.DayOfYear
                && wt1.DayOfWeek == wt2.DayOfWeek
                && wt1.DateTime == wt2.DateTime
                && wt1.ClientIP == wt2.ClientIP
                && wt1.Abbreviation == wt1.Abbreviation;
        }

        public static bool operator !=(WorldTime wt1, WorldTime wt2)
        {
            return !(wt1 == wt2);
        }

        public static explicit operator DateTime(WorldTime time)
        {
            return time.DateTime.DateTime;
        }
    }
}