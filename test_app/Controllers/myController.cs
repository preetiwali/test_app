using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using test_app.Models;


namespace test_app.Controllers
{
    public class myController : ApiController
    {
        [System.Web.Http.RoutePrefix("api/User")]
        public class UserController : ApiController
        {
            invoicesEntities objEntity = new invoicesEntities();

            [System.Web.Http.HttpGet]
            [System.Web.Http.Route("GetUserDetails")]
            public ActionResult GetUserDetails()
            {
                var allUsers = new List<user>();
                try
                {
                    Console.WriteLine(objEntity.users);
                    allUsers = objEntity.users.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e.Message);
                }

                return null; /*Json(allUsers, JsonRequestBehavior.AllowGet);*/
            }

            [System.Web.Http.HttpGet]
            [System.Web.Http.Route("GetUserDetailsById/{userId}")]
            public IHttpActionResult GetUserById(int userId)
            {
                Console.WriteLine("GetUserDetailsById"); 
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

            [System.Web.Http.HttpPost]
            [System.Web.Http.Route("InsertUserDetails")]
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

            [System.Web.Http.HttpPut]
            [System.Web.Http.Route("UpdateEmployeeDetails")]
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

            [System.Web.Http.HttpDelete]
            [System.Web.Http.Route("DeleteUserDetails/{id}")]
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
