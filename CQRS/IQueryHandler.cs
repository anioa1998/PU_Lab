using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public interface IQueryHandler<in T, out D> where T : IQuery
    {
        D Handle(T query);
    }
}
