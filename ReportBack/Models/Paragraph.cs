using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportBack.Models
{
    public class Paragraph
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        /*        public Guid PlanetId { get; set; }
                public Guid HouseId { get; set; }
                public Guid SignId { get; set; }*/

        public Planet Planet { get; set; }
        public House House { get; set; }
        public Sign Sign { get; set; }

        public Paragraph() { }

        public Paragraph(ParagraphDTO paragraphDTO)
        {
            Name = paragraphDTO.Name;
            Gender = paragraphDTO.Gender;
            Age = paragraphDTO.Age;
        }

    }
}
