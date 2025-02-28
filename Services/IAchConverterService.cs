namespace JsonNachaAchApi.Services
{
    public interface IAchConverterService
    {
        Task<string> ConvertToAchAsync(AchFile achFile);
    }
} 