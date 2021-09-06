using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Person : EntityBase
    {
        public string Salutation { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual string DisplayName
        {
            get
            {
                return (FirstName + " " + LastName).Trim();
            }
        }
        public virtual string FullName
        {
            get
            {
                return (FirstName + " " + MiddleName + " " + LastName).Trim();
            }
        }
        public DateTime? DateOfBirth { get; set; }
        public virtual int Age
        {
            get
            {
                return DateOfBirth == null ? 0 : DateTime.Now.Year - DateOfBirth.Value.Year;
            }
        }
        public string Sex { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
    }
}
