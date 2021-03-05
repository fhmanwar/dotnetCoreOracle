using API.Contexts;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepo
    {
        private readonly MyContext _context;
        private readonly string _connectionString;

        public UserRepo(IConfiguration configuration, MyContext myContext)
        {
            _context = myContext;
            _connectionString = configuration.GetConnectionString("myConn");
        }

        public async Task<IEnumerable<GetUserVM>> getAll()
        {
            List<GetUserVM> list = new List<GetUserVM>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = "select * from users Order by userId";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var user = new GetUserVM()
                            {
                                Id = Convert.ToInt32(reader["userId"]),
                                Email = reader["Email"].ToString(),
                                Password = reader.GetString(2),
                            };
                            list.Add(user);
                        }
                        reader.Dispose();
                        return list.AsEnumerable();
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
        }

        public GetUserVM getId(int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select * from users where userId = :id";

                        // Assign id to the department number 50 
                        OracleParameter userId = new OracleParameter("id", id);
                        cmd.Parameters.Add(userId);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        var user = new GetUserVM();
                        while (reader.Read())
                        {
                            user = new GetUserVM()
                            {
                                Id = Convert.ToInt32(reader["userId"]),
                                Email = reader["Email"].ToString(),
                                Password = reader.GetString(2),
                            };
                        }
                        reader.Dispose();
                        return user;
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
        }

        public bool Create(GetUserVM userVM)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = "insert into users(Email, Password) Values (:mail, :pass)";

                        OracleParameter mail = new OracleParameter("mail", userVM.Email);
                        OracleParameter pass = new OracleParameter("pass", userVM.Password);
                        cmd.Parameters.Add(mail);
                        cmd.Parameters.Add(pass);
                        var create = cmd.ExecuteNonQuery(); //result is int
                        return true;
                    }
                    catch 
                    {
                        return false;
                    }
                }
            }
        }

        public bool Update(GetUserVM userVM, int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = "update users set Email='"+ userVM.Email + "', Password='"+ userVM.Password + "' where UserId='"+id+"'";

                        //OracleParameter userId = new OracleParameter("id", id);
                        //OracleParameter mail = new OracleParameter("mail", userVM.Email);
                        //OracleParameter pass = new OracleParameter("pass", userVM.Password);
                        //cmd.Parameters.Add(userId);
                        //cmd.Parameters.Add(mail);
                        //cmd.Parameters.Add(pass);
                        var update = cmd.ExecuteNonQuery(); //result is int
                        return true;
                    }
                    catch 
                    {
                        return false;
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = "Delete from users where UserId='" + id + "'";
                        var delete = cmd.ExecuteNonQuery(); //result is int
                        return true;
                    }
                    catch 
                    {
                        return false;
                    }
                }
            }
        }

    }
}
