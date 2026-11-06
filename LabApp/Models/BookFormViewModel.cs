using System.ComponentModel.DataAnnotations;

namespace LaboratoriumAsp.Models;

public class BookFormViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Tytuł jest wymagany")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Tytuł: 2–200 znaków")]
    [Display(Name = "Tytuł")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "ISBN jest wymagany")]
    [Isbn(RequireChecksum = true, ErrorMessage = "Nieprawidłowy numer ISBN lub suma kontrolna")]
    public string Isbn { get; set; } = "";

    [DataType(DataType.Date)]
    [NotInFuture]
    [Display(Name = "Data wydania")]
    public DateTime? PublicationDate { get; set; }

    [Range(1, 20000, ErrorMessage = "Liczba stron musi być z zakresu 1–20000")]
    [Display(Name = "Liczba stron")]
    public int NumPages { get; set; } = 100;

    [Range(0.01, 10000, ErrorMessage = "Cena musi być dodatnia i nie większa niż 10000 zł")]
    [Display(Name = "Cena")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Autor jest wymagany")]
    [Display(Name = "Autor")]
    public string Author { get; set; } = "";
    
    [RegularExpression(@"^[A-Z]{3}-\d{4}$", ErrorMessage = "Kod wydawcy musi być w formacie XXX-1234 (np. PWN-2024)")]
    [Display(Name = "Kod wydawcy")]
    public string? PublisherCode { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PublicationDate.HasValue && PublicationDate.Value.Year < 1900 && Price > 100m)
        {
            yield return new ValidationResult(
                "Książka wydana przed 1900 rokiem nie może kosztować więcej niż 100 zł.",
                new[] { nameof(Price), nameof(PublicationDate) }
            );
        }
    }
}