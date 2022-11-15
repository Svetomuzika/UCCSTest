using System.ComponentModel.DataAnnotations.Schema;
using UCSB;

namespace UCSB.Models
{
    public class Letter
    {
        public int Id { get; set; }
        public char Name { get; set; }
        public int Count { get; set; }
    }
}
