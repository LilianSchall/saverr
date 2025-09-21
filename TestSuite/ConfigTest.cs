using Saverr;

namespace TestSuite;

public class ConfigTest
{
    [Test]
    public void From_ValidArgs_ReturnsConfig()
    {
        // Arrange
        string[] args = { "saverr", "/src", "/dst", "5" };

        // Act
        var config = Config.From(args);

        // Assert
        Assert.That(config.sourceFolder, Is.EqualTo("/src"));
        Assert.That(config.destinationFolder, Is.EqualTo("/dst"));
        Assert.That(config.nbSaves, Is.EqualTo(5));
    }

    [Test]
    public void From_InvalidArgsLength_ThrowsArgumentException()
    {
        // Arrange
        string[] args = { "saverr", "/src", "/dst" };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Config.From(args));
        Assert.That(ex.Message, Does.Contain("Usage:"));
    }

    [Test]
    public void From_InvalidNbSaves_ThrowsArgumentException()
    {
        // Arrange
        string[] args = { "saverr", "/src", "/dst", "notanumber" };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Config.From(args));
        Assert.That(ex.Message, Is.EqualTo("max save parameter is not a valid integer."));
    }
}