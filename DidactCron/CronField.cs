namespace DidactCron
{
    public class CronField
    {
        // Use HashSet for maximum performance.
        private readonly HashSet<int> _allowedValues;

        public CronField(string expression, int min, int max, bool isDayField = false)
        {
            _allowedValues = Parse(expression, min, max, isDayField);
        }

        // Should be an O(1) operation, see https://stackoverflow.com/questions/18651940/performance-benchmarking-of-contains-exists-and-any.
        public bool Matches(int value) => _allowedValues.Contains(value);

        private static HashSet<int> Parse(string expression, int min, int max, bool isDayField)
        {
            var allowedValues = new HashSet<int>();

            // Split on comma in case of multiple values per field.
            foreach (var part in expression.Split(','))
            {
                // Handle all possible values.
                if (part == "*")
                {
                    for (int i = min; i <= max; i++)
                        allowedValues.Add(i);
                }

                // Handle step increments.
                else if (part.Contains('/'))
                {
                    var split = part.Split('/');
                    // Handle restricted vs. unrestricted increments.
                    int start = split[0] == "*" ? min : int.Parse(split[0]);
                    int step = int.Parse(split[1]);

                    for (int i = start; i <= max; i += step)
                        allowedValues.Add(i);
                }

                // Handle ranges.
                else if (part.Contains('-'))
                {
                    var split = part.Split('-');
                    int rangeStart = int.Parse(split[0]);
                    int rangeEnd = int.Parse(split[1]);

                    for (int i = rangeStart; i <= rangeEnd; i++)
                        allowedValues.Add(i);
                }

                #region TODO Possibly implement later

                // Handle nth day of month.
                //else if (part.Contains("#") && isDayField)
                //{
                //    var split = part.Split('#');
                //    int weekday = int.Parse(split[0]);
                //    int occurrence = int.Parse(split[1]);

                //    DateTime date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                //    int count = 0;
                //    while (date.Month == DateTime.UtcNow.Month)
                //    {
                //        if ((int)date.DayOfWeek == weekday) count++;
                //        if (count == occurrence)
                //        {
                //            allowedValues.Add(date.Day);
                //            break;
                //        }
                //        date = date.AddDays(1);
                //    }
                //}

                // Handle last day of month.
                //else if (part == "L" && isDayField)
                //{
                //    int lastDay = DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month);
                //    allowedValues.Add(lastDay);
                //}

                #endregion

                // Handle plain integer.
                else
                {
                    allowedValues.Add(int.Parse(part));
                }
            }

            return allowedValues;
        }
    }
}