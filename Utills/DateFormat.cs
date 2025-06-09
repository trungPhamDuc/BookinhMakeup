namespace www_Blush_Brush.Utills
{
    public class DateFormat
    {
        public static string ConvertToIsoDate(string input)
        {
            try
            {
                var parts = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4) return null;

                var day = int.Parse(parts[0]);
                var month = ConvertVietnameseMonthToNumber(parts[2]);
                var year = int.Parse(parts[3]);

                var date = new DateTime(year, month, day);
                return date.ToString("yyyy-MM-dd");
            }
            catch
            {
                return null;
            }
        }
        private static int ConvertVietnameseMonthToNumber(string thang)
        {
            return int.TryParse(thang, out int result) ? result : 1;
        }
    }
}
