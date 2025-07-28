using Microsoft.AspNetCore.Mvc;
using TutorialMVC.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TutorialMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HomeApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("getMember")]
        public IActionResult getMember([FromBody] Member member)
        {
            if (member == null || string.IsNullOrEmpty(member.Name))
            {
                return BadRequest("Member object or Name is null or empty.");
            }

            string basePath = _configuration["FilePath"];
            if (string.IsNullOrEmpty(basePath))
            {
                return StatusCode(500, "File path not configured.");
            }

            string fileName = member.Name + ".txt";
            string filePath = Path.Combine(basePath, fileName);

            try
            {
                // ディレクトリが存在しない場合は作成
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                // ファイルに書き込む内容
                string content = $"ID: {member.id}\nName: {member.Name}\nRegistDate: {member.registDate}";

                System.IO.File.WriteAllText(filePath, content);

                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error writing to file: {ex.Message}");
            }
        }
    }
}
