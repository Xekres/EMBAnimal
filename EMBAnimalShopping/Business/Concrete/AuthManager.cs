using Business.Abstract;
using Business.Constants;
using Core.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using Core.Utilities.Results.SuccessOrErrorDataResults;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private ITokenHelper _tokenHelper;
        private IUserService _userService;
        public AuthManager(ITokenHelper tokenHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            //AccessToken FrontEnd kısmında kaydolan kişiye kayıt veya login olduktan sonra işlem yaparken kullanması için verilen tokendır
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck==null)
            {
                return new ErrorDataResult<User>(userToCheck,Messages.UserNotFound);
            }
            //Tam olarak bu kısımda Kullaıcının gönderdiği datayı HASH ve SALT ile kontrol etmek için HASHHELPER yazıyorum.
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {//Gönderilen şifre kayıtlı şifreye eşit değilse
                return new ErrorDataResult<User>(userToCheck,Messages.PasswordError);
            }
            //Kod 1. kısmı geçerse kullanıcı var,2. kısmı geçerse şifre doğru, E bu 2 bilgi de dogruysa demek ki :
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfullLogin);

            
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash,passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExist(string email)
        {
            if (_userService.GetByMail(email) !=null)
            {
                return new ErrorResult(Messages.UserAvailable);
            }
            return new SuccessResult();
        }
    }
}
