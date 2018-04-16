using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QhTemplate.ApplicationService.Utils
{
    public class MapperUtil<TIn, TOut>
    {
        private static readonly Func<TIn, TOut> Cache = GetCache();

        private static Func<TIn, TOut> GetCache()
        {
            var parameterExpression = Expression.Parameter(typeof(TIn), "p");
            var memberBindingList =
                (from item in typeof(TOut).GetProperties()
                    where item.CanWrite
                    let property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name))
                    select Expression.Bind(item, property)).Cast<MemberBinding>().ToList();

            var memberInitExpression =
                Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList);
            var lambda =
                Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, parameterExpression);
            return lambda.Compile();
        }

        public static TOut Convert(TIn obj)
        {
            return Cache(obj);
        }
    }
}