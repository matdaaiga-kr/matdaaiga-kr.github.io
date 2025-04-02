using System.Reflection;

using MatdaAIga.LinkConverter.Models;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests;

public class ConverterServiceTest
{
    [Fact]
    public async Task Given_ValidData_When_Invoke_ConvertAsync_Then_It_Should_Return_MarkdownText()
    {
        // Arrange
        var service = new ConverterService();
        var dataWithImage = new LinkCollection
        {
            Name = "Test Collection",
            Links =
            [
                new LinkItem
                {
                    Title = "글로벌 AI 부트캠프 - 대구",
                    Url = "https://example.com",
                    ImageUrl = "/images/example.png"
                }
            ]
        };

        var dataWithoutImage = new LinkCollection
        {
            Name = "Test Collection",
            Links =
            [
                new LinkItem
                {
                    Title = "Semantic Kernel 워크샵 리포지토리",
                    Url = "https://example.com",
                }
            ]
        };

        // Act
        var resultWithImage = await service.ConvertAsync(dataWithImage);
        var resultWithoutImage = await service.ConvertAsync(dataWithoutImage);

        // Assert
        resultWithImage.ShouldContain("- [![글로벌 AI 부트캠프 - 대구](/images/example.png)](https://example.com)\n  [글로벌 AI 부트캠프 - 대구](https://example.com)");
        resultWithoutImage.ShouldContain("- [Semantic Kernel 워크샵 리포지토리](https://example.com)");
    }

    [Fact]
    public async Task Given_NullOrEmptyTitle_When_Invoke_ConvertAsync_Then_It_Should_Throw_Exception()
    {
        // Arrange
        var service = new ConverterService();
        var data = new LinkCollection
        {
            Name = "Test Collection",
            Links =
            [
                new LinkItem
                {
                    Title = null ?? string.Empty,
                    Url = "https://example.com"
                }
            ]
        };

        // Act & Assert
        await Should.ThrowAsync<ArgumentException>(() => service.ConvertAsync(data));
    }

    [Fact]
    public async Task Given_NullOrEmptyUrl_When_Invoke_ConvertAsync_Then_It_Should_Throw_Exception()
    {
        // Arrange
        var service = new ConverterService();
        var data = new LinkCollection
        {
            Name = "Test Collection",
            Links =
            [
                new LinkItem
                {
                    Title = "Valid Title",
                    Url = null ?? string.Empty
                }
            ]
        };

        // Act & Assert
        await Should.ThrowAsync<ArgumentException>(() => service.ConvertAsync(data));
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
