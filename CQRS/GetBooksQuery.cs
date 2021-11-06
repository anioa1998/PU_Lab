using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public record GetBooksQuery(int Page, int Count) : IQuery; 
}
