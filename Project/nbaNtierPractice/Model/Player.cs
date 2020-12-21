using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Player
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
