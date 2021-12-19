using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public record AddAuthorDTO(string FirstName,
                               string SecondName,
                               [MaxLength(1000)] string CV);
}
