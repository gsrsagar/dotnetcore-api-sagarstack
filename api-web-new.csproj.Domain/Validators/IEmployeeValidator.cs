using api_web_new.csproj.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace api_web_new.csproj.Domain.Validators
{
    public interface IEmployeeValidator
    {
        ValidationResult? Validate(EmployeeRequestDTO dto);
    }
}
