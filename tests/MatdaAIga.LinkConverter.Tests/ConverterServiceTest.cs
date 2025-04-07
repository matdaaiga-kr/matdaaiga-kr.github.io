using System.Reflection;
using System.Threading.Tasks;

using MatdaAIga.LinkConverter.Models;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests;

public class ConverterServiceTest
{

    [Fact]
    public async Task Given_ValidFilePath_Without_Image_Url_When_Invoke_LoadAsync_Then_It_Should_Return_LinkCollection()
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
    
    [Fact]
    public async Task Given_ValidFilePath_With_Image_Url_When_Invoke_LoadAsync_Then_It_Should_Return_LinkCollection()
    {
        // Arrange
        var service = new ConverterService();
        var filePath = "files/testfile-1.yaml";

        // Act
        var result = await service.LoadAsync(filePath);

        // Assert
        LinkCollection expected = new()
        {
            Name = "testfile-1",
            Links =
            [
                new LinkItem { Title = "testfile-1-1", Url = "https://www.microsoft.com", ImageUrl = "https://www.microsoft.com/favicon.ico" },
                new LinkItem { Title = "testfile-1-2", Url = "https://www.google.com", ImageUrl = "https://www.google.com/favicon.ico" },
                new LinkItem { Title = "testfile-1-3", Url = "https://www.amazon.com", ImageUrl = "https://www.amazon.com/favicon.ico" },
            ]
        };

        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Given_InvalidFilePath_When_Invoke_LoadAsync_Then_It_Should_Throw_Exception()
    {
        // Arrange
        var service = new ConverterService();
        var filePath = "files/invalidfile.yaml";

        // Act & Assert
        await Should.ThrowAsync<FileNotFoundException>(() => service.LoadAsync(filePath));
    }

    [Theory]
    [InlineData("글로벌 AI 부트캠프 - 대구", "https://example.com", "/images/example.png")]
    public async Task Given_ValidDataWithImageUrl_When_Invoke_ConvertAsync_Then_It_Should_Return_MarkdownText(string title, string url, string imgUrl)
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
                    Title = title,
                    Url = url,
                    ImageUrl = imgUrl
                }
            ]
        };

        // Act
        var result = await service.ConvertAsync(data);

        // Assert
        result.ShouldBe($"- [![{title}]({imgUrl})]({url})\n  [{title}]({url})");  
    }

    [Theory]
    [InlineData("글로벌 AI 부트캠프 - 대구", "https://example.com", null)]
    [InlineData("글로벌 AI 부트캠프 - 대구", "https://example.com", "")]
    public async Task Given_ValidInputWithoutImageUrl_When_Invoke_ConvertAsync_Then_It_Should_Return_MarkdownText(string title, string url, string? imgUrl)
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
                    Title = title,
                    Url = url,
                    ImageUrl = imgUrl
                }
            ]
        };

        // Act
        var result = await service.ConvertAsync(data);

        // Assert
        result.ShouldBe($"- [{title}]({url})");
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
