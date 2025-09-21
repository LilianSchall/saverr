namespace Saverr;

public record SaveFile(string path, DateTime saveDate);
public class SaveFileDateComparer : IComparer<SaveFile>
{
    public int Compare(SaveFile? x, SaveFile? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;
        return x.saveDate.CompareTo(y.saveDate);
    }
}