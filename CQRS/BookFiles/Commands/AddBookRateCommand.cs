using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.BookFiles.Commands
{
    public record AddBookRateCommand(int id, short rate) : ICommand;
}
