using System;

namespace WorldTimeAPI
{
    /// <summary>
    /// Represents a time zone.
    /// </summary>
    public struct TimeZone : IEquatable<TimeZone>
    {
        /// <summary>String that represents area.</summary>
        public string Area { get; set; }

        /// <summary>String that represents location.</summary>
        public string Location { get; set; }

        /// <summary>String that represents region.</summary>
        public string Region { get; set; }

        public TimeZone(string area, string location = null, string region = null)
        {
            Area = area;
            Location = location;
            Region = region;
        }

        /// <summary>
        /// Converts the string representation of a time zone to <see cref="TimeZone"/> instance equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string contains a time zone to convert.</param>
        /// <returns>A <see cref="TimeZone"/> instance equivalent contained in s.</returns>
        public static TimeZone Parse(string s)
        {
            Exception exception = ParseImplementation(s, out TimeZone result);
            if (exception != null)
            {
                throw exception;
            }
            return result;
        }

        /// <summary>
        /// Converts the string representation of a time zone to <see cref="TimeZone"/> instance equivalent.
        /// </summary>
        /// <param name="s">A string contains a time zone to convert.</param>
        /// <param name="result">When this method returns, contains the <see cref="TimeZone"/> instance equivalent
        /// of the time zone contained in s, if the conversion succeeded, or default value if the conversion
        /// failed. The conversion fails if the s parameter is null or System.String.Empty, 
        /// is not in a format compliant.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string s, out TimeZone result)
        {
            Exception exception = ParseImplementation(s, out result);
            return exception == null;
        }

        private static Exception ParseImplementation(string s, out TimeZone result)
        {
            result = new TimeZone();

            if (s == null)
            {
                return new ArgumentNullException(nameof(s));
            }

            string[] parts = s.Split('/');
            if (parts.Length > 0)
            {
                result.Area = parts[0];
                result.Location = parts.Length > 1 ? parts[1] : null;
                result.Region = parts.Length > 2 ? parts[2] : null;
                return null;
            }
            else
            {
                return new FormatException();
            }
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance in 'Area/Location' or 'Area/Location/Region' format.
        /// </summary>
        public override string ToString()
        {
            return $"{Area}/{Location}/{Region}".Trim('/');
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = Area != null ? hash * 23 + Area.GetHashCode() : hash;
                hash = Location != null ? hash * 23 + Location.GetHashCode() : hash;
                hash = Region != null ? hash * 23 + Region.GetHashCode() : hash;
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is TimeZone tz)
                return this.Equals(tz);
            else
                return false;
        }

        public bool Equals(TimeZone other)
        {
            return this == other;
        }

        public static bool operator ==(TimeZone tz1, TimeZone tz2)
        {
            return tz1.Area == tz2.Area
                && tz1.Region == tz2.Region
                && tz1.Location == tz2.Location;
        }

        public static bool operator !=(TimeZone tz1, TimeZone tz2)
        {
            return !(tz1 == tz2);
        }
    }
}