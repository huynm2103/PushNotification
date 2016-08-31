using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using PushNotification.Helpers;
using PushNotification.Models.DTOs;
using PushNotification.Respository;

namespace PushNotification.Controllers.ApiController
{
    public class UserApiController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [ResponseType(typeof(UserRegisterDTO))]
        [HttpPost]
        public IHttpActionResult Register(UserRegisterDTO newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.ERROR, Message = "Wrong data format", Type = VarConstants.HttpMessageType.BAD_REQUEST });
                }
                var user = UserRespository.Instance.Register(newUser);
            }
            catch (DuplicateUserNameException)
            {
                return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.ERROR, Message = "Account existed", Type = "DuplicateUserName" });
            }
            catch (DuplicateEmailException)
            {
                return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.ERROR, Message = "Email used", Type = "DuplicateEmail" });
            }
            catch (Exception)
            {
                return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.ERROR, Message = "", Type = VarConstants.HttpMessageType.BAD_REQUEST });
            }
            return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.SUCCESS, Message = "", Type = "" });
        }

        /// <summary>
        /// Function check login status
        /// </summary>
        /// <returns>user info</returns>
        [HttpGet]
        public IHttpActionResult CheckLoginStatus()
        {
            UserBasicInfoDTO currentUser;

            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return
                        Ok(new HttpMessageDTO
                        {
                            Status = VarConstants.HttpMessageType.ERROR,
                            Message = "",
                            Type = VarConstants.HttpMessageType.NOT_AUTHEN
                        });
                }
                currentUser = UserRespository.Instance.GetBasicInfo(User.Identity.Name);
            }
            catch (Exception)
            {

                return Ok(new HttpMessageDTO
                {
                    Status = VarConstants.HttpMessageType.ERROR,
                    Message = "",
                    Type = VarConstants.HttpMessageType.BAD_REQUEST
                });
            }

            return Ok(new HttpMessageDTO
            {
                Status = VarConstants.HttpMessageType.SUCCESS,
                Message = "",
                Type = "",
                Data = currentUser
            });
        }

        //[ResponseType(typeof(UserBasicInfoDTO))]
        [HttpPost]
        public IHttpActionResult PokeUser(UserBasicInfoDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.ERROR, Message = "Wrong data format", Type = VarConstants.HttpMessageType.BAD_REQUEST });
                }
                var pokeEvent = UserRespository.Instance.PokeUser(user.UserName);
            }
            catch (Exception)
            {
                return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.ERROR, Message = "", Type = VarConstants.HttpMessageType.BAD_REQUEST });
            }
            return Ok(new HttpMessageDTO { Status = VarConstants.HttpMessageType.SUCCESS, Message = "", Type = "" });
        }
    }
}