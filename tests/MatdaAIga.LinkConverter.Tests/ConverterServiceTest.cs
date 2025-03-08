using Shouldly;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests
{
    public class ConverterServiceTest
    {
        // 프로젝트 루트 디렉토리 설정
        private static readonly string _projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../"));
        // test.md 파일 내 placeholder 사이에 넣어줄 마크다운 파일
        private readonly string _markdown = Path.Combine(_projectRoot, "src/MatdaAIga.Generator/input/pages/about.md");
        // 테스트하고 저장할 파일 경로 : placeholder가 존재하는 파일
        private readonly string _filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/placeholder/correct-placeholder-test.md");

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
            var no_placeholder_filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/placeholder/no-placeholder-test.md");

            // Act
            string markdownContent = await File.ReadAllTextAsync(_markdown);

            // Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, no_placeholder_filepath));
        }

        [Fact]
        public async Task Given_OnePlaceholderFound_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();
            var one_placeholder_filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/placeholder/one-placeholder-test.md");

            // Act
            string markdownContent = await File.ReadAllTextAsync(_markdown);

            // Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, one_placeholder_filepath));
        }

        [Fact]
        public async Task Given_MorePlaceholderFound_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();
            var more_placeholder_filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/placeholder/more-placeholder-test.md");

            // Act
            string markdownContent = await File.ReadAllTextAsync(_markdown);

            // Assert
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(markdownContent, more_placeholder_filepath));
        }

        [Fact]
        public async Task Given_ValidInput_When_Invoke_SaveAsync_Then_It_Should_SaveMarkdownContent()
        {
            // Arrange
            var service = new ConverterService();

            // Act
            string markdownContent = await File.ReadAllTextAsync(_markdown);
            await service.SaveAsync(markdownContent, _filepath);

            // Assert
            string result = await File.ReadAllTextAsync(_filepath);
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
            var non_existence_filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/non-existence.md");
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
                File.SetAttributes(_filepath, FileAttributes.Normal); // 파일 속성을 원래대로 복원
            }
        }
    }
}