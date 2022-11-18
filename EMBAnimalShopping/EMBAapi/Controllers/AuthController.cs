using Business.Abstract;
using Core.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMBAapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin=_authService.Login(userForLoginDto);
            //userForLoginDto da önce mail kontrol edilir,problem varsa ErrorData döner yoksa Şifreyi check eder.
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            //EĞER KOD BURAYA GELDİYSE KULLANICI LOGİN OLDU TOKEN VERELİM.
            var result = _authService.CreateAccessToken(userToLogin.Data);
            //işlem başarılı mı bakalım
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            //Önce Kayıt olma durumunda Database de kullanıccı var mı yok mu emaille kontrol edelim
            var userExist = _authService.UserExist(userForRegisterDto.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            //Eğer kullanıcı yoksa;
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);

            var result = _authService.CreateAccessToken(registerResult.Data);
            //registerResult ın datası ile access token ürettim.
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
