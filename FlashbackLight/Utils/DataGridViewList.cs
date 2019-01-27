
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashbackLight.Utils
{
    public class DataGridViewList<T> : List<ListItem<T>> 
    {
        private readonly List<T> source;

        public DataGridViewList(List<T> source)
        {
            this.source = source;
            AddRange(source.Select(x => new ListItem<T> { Value = x }));
        }

        public void PushData()
        {
            source.Clear();
            source.AddRange(this.Select(x => x.Value));
        }
    }
    public class ListItem<T>
    {
        public T Value { get; set; }
    }
}
