using Shouldly;
using System.Reflection;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests
{
    public class ConverterServiceTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Given_NullOrEmptyFilePath_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception(string? filepath) {
            // Arrange
            var service = new ConverterService();
            var markdownContent = "**Hello, World!**";

            // Act & Assert
            if(filepath == null) {
                await Should.ThrowAsync<ArgumentNullException>(() => service.SaveAsync(markdownContent, null!));
            }
            await Should.ThrowAsync<ArgumentException>(() => service.SaveAsync(markdownContent, string.Empty));
        }   

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Given_NullOrEmptyMarkdown_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception(string? markdown) {
            // Arrange
            var service = new ConverterService();
            var markdownPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @"files/content.md");

            // Act & Assert
            if(markdown == null) {
                await Should.ThrowAsync<ArgumentNullException>(() => service.SaveAsync(null!, markdownPath));
            }
            await Should.ThrowAsync<ArgumentException>(() => service.SaveAsync(string.Empty, markdownPath));
        }

        [Theory]
        [InlineData("files/placeholder-0.md")]
        public async Task Given_InvalidPlaceholderCounts_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception(string filepath) {
            // Arrange
            var service = new ConverterService();
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, filepath);
            var markdownContent = "**Hello, World!**";

            // Act & Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, filePath));
        }

        [Theory]
        [InlineData("files/placeholder-3-text.md")]
        public async Task Given_InvalidSegmentCounts_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception(string filepath) {
            // Arrange
            var service = new ConverterService();
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, filepath);
            var markdownContent = "**Hello, World!**";

            // Act & Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, filePath));
        }

        [Theory]
        [InlineData("files/placeholder-1.md")]
        [InlineData("files/placeholder-3.md")]
        [InlineData("files/placeholder-2.md")]
        public async Task Given_ValidInput_When_Invoke_SaveAsync_Then_It_Should_SaveMarkdownContent(string filepath)
        {
            // Arrange
            var service = new ConverterService();
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, filepath);
            var markdownContent = "**Hello, World!**";

            // Act
            await service.SaveAsync(markdownContent, filePath);
            var result = await File.ReadAllTextAsync(filePath);

            // Assert
            var section = result.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries)
                                .Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                                .Select(p => p.Trim())
                                .ToList();
            section.Count.ShouldBe(3);
            section[1].ShouldContain(markdownContent.Trim());
        }
    }
}