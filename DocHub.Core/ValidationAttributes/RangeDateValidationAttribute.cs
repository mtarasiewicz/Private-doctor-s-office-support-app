using System.ComponentModel.DataAnnotations;

namespace DocHub.Core.ValidationAttributes;

public class RangeDateValidationAttribute : ValidationAttribute
{
    private readonly string _startDateProperty;

    public RangeDateValidationAttribute(string startDateProperty)
    {
        _startDateProperty = startDateProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var startDateProperty = validationContext.ObjectType.GetProperty(_startDateProperty);
        if (startDateProperty is null) return new ValidationResult($"Unknown property {_startDateProperty}");

        var startDateValue =(DateTime) (startDateProperty.GetValue(validationContext.ObjectInstance) ?? throw new InvalidOperationException());
        var endDateValue = (DateTime) (value ?? throw new ArgumentNullException(nameof(value)));

        if (endDateValue < startDateValue) return new ValidationResult("Start date must be earlier than end date.");
        return ValidationResult.Success;
    }
}