namespace WebApplicationForTests.Models
{
    public class TestResult
    {
        public int Id { get; set; }

        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public decimal Score { get; set; }
        public DateTime Date { get; set; }
    }
}