using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Utils
{
    public class ItemAddedEventArgs<TKey, TValue>:EventArgs
    {
        public TKey Key { get; set; }   
        public TValue Value { get; set; }   
    }
}
