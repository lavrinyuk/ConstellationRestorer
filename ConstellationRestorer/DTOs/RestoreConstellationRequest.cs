using System.ComponentModel.DataAnnotations;

namespace ConstellationRestorer.DTOs;

public class RestoreConstellationRequest : IValidatableObject
{
    [Required(ErrorMessage = "Поле reference обязательно.")]
    public string? Reference { get; set; }

    [Required(AllowEmptyStrings = true, ErrorMessage = "Поле damaged обязательно.")]
    public string? Damaged { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        bool referenceIsValid = false;

        if (Reference is not null)
        {
            if (!ContainsOnlyLetters(Reference))
            {
                yield return new ValidationResult(
                    "Эталонный код может содержать только заглавные латинские буквы A-Z.",
                    new[] { nameof(Reference) });
            }
            else if (!HasUniqueSymbols(Reference))
            {
                yield return new ValidationResult(
                    "Эталонный код должен содержать уникальные символы.",
                    new[] { nameof(Reference) });
            }
            else if (Reference.Length > 0)
            {
                referenceIsValid = true;
            }
        }

        if (Damaged is null)
        {
            yield break;
        }

        if (!ContainsOnlyLetters(Damaged))
        {
            yield return new ValidationResult(
                "Повреждённый код может содержать только заглавные латинские буквы A-Z.",
                new[] { nameof(Damaged) });
            yield break;
        }

        if (referenceIsValid && !ContainsOnlySymbolsFrom(Damaged, Reference!))
        {
            yield return new ValidationResult(
                "Повреждённый код может содержать только символы из эталонного кода.",
                new[] { nameof(Damaged) });
        }
    }

    private static bool ContainsOnlyLetters(string value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] < 'A' || value[i] > 'Z')
            {
                return false;
            }
        }

        return true;
    }

    private static bool HasUniqueSymbols(string value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            for (int j = i + 1; j < value.Length; j++)
            {
                if (value[i] == value[j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool ContainsOnlySymbolsFrom(string damaged, string reference)
    {
        for (int i = 0; i < damaged.Length; i++)
        {
            bool found = false;

            for (int j = 0; j < reference.Length; j++)
            {
                if (damaged[i] == reference[j])
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false;
            }
        }

        return true;
    }
}
