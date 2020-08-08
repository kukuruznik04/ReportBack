using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportBack.Models
{
    public class Report
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Paragraph Paragraph1 { get; set; }
        public Paragraph Paragraph2 { get; set; }
        public Paragraph Paragraph3 { get; set; }
        public Paragraph Paragraph4 { get; set; }
        public Paragraph Paragraph5 { get; set; }

        public Report() { }
    }
}
