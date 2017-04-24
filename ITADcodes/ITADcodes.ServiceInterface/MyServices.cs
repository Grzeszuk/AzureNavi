using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ITADcodes.ServiceModel;

namespace ITADcodes.ServiceInterface
{
 
    public class RegisterService : Service
    {
        public object Any(RegisterUsername response)
        {
            return new ServiceModel.RegisterResponse(response.username,response.password,response.email);
        }

        public object Any(LoginUsername response)
        {
            return new ServiceModel.LoginResponse(response.username, response.password);
        }

        public object Any(SaveSelection response)
        {
            return new SaveResponse(response.username,response.password,response.location,response.type,response.radius,response.transport,response.foodindex,response.typeindex);
        }

        public object Any(LoadSelection response)
        {
            return new LoadResponse(response.username,response.password);
        }

        public object Any(Delete response)
        {
            return new DeleteResponse(response.username,response.password,response.deleteString);
        }

        public object Any(RegistrationConfirm response)
        {
            return new RegistrationConfirmResponse(response.username);
        }

        public object Any(PhotoLoad response)
        {
            return new PhotoResponse(response.username,response.url);
        }

        public object Any(PasswordRemember response)
        {
            return new PasswordRememberResponse(response.email);
        }

        public object Any(PasswordChange response)
        {
            return new PasswordChangeResponse(response.username,response.password);
        }

        public object Any(PasswordChanger response)
        {
            return new PasswordChangerResponse(response.username, response.password,response.key);
        }
    }
}