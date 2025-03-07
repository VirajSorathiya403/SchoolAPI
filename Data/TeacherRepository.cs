using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Models;

namespace SchoolAPI.Data;

public class TeacherRepository
{
    private readonly IConfiguration _configuration;
 
    public TeacherRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    #region GetAllTeacher
    public List<TeacherModel> GetAllTeachers()
    {
        string connectionstr = _configuration.GetConnectionString("ConnectionString");
        var teachers = new List<TeacherModel>();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Teachers_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    teachers.Add(new TeacherModel
                    {
                        TeacherId = Convert.ToInt32(reader["TeacherId"]),
                        TeacherName = reader["TeacherName"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Email = reader["Email"].ToString(),
                        SchoolId = Convert.ToInt32(reader["SchoolId"]),
                        SchoolName = reader["SchoolName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        ExperienceYears = Convert.ToInt32(reader["ExperienceYears"]),
                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                        Gender = reader["Gender"].ToString()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception("Error fetching teachers: " + ex.Message);
        }

        return teachers;
    }
    #endregion
    
    #region GetTeacherById
    public TeacherModel SelectByPk(int TeacherId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        TeacherModel teacher = null;
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Teachers_SelectByPK", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@TeacherId", TeacherId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                teacher = new TeacherModel
                {
                    TeacherId = Convert.ToInt32(reader["TeacherId"]),
                    TeacherName = reader["TeacherName"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    SchoolId = Convert.ToInt32(reader["SchoolId"]),
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                    Salary = Convert.ToDecimal(reader["Salary"]),
                    ExperienceYears = Convert.ToInt32(reader["ExperienceYears"]),
                    JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                    Gender = reader["Gender"].ToString()
                };
            }
        }
        return teacher;
    }
    #endregion
    
    #region InsertTeacher
    public bool Insert(TeacherModel teacher)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Teachers_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@TeacherName", teacher.TeacherName);
            cmd.Parameters.AddWithValue("@MobileNo", teacher.MobileNo);
            cmd.Parameters.AddWithValue("@Email", teacher.Email);
            cmd.Parameters.AddWithValue("@SchoolId", teacher.SchoolId);
            cmd.Parameters.AddWithValue("@DateOfBirth", teacher.DateOfBirth);
            cmd.Parameters.AddWithValue("@Salary", teacher.Salary);
            cmd.Parameters.AddWithValue("@ExperienceYears", teacher.ExperienceYears);
            cmd.Parameters.AddWithValue("@Gender", teacher.Gender);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region UpdateTeacher
    public bool Update(TeacherModel teacher)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Teachers_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
            cmd.Parameters.AddWithValue("@TeacherName", teacher.TeacherName);
            cmd.Parameters.AddWithValue("@MobileNo", teacher.MobileNo);
            cmd.Parameters.AddWithValue("@Email", teacher.Email);
            cmd.Parameters.AddWithValue("@SchoolId", teacher.SchoolId);
            cmd.Parameters.AddWithValue("@DateOfBirth", teacher.DateOfBirth);
            cmd.Parameters.AddWithValue("@Salary", teacher.Salary);
            cmd.Parameters.AddWithValue("@ExperienceYears", teacher.ExperienceYears);
            cmd.Parameters.AddWithValue("@Gender", teacher.Gender);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DeleteTeacher
    public bool Delete(int teacherId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Teachers_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@TeacherId", teacherId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DropDown For School
    public IEnumerable<SchoolDropDownModel> GetSchools()
    {
        var teachers = new List<SchoolDropDownModel>();
            
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            SqlCommand cmd = new SqlCommand("PR_Schools_DropDown", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
                
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                teachers.Add(new SchoolDropDownModel
                {
                    SchoolId = Convert.ToInt32(reader["SchoolId"]),
                    SchoolName = reader["SchoolName"].ToString()
                });
            }
        }

        return teachers;
    }
    #endregion
}