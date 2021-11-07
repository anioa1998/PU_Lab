using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.AuthorFiles.Commands
{
    public record AddAuthorCommand(AddAuthorDTO authorDTO) : ICommand;
}
