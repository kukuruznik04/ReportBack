using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportBack.Models
{
    public class ParagraphDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public Guid PlanetId { get; set; }
        public Guid HouseId { get; set; }
        public Guid SignId { get; set; }

    }
}
