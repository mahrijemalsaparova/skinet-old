using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // where(x => x.Id == id) gibi kriter getirir
        Expression<Func<T, bool>> Criteria {get; }
        List<Expression<Func<T, object>>> Includes {get; }
        Expression<Func<T, object>> OrderBy {get;}
        Expression<Func<T, object>> OrderByDescending {get; }
        
        //for pagination
        int Take {get;} // belli sayıda kayıtları getir
        int Skip {get;} // belli sayıdaki kayıtları görmezden gel
        bool IsPagingEnabled {get;}
    }
}