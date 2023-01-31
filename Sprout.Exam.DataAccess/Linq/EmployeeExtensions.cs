using System.Linq;

namespace Sprout.Exam.Common.Models
{
    public static class EmployeeExtensions
    {
        /// <summary>
        /// Remove the records with IsDeleted true.
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public static IQueryable<Employee> ActiveOnly(this IQueryable<Employee> employees)
        {
            return employees.Where(e => !e.IsDeleted);
        }
    }
}
