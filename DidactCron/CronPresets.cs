namespace DidactCron
{
    public static class CronPresets
    {
        public const string Yearly = "@yearly";
        public const string Monthly = "@monthly";
        public const string Weekly = "@weekly";
        public const string Daily = "@daily";
        public const string Midnight = "@midnight";
        public const string Hourly = "@hourly";
        public const string Minutely = "@minutely";
        public const string Secondly = "@secondly";
        public const string Immediately = "@immediately";
    }

    public static class CronYearlyPreset
    {
        public const string Key = CronPresets.Yearly;
        public const string CronExpression = "0 0 0 1 1 *";
    }

    public static class CronMonthlyPreset
    {
        public const string Key = CronPresets.Monthly;
        public const string CronExpression = "0 0 0 1 * *";
    }

    public static class CronWeeklyPreset
    {
        public const string Key = CronPresets.Weekly;
        public const string CronExpression = "0 0 0 * * 0";
    }

    public static class CronDailyPreset
    {
        public const string Key = CronPresets.Daily;
        public const string CronExpression = "0 0 0 * * *";
    }

    public static class CronMidnightPreset
    {
        public const string Key = CronPresets.Midnight;
        public const string CronExpression = "0 0 0 * * *";
    }

    public static class CronHourlyPreset
    {
        public const string Key = CronPresets.Hourly;
        public const string CronExpression = "0 0 * * * *";
    }

    public static class CronMinutelyPresent
    {
        public const string Key = CronPresets.Minutely;
        public const string CronExpression = "0 0 * * * *";
    }

    public static class CronSecondlyPreset
    {
        public const string Key = CronPresets.Secondly;
        public const string CronExpression = "* * * * * *";
    }
}
