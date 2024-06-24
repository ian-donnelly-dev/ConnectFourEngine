using System.Collections.Generic;

public class TranspositionTable
{
    private readonly int maxEntries;
    private readonly Dictionary<ulong, int> table;
    private readonly Queue<ulong> order;

    public TranspositionTable(int maxEntries)
    {
        this.maxEntries = maxEntries;
        table = new Dictionary<ulong, int>(maxEntries);
        order = new Queue<ulong>(maxEntries);
    }

    public bool TryGetValue(ulong key, out int score)
    {
        return table.TryGetValue(key, out score);
    }

    public void AddEntry(ulong key, int score)
    {
        if (table.Count >= maxEntries)
        {
            ulong oldestKey = order.Dequeue();
            table.Remove(oldestKey);
        }

        table[key] = score;
        order.Enqueue(key);
    }
}
