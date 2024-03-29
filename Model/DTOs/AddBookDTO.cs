﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public record AddBookDTO(string Title,
                             DateTime ReleaseDate,
                             [MaxLength(1000)] string Description,
                             List<int> AuthorIds);
}
