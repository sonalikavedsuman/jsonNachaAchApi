namespace JsonNachaAchApi.Services
{
    public interface IAchFileService
    {
        Task<AchFile> CreateAchFileAsync(AchFile achFile);
        Task<AchFile> GetAchFileAsync(string id);
        Task<IEnumerable<AchFile>> GetAllAchFilesAsync();
        Task<AchFile> UpdateAchFileAsync(string id, AchFile achFile);
        Task<bool> DeleteAchFileAsync(string id);
        Task<AchFile> CreateAchFileFromJsonAsync(string jsonContent);
    }
} 