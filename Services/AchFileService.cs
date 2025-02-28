using System.Text.Json;

namespace JsonNachaAchApi.Services
{
    public class AchFileService : IAchFileService
    {
        private static readonly List<AchFile> _achFiles = new();

        public async Task<AchFile> CreateAchFileAsync(AchFile achFile)
        {
            if (achFile == null)
                throw new ArgumentNullException(nameof(achFile));

            achFile.Id = Guid.NewGuid().ToString();
            _achFiles.Add(achFile);
            return await Task.FromResult(achFile);
        }

        public async Task<AchFile> GetAchFileAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var achFile = _achFiles.FirstOrDefault(f => f.Id == id);
            return await Task.FromResult(achFile);
        }

        public async Task<IEnumerable<AchFile>> GetAllAchFilesAsync()
        {
            return await Task.FromResult(_achFiles);
        }

        public async Task<AchFile> UpdateAchFileAsync(string id, AchFile achFile)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            if (achFile == null)
                throw new ArgumentNullException(nameof(achFile));

            var existingFile = _achFiles.FirstOrDefault(f => f.Id == id);
            if (existingFile == null)
                return null;

            var index = _achFiles.IndexOf(existingFile);
            achFile.Id = id;
            _achFiles[index] = achFile;

            return await Task.FromResult(achFile);
        }

        public async Task<bool> DeleteAchFileAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var existingFile = _achFiles.FirstOrDefault(f => f.Id == id);
            if (existingFile == null)
                return await Task.FromResult(false);

            _achFiles.Remove(existingFile);
            return await Task.FromResult(true);
        }

        public async Task<AchFile> CreateAchFileFromJsonAsync(string jsonContent)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                };

                var achFile = JsonSerializer.Deserialize<AchFile>(jsonContent, options);
                if (achFile == null)
                    throw new InvalidOperationException("Failed to deserialize JSON to ACH file");

                achFile.Id = Guid.NewGuid().ToString();
                _achFiles.Add(achFile);
                
                return await Task.FromResult(achFile);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Error parsing JSON: {ex.Message}", ex);
            }
        }
    }
} 