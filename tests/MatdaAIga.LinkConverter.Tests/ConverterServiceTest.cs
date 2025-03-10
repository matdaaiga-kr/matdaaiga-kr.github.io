using Shouldly;
using System.Reflection;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests
{
    public class ConverterServiceTest
    {
        private readonly string _filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"files/placeholder-2.md");
        private readonly string _markdown = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"pages/about.md");

        [Fact]
        public async Task Given_NullMardown_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(() => service.SaveAsync(null!, _filepath));
        }

        [Fact]
        public async Task Given_EmptyMarkdown_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(() => service.SaveAsync(string.Empty, _filepath));
        }

        [Fact]
        public async Task Given_NullFilePath_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(() => service.SaveAsync(_markdown, null!));
        }

        [Fact]
        public async Task Given_EmptyFilePath_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(() => service.SaveAsync(_markdown, string.Empty));
        }

        [Fact]
        public async Task Given_PlaceholderNotFound_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();
            var no_placeholder_filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"files/placeholder-0.md");
            var markdownContent = await File.ReadAllTextAsync(_markdown);

            // Act & Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, no_placeholder_filepath));
        }

        [Fact]
        public async Task Given_OnePlaceholderFound_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();
            var one_placeholder_filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"files/placeholder-1.md");

            // Act
            var markdownContent = await File.ReadAllTextAsync(_markdown);

            // Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, one_placeholder_filepath));
        }

        [Fact]
        public async Task Given_MorePlaceholderFound_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();
            var more_placeholder_filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"files/placeholder-3.md");

            // Act
            var markdownContent = await File.ReadAllTextAsync(_markdown);

            // Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, more_placeholder_filepath));
        }

        [Fact]
        public async Task Given_ValidInput_When_Invoke_SaveAsync_Then_It_Should_SaveMarkdownContent()
        {
            // Arrange
            var service = new ConverterService();
            var markdownContent = await File.ReadAllTextAsync(_markdown);

            // Act
            await service.SaveAsync(markdownContent, _filepath);
            var result = await File.ReadAllTextAsync(_filepath);

            // Assert
            var section = result.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries)
                                .Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                                .Select(p => p.Trim())
                                .ToList();
            
            section[1].ShouldContain(markdownContent.Trim());
        }

        [Fact]
        public async Task Given_FileRead_Fails_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();

            // Act & Assert
            var non_existence_filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"files/non-existence.md");
            await Should.ThrowAsync<IOException>(() => service.SaveAsync(_markdown, non_existence_filepath));
        }
        
        [Fact]
        public async Task Given_FileWrite_Fails_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();

            try 
            {
                // Act & Assert
                File.SetAttributes(_filepath, FileAttributes.ReadOnly);
                await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(_markdown, _filepath));
            }
            finally
            {
                File.SetAttributes(_filepath, FileAttributes.Normal);
            }
        }
    }
}