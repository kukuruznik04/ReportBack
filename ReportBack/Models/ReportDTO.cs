using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportBack.Models
{
    public class ReportDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid Paragraph1Id { get; set; }
        public Guid Paragraph2Id { get; set; }
        public Guid Paragraph3Id { get; set; }
        public Guid Paragraph4Id { get; set; }
        public Guid Paragraph5Id { get; set; }
    }
}
