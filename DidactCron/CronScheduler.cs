namespace DidactCron
{
    public static class CronScheduler
    {
        public static List<DateTime> GetOccurrencesBetween(string cronExpression, DateTime start, DateTime end, string timeZoneId = "UTC")
        {
            var cron = new CronExpression(cronExpression);
            List<DateTime> occurrences = [];
            DateTime next = cron.GetNextOccurrence(start, timeZoneId);

            while (next <= end)
            {
                occurrences.Add(next);
                next = cron.GetNextOccurrence(next, timeZoneId);
            }

            return occurrences;
        }
    }
}