using System.Collections;
using System.Collections.Generic;

namespace XPence.Framework.XmlSerialization
{
    /// <summary>
    ///     Collection of child items. This collection automatically set the
    ///     Parent property of the child items when they are added or removed
    /// </summary>
    /// <typeparam name="TP">Type of the parent object</typeparam>
    /// <typeparam name="T">Type of the child items</typeparam>
    public class ChildItemCollection<TP, T> : IList<T>
        where TP : class
        where T : class, IChildItem<TP>
    {
        private readonly TP parent;
        private readonly IList<T> collection;

        public ChildItemCollection(TP parent)
        {
            this.parent = parent;
            this.collection = new List<T>();
        }

        public ChildItemCollection(TP parent, IList<T> collection)
        {
            this.parent = parent;
            this.collection = collection;
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return collection.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (item != null)
                item.Parent = parent;
            collection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            var oldItem = collection[index];
            collection.RemoveAt(index);
            if (oldItem != null)
                oldItem.Parent = null;
        }

        public T this[int index]
        {
            get { return collection[index]; }
            set
            {
                var oldItem = collection[index];
                if (value != null)
                    value.Parent = parent;
                collection[index] = value;
                if (oldItem != null)
                    oldItem.Parent = null;
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            if (item != null)
                item.Parent = parent;
            collection.Add(item);
        }

        public void Clear()
        {
            foreach (var item in collection)
            {
                if (item != null)
                    item.Parent = null;
            }
            collection.Clear();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return collection.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            var b = collection.Remove(item);
            if (item != null)
                item.Parent = null;
            return b;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (collection as IEnumerable).GetEnumerator();
        }

        #endregion
    }
}
