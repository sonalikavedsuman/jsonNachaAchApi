using Microsoft.AspNetCore.Mvc;
using JsonNachaAchApi.Services;
using System.Text.Json;
using System.Text;

namespace JsonNachaAchApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchFileController : ControllerBase
    {
        private readonly IAchFileService _achFileService;
        private readonly IAchConverterService _achConverterService;

        public AchFileController(IAchFileService achFileService, IAchConverterService achConverterService)
        {
            _achFileService = achFileService;
            _achConverterService = achConverterService;
        }

        [HttpPost]
        public async Task<ActionResult<AchFile>> CreateAchFile([FromBody] AchFile achFile)
        {
            try
            {
                var result = await _achFileService.CreateAchFileAsync(achFile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AchFile>>> GetAllAchFiles()
        {
            try
            {
                var results = await _achFileService.GetAllAchFilesAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        [HttpPost("convert")]
        public async Task<ActionResult> ConvertToAchFile([FromBody] JsonElement jsonData)
        {
            try
            {
                string jsonString;
                if (jsonData.ValueKind == JsonValueKind.Array)
                {
                    jsonString = jsonData[0].GetRawText();
                }
                else
                {
                    jsonString = jsonData.GetRawText();
                }

                var achFile = await _achFileService.CreateAchFileFromJsonAsync(jsonString);
                var achContent = await _achConverterService.ConvertToAchAsync(achFile);
               
                return File(Encoding.UTF8.GetBytes(achContent), "text/plain", "ach_file.txt");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error converting JSON to ACH file: {ex.Message}");
            }
        }
    }
} 