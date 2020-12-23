using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Player
    {
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        public void AddError(ValidationError error)
        {
            Errors.Add(error);
        }

        public int PlayerId { get; set; }

        public int PlayerIdOut { get; set; }


        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name must be between 1 to 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name must be between 1 to 50 characters.")]
        public string LastName { get; set; }
        
        [Required]
        public bool Active { get; set; }
        
        [Required]
        [MinimumAge(19, ErrorMessage = "Drafted players must be at least 19 years old during the calendar year of the draft.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(100000, 50000000, ErrorMessage = "Salary must be between $100.000,00 to $50.000.000,00.")]
        public decimal Salary { get; set; }
        
        public string TeamName { get; set; }
        
        public int? TeamId { get; set; }
        
        public string Arena { get; set; }
        
        public string Conference { get; set; }

        public byte[] RecordVersion { get; set; }
    }
}
