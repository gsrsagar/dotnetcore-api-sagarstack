using api_web_new.csproj.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace api_web_new.csproj.Domain.Validators
{
    public class EmployeeValidator : IEmployeeValidator
    {
        public ValidationResult? Validate(EmployeeRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                return new ValidationResult("First name is required.");
            if (string.IsNullOrWhiteSpace(dto.LastName))
                return new ValidationResult("Last name is required.");
            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                return new ValidationResult("A valid email is required.");
            return ValidationResult.Success;
        }
    }
}
