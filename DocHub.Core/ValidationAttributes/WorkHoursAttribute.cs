using System.ComponentModel.DataAnnotations;

namespace DocHub.Core.ValidationAttributes;

public class WorkHoursAttribute : ValidationAttribute
{
    private readonly string _startHourProperty;

    public WorkHoursAttribute(string startHourProperty)
    {
        _startHourProperty = startHourProperty;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var startHourProperty = validationContext.ObjectType.GetProperty(_startHourProperty);
        if (startHourProperty is null) 
            throw new InvalidOperationException($"Unknown property {_startHourProperty}");
        var startHourValue = (int) (startHourProperty.GetValue(validationContext.ObjectInstance) ?? throw new InvalidOperationException());
        var endHourValue =(int) (value ?? throw new ArgumentNullException(nameof(value)));
        if (startHourValue > endHourValue)
        {
            return new ValidationResult("Start hour must be earlier than endHour");
        }
       return ValidationResult.Success;
    }
}