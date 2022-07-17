using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models
{
    public class EmailSetting
    {
        public string APIKey { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
    }
}
