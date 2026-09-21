using ILS.RfqManagement.Repositories;
using Shouldly;
using System;
using Xunit;

namespace ILS.RfqManagement.Tests.Repositories
{
    public class EscalationTimeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ToMinutesSinceMidnight_ShouldReturnNull_WhenTimeIsNullOrEmpty(string time)
        {
            // Act
            var actual = EscalationTimeConverter.ToMinutesSinceMidnight(time);

            // Assert
            actual.ShouldBeNull();
        }

        [Theory]
        [InlineData("6:00 PM", 1080)]
        [InlineData("6.00 PM", 1080)]
        [InlineData("12:00 AM", 0)]
        [InlineData("12:00 PM", 720)]
        [InlineData("8:00 AM", 480)]
        [InlineData("11:59 PM", 1439)]
        public void ToMinutesSinceMidnight_ShouldConvertFormattedTimeToMinutesSinceMidnight(string time, int expected)
        {
            // Act
            var actual = EscalationTimeConverter.ToMinutesSinceMidnight(time);

            // Assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void ToMinutesSinceMidnight_ShouldThrow_WhenTimeIsNotParseable()
        {
            // Act & Assert
            Should.Throw<FormatException>(() => EscalationTimeConverter.ToMinutesSinceMidnight("not a time"));
        }

        [Fact]
        public void ToTimeString_ShouldReturnNull_WhenMinutesIsNull()
        {
            // Act
            var actual = EscalationTimeConverter.ToTimeString(null);

            // Assert
            actual.ShouldBeNull();
        }

        [Theory]
        [InlineData(1080, "6:00 PM")]
        [InlineData(0, "12:00 AM")]
        [InlineData(720, "12:00 PM")]
        [InlineData(480, "8:00 AM")]
        [InlineData(1439, "11:59 PM")]
        public void ToTimeString_ShouldConvertMinutesSinceMidnightToFormattedTime(int minutes, string expected)
        {
            // Act
            var actual = EscalationTimeConverter.ToTimeString(minutes);

            // Assert
            actual.ShouldBe(expected);
        }
    }
}
