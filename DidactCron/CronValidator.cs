namespace DidactCron
{
    public static class CronValidator
    {
        /// <summary>
        /// Checks the validity of a CRON expression and outputs an exception if invalid.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool IsValid(string expression, out string? errorMessage)
        {
            try
            {
                _ = new CronExpression(expression);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}