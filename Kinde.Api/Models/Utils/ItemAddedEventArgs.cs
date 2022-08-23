namespace Kinde.Api.Models.Utils
{
    public class ItemAddedEventArgs<TKey, TValue> : EventArgs
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

    }
}
