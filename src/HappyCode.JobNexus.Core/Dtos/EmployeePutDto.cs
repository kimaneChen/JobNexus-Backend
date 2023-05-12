using System.ComponentModel.DataAnnotations;

namespace HappyCode.JobNexus.Core.Dtos
{
    public class EmployeePutDto
    {
        [Required]
        public string LastName { get; set; }
    }
}
