using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeDemo.web.Models
{
    public class EmployeeModelView
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please Enter Employee FirstName")]
        [DisplayName("FirstName:")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Employee LastName")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Employee Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please Enter Employee Gender")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Please Enter Employee DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please Enter Employee DateOfJoining")]
        [DataType(DataType.Date)]
        public DateTime? DateOfJoining { get; set; }

        [Required(ErrorMessage = "Please Enter Employee Designation")]
        public string? Designation { get; set; }

        public string? Image { get; set; }


        [Required(ErrorMessage ="Please Enter Employee Details")]
        public string? Description { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string? SkillNames { get; set; }

        public List<string>? AllSkills { get; set; }
    }
}
