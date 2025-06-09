namespace www_Blush_Brush.Utills
{
    public class RandomPassword
    {
        public static string GenerateRandomPassword(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var passwordChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                passwordChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(passwordChars);
        }
    }
}
