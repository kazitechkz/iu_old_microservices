using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Domain.Models
{
    public class SubjectModel : BaseModel
    {
        public string TitleRu { get; set; }
        public string TitleKk { get; set; }
        public string TitleEn { get; set; }
        public string Code { get; set; }

        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
