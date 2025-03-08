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

            var content = await File.ReadAllTextAsync(filepath);
            
            var section = content.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries)
                                .Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                                .Select(p => p.Trim())
                                .ToList();

            if (section.Count != 2)
            {
                throw new InvalidOperationException("The given file is not properly formatted");
            }

            section.Insert(1, markdown);

            var mergedSection = string.Join("\n\n<!-- {{ LINKS }} -->\n\n", section);

            await File.WriteAllTextAsync(filepath, mergedSection);
        }
    }
}