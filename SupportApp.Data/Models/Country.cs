using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SupportApp.Data.Models
{
    public class Country: CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(3)]
        public string Extension { get; set; }
        public string FlagIcon { get; set; }
        public string Abbreviation { get; set; }

    }
}
