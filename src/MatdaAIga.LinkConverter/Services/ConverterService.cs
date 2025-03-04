using MatdaAIga.LinkConverter.Models;

namespace MatdaAIga.LinkConverter.Services
{
    public class ConverterService : IConverterService
    {
        public async Task<LinkCollection> LoadAsync(string filepath)
        {
            // 구현해야함 : 임시 내용
            return await Task.FromResult(new LinkCollection());
        }

        public async Task<string> ConvertAsync(LinkCollection data)
        {
            // 구현해야함 : 임시 내용
            return await Task.FromResult(string.Empty);
        }

        public async Task SaveAsync(string markdown, string filepath)
        {   
            if (string.IsNullOrEmpty(markdown))
            {
                throw new ArgumentNullException(nameof(markdown), "Markdown text cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentNullException(nameof(filepath), "File path cannot be null or empty.");
            }

            try
            {
                // 파일 읽기 : 못 읽어오면 예외 발생
                string content = await File.ReadAllTextAsync(filepath);
                
                // 첫 번째와 두 번째 플레이스홀더의 인덱스를 찾기
                const string placeholder = "<!-- {{ LINKS }} -->";
                // 첫 번째 플레이스 홀더의 인덱스 찾기 + 찾지 못하면 예외 발생
                int firstPlaceholderIndex = content.IndexOf(placeholder);
                if (firstPlaceholderIndex == -1)
                {
                    throw new InvalidOperationException("The first placeholder for links was not found in the file.");
                }
                // 두 번째 플레이스 홀더의 인덱스 찾기 + 찾지 못하면 예외 발생
                int secondPlaceholderIndex = content.IndexOf(placeholder, firstPlaceholderIndex + placeholder.Length);
                if (secondPlaceholderIndex == -1)
                {
                    throw new InvalidOperationException("The second placeholder for links was not found in the file.");
                }

                // 플레이스홀더 사이의 내용을 markdown 텍스트로 교체
                string newContent = content.Substring(0, firstPlaceholderIndex + placeholder.Length) + "\n" + markdown + "\n" + content.Substring(secondPlaceholderIndex);

                // 파일 저장 : 못 저장하면 예외 발생
                await File.WriteAllTextAsync(filepath, newContent);
            }
            catch (Exception ex)
            {
                // 파일 읽기 또는 쓰기 중 에러 발생 시 예외 던지기
                throw new IOException("An error occurred while processing the file.", ex);
            }
        }
    }
}