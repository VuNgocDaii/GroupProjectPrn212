using System.Text.RegularExpressions;

namespace GroupProjectPrn212.Views
{
    public static class FrontendValidation
    {
        public static bool IsRequired(string? value) => !string.IsNullOrWhiteSpace(value);

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true;
            return Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return true;
            return Regex.IsMatch(phone.Trim(), @"^(0|\+84)\d{9,10}$");
        }

        public static bool IsPositiveInt(string? value, out int result)
        {
            return int.TryParse(value?.Trim(), out result) && result > 0;
        }

        public static bool IsPositiveDecimal(string? value, out decimal result)
        {
            return decimal.TryParse(value?.Trim(), out result) && result > 0;
        }

        public static bool IsScore(string? value, out decimal result)
        {
            return decimal.TryParse(value?.Trim(), out result) && result >= 0 && result <= 10;
        }

        public static string BuildXepLoai(decimal diem)
        {
            if (diem >= 8) return "Giỏi";
            if (diem >= 6.5m) return "Khá";
            if (diem >= 5) return "Trung bình";
            return "Yếu";
        }

        public static bool IsTimeRangeValid(TimeOnly start, TimeOnly end)
        {
            return end > start;
        }
    }
}
