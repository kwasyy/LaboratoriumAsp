using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LaboratoriumAsp.Models;

public class IsbnAttribute : ValidationAttribute
{
    public bool RequireChecksum { get; set; } = false;

    public IsbnAttribute() => ErrorMessage = "Niepoprawny format ISBN (wymagany ISBN-10 lub ISBN-13)";

    public override bool IsValid(object? value)
    {
        if (value is not string rawIsbn || string.IsNullOrWhiteSpace(rawIsbn))
            return true; // Puste pole obsługuje [Required]

        string cleanIsbn = rawIsbn.Replace("-", "").Replace(" ", "");

        if (!Regex.IsMatch(cleanIsbn, @"^(\d{10}|\d{13})$"))
            return false;

        if (RequireChecksum && cleanIsbn.Length == 13)
        {
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = cleanIsbn[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }
            int checksum = (10 - (sum % 10)) % 10;
            int lastDigit = cleanIsbn[12] - '0';
            return checksum == lastDigit;
        }

        return true;
    }
}