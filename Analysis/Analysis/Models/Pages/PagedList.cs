using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Analysis.Models.Pages
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IQueryable<T> query, QueryOptions options = null)
        {
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            Options = options;
            if(options != null)
            {
                if (!string.IsNullOrEmpty(options.OrderPropertyName) && !string.IsNullOrEmpty(options.SearchTerm))
                {
                    query = Order(query, options.OrderPropertyName, options.DescendingOrder);
                }
                if (!string.IsNullOrEmpty(options.SearchPropertyName) && !string.IsNullOrEmpty(options.SearchTerm))
                {
                    query = Search(query, options.SearchPropertyName, options.SearchTerm);
                }
            }
            TotalPages = query.Count() / PageSize;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
            List<QueryOptions> queryOptions = new List<QueryOptions>();
            //queryOptions.Where(x => x.PageSize.ToString().Contains('c'));
    }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public QueryOptions Options { get; set; }

        public bool HasPrevoisPages => CurrentPage > 1;
        public bool HasNextPages => CurrentPage < TotalPages;

        
       


        private static IQueryable<T> Search(IQueryable<T> query,string PropertyName,string SearchTerm)
        {
              
            var Parameter = Expression.Parameter(typeof(T), "x");
            var source = PropertyName.Split('.').Aggregate((Expression)Parameter, Expression.Property);
            var Initbody = Expression.Call(source, "ToString", Type.EmptyTypes);
            var body = Expression.Call(Initbody, "Contains", Type.EmptyTypes, Expression.Constant(SearchTerm, typeof(string)));
            
            var lambda = Expression.Lambda<Func<T, bool>>(body, Parameter);
            return query.Where(lambda);
        }
        private static IQueryable<T> Order(IQueryable<T> query , string PropertyName,bool desc)
        {
            
            var parameter = Expression.Parameter(typeof(T), "x");
            var source = PropertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T), source.Type),source,parameter);
            return typeof(Queryable).GetMethods().Single(method => method.Name == (desc ? "OrderByDescending" : "OrderBy")
            && method.IsGenericMethodDefinition
            && method.GetGenericArguments().Length == 2
            && method.GetParameters().Length == 2).MakeGenericMethod(typeof(T), source.Type)
            .Invoke(null, new object[] { query, lambda }) as IQueryable<T>;
        }
    }
}
