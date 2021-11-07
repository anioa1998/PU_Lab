using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.AuthorFiles.Commands
{
    public record AddAuthorRateCommand(int id, short rate) : ICommand;
}
