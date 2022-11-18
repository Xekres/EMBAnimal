using Core.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        //Bu servis vasıtası ile Login ve Register işlemlerini yapıcam.
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto,string password);
        //userForRegisterDto dan Email,kullanıcıdan da password alacagım
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        //Login işlemi için zaten userForLoginDto da 2 tane işlem var Email ve password onları alacagız
        IResult UserExist(string email);
        //veri tabanımda bu kullanıcı var mı onun kontrolünü yapalım
        IDataResult<AccessToken> CreateAccessToken(User user);
        //User bilgisi gönderilmeli AccessToken üretmek için çünkü hangi kullanıcı için üretileceği bilgisi önemli
    }
}
