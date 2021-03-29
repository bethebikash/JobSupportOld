using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApp.Data.Models
{
    public class Mentor
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
