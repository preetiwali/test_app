using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using test_app.Models;

namespace test_app.Controllers
{
    public class myController : ApiController
    {
        [RoutePrefix("api/User")]
        public class UserController : ApiController
        {
            invoicesEntities objEntity = new invoicesEntities();

            [HttpGet]
            [Route("GetUserDetails")]
            public IQueryable<user> GetUserDetails()
            {
                try
                {
                    return objEntity.users;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [HttpGet]
            [Route("GetUserDetailsById/{userId}")]
            public IHttpActionResult GetUserById(int userId)
            {
                user objUser = new user();
                //int ID = Convert.ToInt32(userId);
                try
                {
                    objUser = objEntity.users.Find(userId);
                    if (objUser == null)
                    {
                        return NotFound();
                    }

                }
                catch (Exception)
                {
                    throw;
                }

                return Ok(objUser);
            }

            [HttpPost]
            [Route("InsertUserDetails")]
            public IHttpActionResult PostUser(user data)
            {
                string message = "";
                if (data != null)
                {

                    try
                    {
                        objEntity.users.Add(data);
                        int result = objEntity.SaveChanges();
                        if (result > 0)
                        {
                            message = "User has been sussfully added";
                        }
                        else
                        {
                            message = "failed";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                return Ok(message);
            }

            [HttpPut]
            [Route("UpdateEmployeeDetails")]
            public IHttpActionResult PutUserMaster(user user)
            {
                string message = "";
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    user objUser = new user();
                    objUser = objEntity.users.Find(user.user_id);
                    if (objUser != null)
                    {
                        objUser.user_id = user.user_id;
                        objUser.user_name = user.user_name;
                        objUser.user_email = user.user_email;
                        objUser.user_phone = user.user_phone;
                        objUser.location_id = user.location_id;
                        //objUser.PinCode = user.PinCode;

                    }

                    int result = objEntity.SaveChanges();
                    if (result > 0)
                    {
                        message = "User has been sussfully updated";
                    }
                    else
                    {
                        message = "failed";
                    }

                }
                catch (Exception)
                {
                    throw;
                }

                return Ok(message);
            }

            [HttpDelete]
            [Route("DeleteUserDetails/{id}")]
            public IHttpActionResult DeleteUserDelete(int id)
            {
                string message = "";
                user user = objEntity.users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }

                objEntity.users.Remove(user);
                int result = objEntity.SaveChanges();
                if (result > 0)
                {
                    message = "User has been sussfully deleted";
                }
                else
                {
                    message = "failed";
                }

                return Ok(message);
            }
        }
    }
}
