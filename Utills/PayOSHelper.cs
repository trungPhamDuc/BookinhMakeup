using System.Security.Cryptography;
using System.Text;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Utills
{
    public class PayOSHelper
    {
        public static string GeneratePayOSSignature(Dictionary<string, string> data, string checksumKey)
        {
            var sorted = data.OrderBy(x => x.Key);
            var rawData = string.Join("&", sorted.Select(kv => $"{kv.Key}={kv.Value}"));

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
