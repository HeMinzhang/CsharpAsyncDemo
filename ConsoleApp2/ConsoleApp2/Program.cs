using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class HeXuShiLinqExtensions
    {
        public async static Task<IEnumerable<TSource>> WhereAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           Func<TSource, bool> predicate,
           CancellationToken cancellationToken = default)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            List<TSource> List = new List<TSource>();
            var e = source.GetAsyncEnumerator(cancellationToken);
            while (await e.MoveNextAsync()
                          .ConfigureAwait(false))
            {
                if (predicate(e.Current))
                {
                    List.Add(e.Current);
                }
            }
            return List;
        }
    }
    class Program
    {
       static async Task Test()
       {
            List<int> data = new List<int> { 1,2,3,4,5,6,7,711,11,22};
            var result = await data.ToAsyncEnumerable().WhereAsync(v => v > 5);
            foreach(var item in result)
            {
                Console.WriteLine(item);
            }
       }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Test().Wait();
        }
    }
}
