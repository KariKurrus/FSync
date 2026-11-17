namespace FSync.SyncCore.Extensions;

public static class DictionaryExtensions
{
    public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) =>
        dictionary == null || dictionary.Count == 0;
}
