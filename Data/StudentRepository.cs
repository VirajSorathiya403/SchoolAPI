using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Models;

namespace SchoolAPI.Data;

public class StudentRepository
{
    private readonly IConfiguration _configuration;
 
    public StudentRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    #region GetAllStudents
    public List<StudentModel> GetAllStudents()
    {
        string connectionstr = _configuration.GetConnectionString("ConnectionString");
        var students = new List<StudentModel>();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Students_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new StudentModel
                    {
                        StudentId = Convert.ToInt32(reader["StudentId"]),
                        StudentName = reader["StudentName"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Email = reader["Email"].ToString(),
                        ParentNo = reader["ParentNo"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Gender = reader["Gender"].ToString(),
                        EnrollmentNumber = Convert.ToInt32(reader["EnrollmentNumber"])
                    });
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception("Error fetching teachers: " + ex.Message);
        }

        return students;
    }
    #endregion
    
    #region GetStudentById
    public StudentModel SelectByPk(int StudentId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        StudentModel student = null;
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Students_SelectByPK", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentId", StudentId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            { 
                student = new StudentModel
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentName = reader["StudentName"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    ParentNo = reader["ParentNo"].ToString(),
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                    Gender = reader["Gender"].ToString(),
                    EnrollmentNumber = Convert.ToInt32(reader["EnrollmentNumber"])
                };
            }
        }
        return student;
    }
    #endregion
     
    #region InsertStudent
    public bool Insert(StudentModel student)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Students_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue("@MobileNo", student.MobileNo);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@ParentNo", student.ParentNo);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@EnrollmentNumber", student.EnrollmentNumber);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region UpdateStudent
    public bool Update(StudentModel student)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Students_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
            cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue("@MobileNo", student.MobileNo);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@ParentNo", student.ParentNo);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@EnrollmentNumber", student.EnrollmentNumber);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DeleteStudent
    public bool Delete(int studentId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Students_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
}