using System.Collections.ObjectModel;
using System.Text;

namespace System.Collections.Generic
{
    public static class ListExtensions
    {
        public delegate string Indexer<T>(T obj);

        /// <summary>
        /// Concatenate a collection of objects using a lambda to retrieve the desitred string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="indexer"></param>
        /// <param name="separator"></param>
        /// <returns></returns>

        public static string Concatenate<T>(this ICollection<T> collection, Indexer<T> indexer, char separator)
        {
            if (collection == null || collection.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var t in collection) sb.Append(indexer(t)).Append(separator);
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        /// <summary>
        /// Concatenate a collection of objects using a lambda to retrieve the desitred string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="indexer"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Concatenate<T>(this IEnumerable collection, Indexer<T> indexer, char separator)
        {
            var sb = new StringBuilder();
            foreach (var t in collection) sb.Append(indexer((T) t)).Append(separator);
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        /// <summary>
        /// Convenience method for creating Observables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            var result = new ObservableCollection<T>();
            foreach (var item in enumerable)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
