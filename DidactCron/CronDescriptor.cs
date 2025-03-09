namespace DidactCron
{
    public static class CronDescriptor
    {
        public static string ToHumanReadable(string cronExpression)
        {
            var parts = cronExpression.Split(' ');
            if (parts.Length != 6) throw new ArgumentException("Invalid CRON format");

            return $"At {DescribeField(parts[2], "hour")}:{DescribeField(parts[1], "minute")}:{DescribeField(parts[0], "second")} " +
                   $"on {DescribeField(parts[3], "day")} of {DescribeField(parts[4], "month")}, " +
                   $"{DescribeField(parts[5], "day of the week")}";
        }

        private static string DescribeField(string field, string unit)
        {
            return field switch
            {
                "*" => $"every {unit}",
                "*/5" => $"every 5 {unit}s",
                "0" => $"at the start of the {unit}",
                _ => $"at {field} {unit}"
            };
        }
    }
}