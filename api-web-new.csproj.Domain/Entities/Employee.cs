using System;
using System.Collections.Generic;
using System.Text;

namespace api_web_new.csproj.Domain.Entities
{
    public class Employee
    {

        public Employee()
        {
            Departments = new List<Department>();
        }

        public Employee(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Departments = new List<Department>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Department>  Departments { get; set; } = new List<Department>();
    }
}
