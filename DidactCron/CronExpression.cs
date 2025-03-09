namespace DidactCron
{
    public class CronExpression
    {
        /// <summary>
        /// The input or converted CRON expression.
        /// </summary>
        public string Expression { get; private set; }

        private readonly CronField _seconds;
        private readonly CronField _minutes;
        private readonly CronField _hours;
        private readonly CronField _days;
        private readonly CronField _months;
        private readonly CronField _dayOfWeek;
        private readonly bool _isImmediate;

        public CronExpression(string expression)
        {
            // Handle immediate.
            if (expression == CronPresets.Immediately)
            {
                Expression = expression;
                _isImmediate = true;
                return;
            }

            // Handle presets.
            if (expression.StartsWith('@'))
            {
                expression = ConvertToCronPreset(expression);
            }

            Expression = expression;

            var parts = expression.Split(' ');

            // Validate CRON expression structure.
            if (parts.Length != 6)
                throw new ArgumentException("CRON expression must have exactly 6 fields: seconds minutes hours days months day-of-week");

            _seconds = new CronField(parts[0], 0, 59);
            _minutes = new CronField(parts[1], 0, 59);
            _hours = new CronField(parts[2], 0, 23);
            _days = new CronField(parts[3], 1, 31, true);
            _months = new CronField(parts[4], 1, 12);
            _dayOfWeek = new CronField(parts[5], 0, 6, true);
        }

        private static string ConvertToCronPreset(string expression)
        {
            return expression switch
            {
                CronPresets.Yearly => CronYearlyPreset.CronExpression,
                CronPresets.Monthly => CronMonthlyPreset.CronExpression,
                CronPresets.Weekly => CronWeeklyPreset.CronExpression,
                CronPresets.Daily => CronDailyPreset.CronExpression,
                CronPresets.Midnight => CronMidnightPreset.CronExpression,
                CronPresets.Hourly => CronHourlyPreset.CronExpression,
                CronPresets.Minutely => CronMinutelyPresent.CronExpression,
                CronPresets.Secondly => CronSecondlyPreset.CronExpression,
                _ => throw new ArgumentException($"Unsupported CRON Preset expression: {expression}")
            };
        }

        public DateTime GetNextOccurrence(DateTime after, string timeZoneId = "UTC")
        {
            if (_isImmediate) return DateTime.UtcNow;

            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Ensure `after` has the correct Kind before conversion
            if (after.Kind == DateTimeKind.Unspecified)
            {
                after = DateTime.SpecifyKind(after, DateTimeKind.Utc);
            }

            // Only convert to UTC if necessary
            DateTime next = (after.Kind == DateTimeKind.Utc)
                ? after.AddSeconds(1)
                : TimeZoneInfo.ConvertTimeToUtc(after.AddSeconds(1), tz);

            //DateTime next = TimeZoneInfo.ConvertTimeToUtc(after.AddSeconds(1), tz);

            while (true)
            {
                int daysInMonth = DateTime.DaysInMonth(next.Year, next.Month);

                if (!_seconds.Matches(next.Second)) { next = next.AddSeconds(1); continue; }
                if (!_minutes.Matches(next.Minute)) { next = next.AddMinutes(1).AddSeconds(-next.Second); continue; }
                if (!_hours.Matches(next.Hour)) { next = next.AddHours(1).AddMinutes(-next.Minute).AddSeconds(-next.Second); continue; }
                if (!_days.Matches(next.Day) || next.Day > daysInMonth || !_months.Matches(next.Month) || !_dayOfWeek.Matches((int)next.DayOfWeek))
                {
                    next = next.AddDays(1).Date;
                    continue;
                }

                return TimeZoneInfo.ConvertTimeFromUtc(next, tz);
            }
        }
    }
}