using Expressions.Task3.E3SQueryProvider.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Expressions.Task3.E3SQueryProvider.Models.Entities;

namespace Expressions.Task3.E3SQueryProvider.QueryProvider
{
    public class E3SEntitySet<T> : IQueryable<T> where T : BaseE3SEntity
    {
        protected readonly Expression _expr;
        protected readonly IQueryProvider _queryProvider;

        public E3SEntitySet(E3SSearchService client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            _expr = Expression.Constant(this);
            _queryProvider = new E3SLinqProvider(client);
        }

        #region public properties

        public Type ElementType => typeof(T);

        public Expression Expression => _expr;

        public IQueryProvider Provider => _queryProvider;

        #endregion

        #region public methods

        public IEnumerator<T> GetEnumerator()
        {
            return _queryProvider.Execute<IEnumerable<T>>(_expr).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _queryProvider.Execute<IEnumerable>(_expr).GetEnumerator();
        }

        #endregion
    }
}