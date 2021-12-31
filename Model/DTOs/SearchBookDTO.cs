using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class SearchBookDTO
    {
        public string MasterQuery { get; set; }
        public bool MatchAll { get; set; }
    }
}
