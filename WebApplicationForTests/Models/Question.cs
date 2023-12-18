using System.ComponentModel.DataAnnotations;

namespace WebApplicationForTests.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
