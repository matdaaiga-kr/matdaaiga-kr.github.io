using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests
{
    public class ConverterServiceTests
    {
        private readonly string _markdown = "- [Semantic Kernel 워크샵 리포지토리](https://github.com/matdaaiga-kr/semantic-kernel-workshop)";
        private readonly string _filepath = Path.GetFullPath(Path.Combine("src", "MatdaAIga.Generator", "input", "pages", "links.md"));

        /// <summary>
        /// Tests that SaveAsync throws an ArgumentNullException when the markdown parameter is null.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldThrowArgumentNullException_WhenMarkdownIsNull()
        {
            var service = new ConverterService();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.SaveAsync(null, _filepath));
        }

        /// <summary>
        /// Tests that SaveAsync throws an ArgumentNullException when the markdown parameter is an empty string.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldThrowArgumentNullException_WhenMarkdownIsEmpty()
        {
            var service = new ConverterService();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.SaveAsync(string.Empty, _filepath));
        }

        /// <summary>
        /// Tests that SaveAsync throws an ArgumentNullException when the filepath parameter is null.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldThrowArgumentNullException_WhenFilepathIsNull()
        {
            var service = new ConverterService();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.SaveAsync(_markdown, null));
        }

        /// <summary>
        /// Tests that SaveAsync throws an ArgumentNullException when the filepath parameter is an empty string.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldThrowArgumentNullException_WhenFilepathIsEmpty()
        {
            var service = new ConverterService();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.SaveAsync(_markdown, string.Empty));
        }

        /// <summary>
        /// Tests that SaveAsync throws an InvalidOperationException when the placeholder is not found in the file.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldThrowInvalidOperationException_WhenPlaceholderNotFound()
        {
            var service = new ConverterService();
            // 일부러 플레이스 홀더가 없는 파일 경로로 설정 : 존재하는 파일의 경우
            string about_filepath = Path.GetFullPath(Path.Combine("src", "MatdaAIga.Generator", "input", "pages", "about.md"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.SaveAsync(_markdown, about_filepath));
        }

        /// <summary>
        /// Tests that SaveAsync correctly saves the markdown content when valid input is provided.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldSaveMarkdownContent_WhenValidInput()
        {
            var service = new ConverterService();

            // Act
            await service.SaveAsync(_markdown, _filepath);

            // Assert
            string result = await File.ReadAllTextAsync(_filepath);
            Assert.Contains(_markdown, result);
        }

        /// <summary>
        /// Tests that SaveAsync throws an IOException when the file read operation fails.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldThrowIOException_WhenFileReadFails()
        {
            var service = new ConverterService();
            // 존재하지 않는 파일 경로로 설정
            string non_existence_filepath = Path.GetFullPath(Path.Combine("src", "MatdaAIga.Generator", "input", "pages", "non-existence.md"));

            // Act & Assert
            await Assert.ThrowsAsync<IOException>(() => service.SaveAsync(_markdown, non_existence_filepath));
        }
        
        /// <summary>
        /// Tests that SaveAsync throws an IOException when the file write operation fails.
        /// </summary>
        [Fact]
        public async Task SaveAsync_ShouldThrowIOException_WhenFileWriteFails()
        {
            var service = new ConverterService();
            string filepath = Path.GetFullPath("path/to/file.md");
            string content = "<!-- {{ LINKS }} -->\n<!-- {{ /LINKS }} -->";
            await File.WriteAllTextAsync(filepath, content);
            string markdown = "This is the markdown content.";

            // 파일을 읽기 전용으로 설정하여 쓰기 실패를 유도
            File.SetAttributes(filepath, FileAttributes.ReadOnly);

            // Act & Assert
            await Assert.ThrowsAsync<IOException>(() => service.SaveAsync(markdown, filepath));

            // 파일 속성을 원래대로 복원
            File.SetAttributes(filepath, FileAttributes.Normal);
        }
    }
}