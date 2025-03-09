namespace DidactCron
{
    public static class CronValidator
    {
        public static bool IsValid(string expression, out string errorMessage)
        {
            try
            {
                _ = new CronExpression(expression);
                errorMessage = string.Empty;
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