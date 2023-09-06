using Data.Datas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IRedisPlatformRepo _redisPlatformRepo;

        public PlatformController(IRedisPlatformRepo redisPlatformRepo)
        {
            _redisPlatformRepo = redisPlatformRepo;
        }

        [HttpPost("createplatform")]
       public IActionResult Create(Platform platform)
        {
            var result = _redisPlatformRepo.CreatePlatform(platform);
            return Ok(result);
        }
        [HttpPost("createplatformhash")]
        public IActionResult CreateHash(Platform platform)
        {
            var result = _redisPlatformRepo.CreatePlatformWithHash(platform);
            return Ok(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAllPlatform()
        {
            var result = _redisPlatformRepo.GetAllPlatformHash();
            return Ok(result);
        }
        [HttpGet("getallhash")]      
        public IActionResult GetAllPlatformHash()
        {
            var result=_redisPlatformRepo.GetAllPlatform();
            return Ok(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(string id)
        {
            var result = _redisPlatformRepo.GetPlatformById(id);
            return Ok(result);
        }
    }
}
