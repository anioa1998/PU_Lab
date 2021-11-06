using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public class QueryBus
    {
        private readonly IServiceProvider _container;

        public QueryBus(IServiceProvider container)
        {
            _container = container;
        }

        public D Handle<T, D>(T query) where T : IQuery
        {
            IQueryHandler<T,D> queryHandler = _container.GetService(typeof(IQueryHandler<T,D>)) as IQueryHandler<T,D>;

            if (queryHandler != null)
            {
                return queryHandler.Handle(query);
            }
            else
            {
                throw new Exception("Method " + typeof(T).Name + " is not supported.");
            }

        }
    }
}
