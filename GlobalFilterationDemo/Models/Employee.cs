using System.ComponentModel.DataAnnotations;

namespace GlobalFilterationDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
