using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class List
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual User? User { get; set; }
    }
}
