using System.ComponentModel.DataAnnotations;

namespace WebApplicationForTests.Models
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual List<Question> Questions { get; set; }

        public virtual List<TestResult> Results { get; set; }
    }
}
