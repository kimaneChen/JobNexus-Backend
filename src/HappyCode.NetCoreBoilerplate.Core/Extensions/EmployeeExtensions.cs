using HappyCode.JobNexus.Core.Dtos;
using HappyCode.JobNexus.Core.Models;

namespace HappyCode.JobNexus.Core.Extensions
{
    internal static class EmployeeExtensions
    {
        public static EmployeeDto MapToDto(this Employee source)
        {
            return new EmployeeDto
            {
                Id = source.EmpNo,
                FirstName = source.FirstName,
                LastName = source.LastName,
                BirthDate = source.BirthDate,
                Gender = source.Gender,
            };
        }
    }
}
