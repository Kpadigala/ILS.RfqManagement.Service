using System;
using System.Globalization;

namespace ILS.RfqManagement.Repositories
{
    /// <summary>
    /// Converts the after-hours escalation time-of-day string the API exposes (e.g. "6:00 PM") to and
    /// from the minutes-since-midnight representation rfq.tbrfqassignmentescalation stores it as.
    /// </summary>
    public static class EscalationTimeConverter
    {
        private static readonly string[] TimeFormats = { "h:mm tt", "hh:mm tt" };

        /// <summary>
        /// Parses a time-of-day string (e.g. "6:00 PM" or "6.00 PM") into minutes since midnight (0-1439).
        /// </summary>
        /// <param name="time">The time-of-day string, or null/empty for no time.</param>
        /// <returns>Minutes since midnight, or null when <paramref name="time"/> is null or empty.</returns>
        public static int? ToMinutesSinceMidnight(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
                return null;

            var normalized = time.Trim().Replace('.', ':');

            if (!DateTime.TryParseExact(normalized, TimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
                throw new FormatException($"Unable to parse escalation time '{time}'. Expected a time like '6:00 PM'.");

            return (parsed.Hour * 60) + parsed.Minute;
        }

        /// <summary>
        /// Formats minutes since midnight (0-1439) back into a time-of-day string (e.g. "6:00 PM").
        /// </summary>
        /// <param name="minutesSinceMidnight">Minutes since midnight, or null for no time.</param>
        /// <returns>The formatted time-of-day string, or null when <paramref name="minutesSinceMidnight"/> is null.</returns>
        public static string ToTimeString(int? minutesSinceMidnight)
        {
            if (minutesSinceMidnight == null)
                return null;

            var time = new DateTime(1, 1, 1, minutesSinceMidnight.Value / 60, minutesSinceMidnight.Value % 60, 0);

            return time.ToString("h:mm tt", CultureInfo.InvariantCulture);
        }
    }
}
