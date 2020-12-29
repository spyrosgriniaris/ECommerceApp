using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // support for include and get product and products
         Expression<Func<T, bool>> Criteria {get; }
         List<Expression<Func<T, object>>> Includes {get; }

         // support for ordering
         Expression<Func<T, object>> OrderBy {get; }
         Expression<Func<T, object>> OrderByDescending {get; }

         // pagination properties
         int Take {get; }
         int Skip {get; }
         bool isPagingEnabled {get; }
    }
}