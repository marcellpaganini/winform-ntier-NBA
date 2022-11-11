using System;


namespace Model
{
    public class PlayerDTO
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public string TeamName { get; set; }
        public string Arena { get; set; }
        public string Conference { get; set; }
    }
}
