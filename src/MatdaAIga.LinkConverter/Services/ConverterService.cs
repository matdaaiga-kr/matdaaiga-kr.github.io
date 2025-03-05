using MatdaAIga.LinkConverter.Models;

namespace MatdaAIga.LinkConverter.Services
{
    /// <summary>
    /// This represents the service entity to convert YAML data into markdown text.
    /// </summary>
    public class ConverterService : IConverterService
    {   
        /// <inheritdoc />
        public async Task<LinkCollection> LoadAsync(string filepath)
        {
            // 구현해야함 : 임시 내용
            return await Task.FromResult(new LinkCollection());
        }

        /// <inheritdoc />
        public async Task<string> ConvertAsync(LinkCollection data)
        {
            // 구현해야함 : 임시 내용
            return await Task.FromResult(string.Empty);
        }
        
        /// <inheritdoc />
        public async Task SaveAsync(string markdown, string filepath)
        {   
            if (string.IsNullOrWhiteSpace(markdown))
            {
                throw new ArgumentNullException(nameof(markdown));
            }

            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentNullException(nameof(filepath));
            }

            // 파일 읽기 : 못 읽어오면 예외 발생
            var content = await File.ReadAllTextAsync(filepath);
            
            // 1. 텍스트를 플레이스홀더 기준으로 분할 : 공백으로만 이루어진 섹션은 제외 + trim
            var section = content.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries)
                                .Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                                .Select(p => p.Trim())
                                .ToList();

            if (section.Count != 2)
            {
                throw new InvalidOperationException("The given file is not properly formatted");
            }

            // 2. 사이에 마크다운 텍스트 삽입
            section.Insert(1, markdown);

            // 3. 섹션을 join : 플레이스홀더로 구분
            var mergedSection = string.Join("\n\n<!-- {{ LINKS }} -->\n\n", section);

            // 파일 저장 : 못 저장하면 예외 발생
            await File.WriteAllTextAsync(filepath, mergedSection);
        }
    }
}