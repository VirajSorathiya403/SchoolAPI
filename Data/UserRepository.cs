using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Models;

namespace SchoolAPI.Data;

public class UserRepository
{
    private readonly IConfiguration _configuration;
 
    public UserRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    #region GetAllUser
    public List<UserModel> GetAllUsers()
    {
        string connectionstr = _configuration.GetConnectionString("ConnectionString");
        var users = new List<UserModel>();

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Users_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new UserModel
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        // Password = reader["Password"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Email = reader["Email"].ToString(),
                        UserRoleId = Convert.ToInt32(reader["UserRoleId"]),
                        UserRoleName = reader["UserRoleName"].ToString(),
                        ContactPersonName = reader["ContactPersonName"].ToString(),
                    });
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception("Error fetching users: " + ex.Message);
        }

        return users;
    }
    #endregion
    
    #region GetUserById
    public UserModel SelectByPk(int userId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        UserModel user = null;
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Users_SelectByPK", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UserID", userId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user = new UserModel
                {
                    UserId = Convert.ToInt32(reader["UserId"]),
                    UserName = reader["UserName"].ToString(),
                    // Password = reader["Password"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    UserRoleId = Convert.ToInt32(reader["UserRoleId"]),
                    ContactPersonName = reader["ContactPersonName"].ToString(),
                };
            }
        }
        return user;
    }
    #endregion
    
    #region InsertUser
    public bool Insert(UserModel user)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Users_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@MobileNo", user.MobileNo);
            cmd.Parameters.AddWithValue("@UserRoleId", user.UserRoleId);
            cmd.Parameters.AddWithValue("@ContactPersonName", user.ContactPersonName);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region UpdateUser
    public bool Update(UserModel user)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Users_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@MobileNo", user.MobileNo);
            cmd.Parameters.AddWithValue("@UserRoleId", user.UserRoleId);
            cmd.Parameters.AddWithValue("@ContactPersonName", user.ContactPersonName);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DeleteUser
    public bool Delete(int userId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Users_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UserId", userId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DropDown For UserRoles
    public IEnumerable<UserRoleDropDownModel> GetUserRoles()
    {
        var userroles = new List<UserRoleDropDownModel>();
            
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            SqlCommand cmd = new SqlCommand("PR_UserRoles_DropDown", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
                
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                userroles.Add(new UserRoleDropDownModel
                {
                    UserRoleId = Convert.ToInt32(reader["UserRoleId"]),
                    UserRoleName = reader["UserRoleName"].ToString()
                });
            }
        }

        return userroles;
    }
    #endregion
    
    #region SignInUser
    public Dictionary<string, object> SignInUser(UserAuthModel userAuthModel)
    {
        var dictionary = new Dictionary<string, object>();
        string connectionString = _configuration.GetConnectionString("ConnectionString");

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("PR_Users_Sign_In", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", userAuthModel.UserEmail);
                    cmd.Parameters.AddWithValue("@Password", userAuthModel.Password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Default response values
                        dictionary["UserDetails"] = null;
                        dictionary["Message"] = "Invalid Email or Password";

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Check if the Message column indicates a successful login
                                string message = reader["Message"]?.ToString();
                                if (message == "Sign in Successfully")
                                {
                                    // Populate user details
                                    var user = new UserModel
                                    {
                                        UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0,
                                        UserName = reader["UserName"]?.ToString(),
                                        MobileNo = reader["MobileNo"]?.ToString(),
                                        Email = reader["Email"]?.ToString(),
                                        UserRoleId = reader["UserRoleId"] != DBNull.Value ? Convert.ToInt32(reader["UserRoleId"]) : 0,
                                        UserRoleName = reader["UserRoleName"]?.ToString(),
                                        ContactPersonName = reader["ContactPersonName"]?.ToString(),
                                    };

                                    dictionary["UserDetails"] = user;
                                    dictionary["Message"] = message;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            dictionary["Error"] = $"An error occurred: {ex.Message}";
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
        }

        return dictionary;
    }
    #endregion
        
    #region SignOutUser

        public Dictionary<string, object> SignOutUser(UserAuthModel userAuthModel)
        {
            var dictionary = new Dictionary<string, object>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("PR_Users_Sign_Out", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", userAuthModel.UserEmail);
                        cmd.Parameters.AddWithValue("@Password", userAuthModel.Password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader.HasRows && reader["Message"].ToString() != "Invalid Email or Password")
                                {
                                    dictionary["Message"] = reader["Message"].ToString();
                                }
                            }
                            else
                            {
                                dictionary["Message"] = "Invalid Email or Password";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dictionary["Error"] = "An error occurred: " + ex.Message;
            }

            return dictionary;
        }

        #endregion
}