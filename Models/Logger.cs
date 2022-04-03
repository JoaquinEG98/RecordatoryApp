using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Logger
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime LogDate { get; set; }
        public virtual User? User { get; set; }
    }
}
