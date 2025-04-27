using System.Linq.Expressions;

namespace Produtos.Commons.Testes.Extensions
{
    internal class EFCoreAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public EFCoreAsyncEnumerable(IEnumerable<T> enumerable)
             : base(enumerable)
        { }

        public EFCoreAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetEnumerator();
        }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new EFCoreAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new EFCoreAsyncQueryProvider<T>(this); }
        }
    }

}
