using System.Reflection;
using Saverr;

namespace TestSuite;

[TestFixture]
public class ToolsTest
{
    [TestCase("2024-06-01", "save-2024-06-01.zip")]
    [TestCase("1999-12-31", "save-1999-12-31.zip")]
    [TestCase("0001-01-01", "save-0001-01-01.zip")]
    [TestCase("2023-02-09", "save-2023-02-09.zip")]
    public void GenerateName_ReturnsCorrectFormat(string dateString, string expected)
    {
        DateTime date = DateTime.Parse(dateString);
        // Use reflection to access private static method
        var method = typeof(Tools).GetMethod("GenerateName", BindingFlags.NonPublic | BindingFlags.Static);
        string result = (string)method!.Invoke(null, new object[] { date })!;
        Assert.That(expected, Is.EqualTo(result));
    }
}