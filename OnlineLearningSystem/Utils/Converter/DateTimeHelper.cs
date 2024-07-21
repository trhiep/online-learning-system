namespace OnlineLearningSystem.Utils.Converter
{
    public class DateTimeHelper
    {
        public static DateTime GetFormatedDateTimeFromString(string SearchDate)
        {
            if (DateTime.TryParseExact(SearchDate, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }
            else
            {
                return default(DateTime);
            }
        }
    }
}
