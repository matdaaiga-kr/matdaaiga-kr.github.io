using System.Reflection;

using MatdaAIga.LinkConverter.Models;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests;

public class ConverterServiceTest
{

    [Fact]
    public async Task Given_ValidFilePath_When_Invoke_LoadAsync_Then_It_Should_Return_LinkCollection()
    {
        // Arrange
        var service = new ConverterService();
        var filePath = "files/testfile-0.yaml";

        // Act
        var result = await service.LoadAsync(filePath);

        // Assert
        LinkCollection expected = new()
        {
            Name = "testfile-0",
            Links =
            [
                new LinkItem { Title = "testfile-0-1", Url = "https://www.microsoft.com" },
                new LinkItem { Title = "testfile-0-2", Url = "https://www.google.com" },
                new LinkItem { Title = "testfile-0-3", Url = "https://www.amazon.com" },
            ]
        };

        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Given_NullOrEmptyFilePath_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception(string? filepath) {
        // Arrange
        var service = new ConverterService();
        var markdown = "**Hello, World!**";

        // Act & Assert
        await Should.ThrowAsync<ArgumentException>(() => service.SaveAsync(markdown, filepath));
    }   

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Given_NullOrEmptyMarkdown_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception(string? markdown) {
        // Arrange
        var service = new ConverterService();
        var filepath = "file";

        // Act & Assert
        await Should.ThrowAsync<ArgumentException>(() => service.SaveAsync(markdown, filepath));
    }

    [Theory]
    [InlineData("files/placeholder-0.md")]
    [InlineData("files/placeholder-3-text.md")]
    public async Task Given_InvalidInput_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception(string filepath) {
        // Arrange
        var service = new ConverterService();
        var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, filepath);
        var markdown = "**Hello, World!**";

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdown, filePath));
    }

    [Theory]
    [InlineData("files/placeholder-1.md")]
    [InlineData("files/placeholder-2.md")]
    [InlineData("files/placeholder-3.md")]
    public async Task Given_ValidInput_When_Invoke_SaveAsync_Then_It_Should_SaveMarkdownContent(string filepath)
    {
        // Arrange
        var service = new ConverterService();
        var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, filepath);
        var markdown = "**Hello, World!**";

        // Act
        await service.SaveAsync(markdown, filePath);
        var text = await File.ReadAllTextAsync(filePath);
        var segment = text.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries)
                            .Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                            .Select(p => p.Trim())
                            .ToList();
        // Assert
        segment.Count.ShouldBe(3);
        segment[1].ShouldContain(markdown.Trim());
    }
}
