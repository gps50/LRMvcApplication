using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LRMvcApplication.Libraries.LambdaExpressions
{
    public static class LambdaExpressionsHelper
    {
        public static string GetPropertyName<T, F>(Expression<Func<T, F>> selector)
        {
            Contract.Requires(selector != null);
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            var member = selector.Body as MemberExpression;
            var propInfo = member.Member as PropertyInfo;
            return propInfo.Name;
        }        
    }
}
