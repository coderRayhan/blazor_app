using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {
        public string AllowedDomain { get; set; } = "asl.com";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //var property = validationContext.ObjectType.GetProperty(validationContext.MemberName!);
            //var attribute = property!.GetCustomAttributes(true);

            //bool hasEmailAddress = attribute.Any(a => a.GetType() == typeof(EmailAddressAttribute));
            //if (hasEmailAddress)
            //{
            //    return new ValidationResult("This attribute cannot be used with [EmailAddress] attribute");
            //}
            if (!value!.ToString()!.Contains('@'))
            {
                return null;
                //return new ValidationResult("The Email field is not a valid e-mail address.", new[] { validationContext.MemberName! });
            }
            if (value.ToString()!.Split("@")[1].ToUpper() == AllowedDomain.ToUpper())
            {
                return null;
            }
            return new ValidationResult(string.IsNullOrEmpty(ErrorMessage) ? $"Domain name must be {AllowedDomain}" : ErrorMessage, new[] {validationContext.MemberName!});
        }
    }
}
