﻿namespace WebApplicationForTests.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public virtual List<TestResult> Results { get; set; }
    }
}