using Saverr;

namespace TestSuite;

public class SaveFileTest
{
    [Test]
    public void SaveFile_Record_StoresPropertiesCorrectly()
    {
        var path = "/tmp/save1.sav";
        var date = new DateTime(2024, 6, 1, 12, 0, 0);
        var saveFile = new SaveFile(path, date);

        Assert.That(saveFile.path, Is.EqualTo(path));
        Assert.That(saveFile.saveDate, Is.EqualTo(date));
    }

    [Test]
    public void SaveFileDateComparer_Compare_ReturnsZeroForEqualDates()
    {
        var date = DateTime.Now;
        var file1 = new SaveFile("file1", date);
        var file2 = new SaveFile("file2", date);
        var comparer = new SaveFileDateComparer();

        Assert.That(comparer.Compare(file1, file2), Is.EqualTo(0));
    }

    [Test]
    public void SaveFileDateComparer_Compare_ReturnsNegativeWhenFirstIsEarlier()
    {
        var earlier = new DateTime(2024, 1, 1);
        var later = new DateTime(2024, 6, 1);
        var file1 = new SaveFile("file1", earlier);
        var file2 = new SaveFile("file2", later);
        var comparer = new SaveFileDateComparer();

        Assert.That(comparer.Compare(file1, file2), Is.LessThan(0));
    }

    [Test]
    public void SaveFileDateComparer_Compare_ReturnsPositiveWhenFirstIsLater()
    {
        var earlier = new DateTime(2024, 1, 1);
        var later = new DateTime(2024, 6, 1);
        var file1 = new SaveFile("file1", later);
        var file2 = new SaveFile("file2", earlier);
        var comparer = new SaveFileDateComparer();

        Assert.That(comparer.Compare(file1, file2), Is.GreaterThan(0));
    }

    [Test]
    public void SaveFileDateComparer_Compare_HandlesNullsCorrectly()
    {
        var date = DateTime.Now;
        var file = new SaveFile("file", date);
        var comparer = new SaveFileDateComparer();

        Assert.That(comparer.Compare(null, null), Is.EqualTo(0));
        Assert.That(comparer.Compare(null, file), Is.EqualTo(-1));
        Assert.That(comparer.Compare(file, null), Is.EqualTo(1));
    }
}