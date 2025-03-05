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
        private readonly string _filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/correct-test.md");

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

            // Act & Assert 일부러 플레이스 홀더가 없는 파일 경로로 설정
            var no_placeholder_filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/wrong-test.md");
            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(_markdown, no_placeholder_filepath));
        }

        [Fact]
        public async Task Given_ValidInput_When_Invoke_SaveAsync_Then_It_Should_SaveMarkdownContent()
        {
            // Arrange
            var service = new ConverterService();

            // Act : about.md 파일을 읽어 test.md 파일에 저장
            string markdownContent = await File.ReadAllTextAsync(_markdown);
            await service.SaveAsync(markdownContent, _filepath);

            // Assert : filepath 경로에 저장된 파일 내용을 읽어 _markdown 내용이 포함되어 있는지 확인
            string result = await File.ReadAllTextAsync(_filepath);
            result.ShouldContain(markdownContent);
        }

        [Fact]
        public async Task Given_FileRead_Fails_When_Invoke_SaveAsync_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new ConverterService();

            // Act & Assert : 읽고자하는 파일의 경로가 잘못된 경우에 해당
            var non_existence_filepath = Path.Combine(_projectRoot, "tests/MatdaAIga.LinkConverter.Tests/files/non-existence.md"); // 존재하지 않는 파일의 경우에 대해 테스트
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
                File.SetAttributes(_filepath, FileAttributes.ReadOnly); // 파일을 읽기 전용으로 설정하여 쓰기 실패를 유도
                await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync(_markdown, _filepath));
            }
            finally
            {
                File.SetAttributes(_filepath, FileAttributes.Normal); // 파일 속성을 원래대로 복원
            }
        }
    }
}