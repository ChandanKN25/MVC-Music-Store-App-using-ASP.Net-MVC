using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // private string connectionstring;

        // GET api/values
        public List<Usersmodel> Get()
        {
            iLead2021Entities1 ilead = new iLead2021Entities1();
            var results = from users in ilead.TestUserTables
                          select users;
            List<Usersmodel> iUsers = new List<Usersmodel>();

            foreach (var userObj in results)
            {

                iUsers.Add(new Usersmodel { user = userObj.username, password = userObj.password, designtion = userObj.designation });
            }

            return iUsers;


            /*    List<Usersmodel> usersmodel = new List<Usersmodel>();
              // connectionstring = " Data Source=306V4B3;Initial Catalog=iLead2021;Integrated Security=True";
              connectionstring = "Data Source=IN-306V4B3;Initial Catalog=iLead2021;User ID=sa; password=sa";
              //connectionstring = "Data Source=IN-306V4B3;Initial Catalog=iLead2021;Persist Security Info=True;User ID=sa;Password=***********";
              SqlConnection conn = new SqlConnection(connectionstring);


              SqlCommand Command;
              SqlDataReader DataReader;
              string sqlstring = "select * from iusers";
              //string dbuser = string.Empty;
              //string dbPass = string.Empty;
              conn.Open();

             Command = new SqlCommand(sqlstring,conn);
              DataReader = Command.ExecuteReader();

              while (DataReader.Read())
              {
                  //dbuser = DataReader.GetString(0);
                  //dbPass = DataReader.GetString(1);
                  usersmodel.Add(new Usersmodel { user = DataReader.GetString(0), password = DataReader.GetString(1) });

              }



              return usersmodel;
              //return new string[] { dbuser, dbPass };  */
        }

        // GET api/values/5


        // POST api/values



        public IHttpActionResult Post(Usersmodel usersmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a Valid Model");
            iLead2021Entities1 ilead = new iLead2021Entities1();
            var existingUser = ilead.TestUserTables.Where(u => u.username == usersmodel.user).FirstOrDefault();

            if (existingUser != null)
                return BadRequest("User already exist!");

            ilead.TestUserTables.Add(new TestUserTable { username = usersmodel.user, password = usersmodel.password, designation = usersmodel.designtion });
            ilead.SaveChanges();

            return Ok("User successfully created");




        }
        public Usersmodel Get(string username)
        {
            iLead2021Entities1 ilead = new iLead2021Entities1();
            var results = from users in ilead.TestUserTables
                          where users.username == username
                          select users;
            Usersmodel iUser = new Usersmodel();

            foreach (var userObj in results)
            {
                iUser.user = userObj.username;
                iUser.password = userObj.password;
                iUser.designtion = userObj.designation;
            }
            return iUser;
            /*  Usersmodel usersmodels = new Usersmodel();

                connectionstring = "Data Source=IN-306V4B3;Initial Catalog=iLead2021;User ID=sa; password=sa";
              SqlConnection connection = new SqlConnection(connectionstring);
              connection.Open();

              SqlCommand Command;
              SqlDataReader DataReader;

              string sqlText = "";

              sqlText = "select * from iusers where username='" + id + "'";
              Command = new SqlCommand(sqlText, connection);
              DataReader = Command.ExecuteReader();

              while (DataReader.Read())
              {
                  //dbuser = sqlDataReader.GetString(0);
                  //dbpass = sqlDataReader.GetString(1);
                  usersmodels.user = DataReader.GetString(0);
                  usersmodels.password = DataReader.GetString(1);
              }
              return usersmodels;*/
        }
        public IHttpActionResult Delete(string username)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a Valid Model");
            iLead2021Entities1 ilead = new iLead2021Entities1();
            var existingUser = ilead.TestUserTables.Where(u => u.username == username).FirstOrDefault();
            if (existingUser != null)
            {
                ilead.TestUserTables.Remove(existingUser);
                ilead.SaveChanges();

            }
            return Ok();
        }

        // PUT api/values/5
        public IHttpActionResult Put(Usersmodel usersmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new iLead2021Entities1())
            {
                var existingUser = ctx.TestUserTables.Where(u => u.username == usersmodel.user).FirstOrDefault();

                if (existingUser != null)
                {
                    // existingUser.username = usersmodel.user;
                    if (!string.IsNullOrEmpty(usersmodel.password))
                        existingUser.password = usersmodel.password;

                    // if (!string.IsNullOrEmpty(usersmodel.designtion))
                    // existingUser.designation = usersmodel.designtion;





                    ctx.SaveChanges();


                }

                else
                {
                    return NotFound();

                }

            }

            return Ok("password successfully changed");


        }

        // DELETE api/values/5
        // DELETE api/values/5
        //public IHttpActionResult Delete(string username)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Not a valid data");

        //    using (var ctx = new ilead2021Entities())
        //    {
        //        var existingUser = ctx.iusers.Where(u => u.username == username).FirstOrDefault();

        //        if (existingUser != null)
        //        {
        //            ctx.iusers.Remove(existingUser);
        //            ctx.SaveChanges();
        //        }
        //        else
        //        {
        //            return NotFound(); // User not found!
        //                               // return NotFound();
        //        }
        //    }
        //    return Ok("User deleted successfully...");

        //}
    }
}
