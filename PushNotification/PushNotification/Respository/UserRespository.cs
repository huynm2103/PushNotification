using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using PushNotification.Helpers;
using PushNotification.Models;
using PushNotification.Models.DTOs;

namespace PushNotification.Respository
{
    public class UserRespository : SingletonBase<UserRespository>
    {
        #region Constructor
        private UserRespository() { }
        #endregion

        public User CreateEmptyUser()
        {
            var user = new User
            {
                Username = string.Empty,
                Email = string.Empty,
                CreatedDate = DateTime.Now,
                IsActive = false,
                IsVerify = false,
                VerifyCode = string.Empty,
                Password = string.Empty,
                LastLogin = null
            };

            return user;
        }

        public User GetByUserNameOrEmail(string userNameOrEmail)
        {
            using (var db = new AppDataContext())
            {
                var user =
                    db.Users.FirstOrDefault(x => x.Username == userNameOrEmail || x.Email == userNameOrEmail);

                return user;
            }
        }

        public User GetByUserNameOrEmail(string userNameOrEmail, string password)
        {
            using (var db = new AppDataContext())
            {
                var user = db.Users.FirstOrDefault(x => ((x.Username == userNameOrEmail || x.Email == userNameOrEmail) && x.Password == password));

                return user;
            }
        }

        public User UpdateUser(User user)
        {
            using (var db = new AppDataContext())
            {
                db.Users.AddOrUpdate(user);
                //db.UserInfos.AddOrUpdate(user.UserInfo);
                db.SaveChanges();

                return GetByUserNameOrEmail(user.Email);
            }
        }

        public User Register(UserRegisterDTO user)
        {
            using (var db = new AppDataContext())
            {
                User newUser;

                if (db.Users.Any(x => x.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new DuplicateUserNameException();
                }
                else if (db.Users.Any(x => x.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new DuplicateEmailException();
                }
                else
                {
                    // Generate verify code.
                    //var verifyCode = CommonUtils.GenerateVerifyCode();

                    // Insert data.
                    newUser = CreateEmptyUser();
                    newUser.Username = user.Username;
                    newUser.Password = user.Password;
                    newUser.Email = user.Email;
                    //newUser.VerifyCode = verifyCode;
                    //newUser.UserInfo.FullName = newUser.FullName;
                    //newUser.UserInfo.ProfileImage = "avatar_default.jpg";
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // Send active link to email of user.
                    //MailHelper.Instance.SendMailActive(newUser.Email, newUser.Username, verifyCode, newUser.FullName);
                }

                return GetByUserNameOrEmail(newUser.Email); ;
            }
        }

        public UserBasicInfoDTO GetBasicInfo(string userNameOrEmail)
        {
            using (var db = new AppDataContext())
            {
                var currentUser = (from user in db.Users
                                   where user.Username.Equals(userNameOrEmail) || user.Email.Equals(userNameOrEmail)
                                   select new UserBasicInfoDTO
                                   {
                                       IsActive = user.IsActive,
                                       LoginType = user.LoginType,
                                       UserName = user.Username,
                                       Role = user.UserType
                                   }).FirstOrDefault();

                return currentUser;
            }
        }

        public UserBasicInfoDTO PokeUser(string username)
        {
            using (var db = new AppDataContext())
            {
                var currentUser = (from user in db.Users
                                   where user.Username.Equals(username)
                                   select new UserBasicInfoDTO
                                   {
                                       IsActive = user.IsActive,
                                       LoginType = user.LoginType,
                                       UserName = user.Username,
                                       Role = user.UserType
                                   }).FirstOrDefault();

                var otherUserQuery = from u in db.Users
                                    where u.Username != username
                                    select new UserBasicInfoDTO
                                    {
                                        UserId = u.UserID,
                                        Email = u.Email,
                                        UserName = u.Username
                                    };
                var userList = otherUserQuery.ToList();



                return currentUser;
            }
        }
    }
}