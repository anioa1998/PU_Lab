using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.BookFiles.Queries
{
    public record GetBookQuery(int id) : IQuery;
}
