using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Models;

namespace SchoolAPI.Data;

public class StudentAttendanceRepository
{
    private readonly IConfiguration _configuration;
 
    public StudentAttendanceRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    #region GetAllStudentAttendances
    public List<StudentAttendanceModel> GetAllStudentAttendances(DateTime? attendanceDate = null)
    {
        string connectionstr = _configuration.GetConnectionString("ConnectionString");
        var studentAttendances = new List<StudentAttendanceModel>();

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_StudentAttendance_SelectAllWithDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add the attendanceDate parameter to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@attendanceDate", SqlDbType.Date)
                {
                    Value = (object)attendanceDate ?? DBNull.Value // Pass NULL if attendanceDate is null
                });

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    studentAttendances.Add(new StudentAttendanceModel
                    {
                        AttendanceId = Convert.ToInt32(reader["AttendanceId"]),
                        TeacherId = Convert.ToInt32(reader["TeacherId"]),
                        TeacherName = reader["TeacherName"].ToString(),
                        SubjectId = Convert.ToInt32(reader["SubjectId"]),
                        SubjectName = reader["SubjectName"].ToString(),
                        StudentId = Convert.ToInt32(reader["StudentId"]),
                        StudentName = reader["StudentName"].ToString(),
                        Email = reader["Email"].ToString(),
                        AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                        Status = reader["Status"].ToString()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception("Error fetching student attendances: " + ex.Message);
        }

        return studentAttendances;
    }
    #endregion
    
    #region GetStudentAttendanceById
    public StudentAttendanceModel SelectByPk(int AttendanceId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        StudentAttendanceModel studentAttendance = null;
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_StudentAttendance_SelectByStudentId", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@AttendanceId", AttendanceId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                studentAttendance = new StudentAttendanceModel
                {
                    AttendanceId = Convert.ToInt32(reader["AttendanceId"]),
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    SubjectId = Convert.ToInt32(reader["SubjectId"]),
                    TeacherId = Convert.ToInt32(reader["TeacherId"]),
                    AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                    Status = reader["Status"].ToString()
                };
            }
        }
        return studentAttendance;
    }
    #endregion
    
    #region InsertStudentAttendance
    public bool Insert(StudentAttendanceModel studentAttendance)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_StudentAttendance_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentId", studentAttendance.@StudentId);
            cmd.Parameters.AddWithValue("@SubjectId", studentAttendance.SubjectId);
            cmd.Parameters.AddWithValue("@TeacherId", studentAttendance.TeacherId);
            cmd.Parameters.AddWithValue("@AttendanceDate", studentAttendance.AttendanceDate);
            cmd.Parameters.AddWithValue("@Status", studentAttendance.Status);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region UpdateStudentAttendance
    public bool Update(StudentAttendanceModel studentAttendance)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_StudentAttendance_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AttendanceId", studentAttendance.AttendanceId);
            cmd.Parameters.AddWithValue("@StudentId", studentAttendance.@StudentId);
            cmd.Parameters.AddWithValue("@SubjectId", studentAttendance.SubjectId);
            cmd.Parameters.AddWithValue("@TeacherId", studentAttendance.TeacherId);
            cmd.Parameters.AddWithValue("@AttendanceDate", studentAttendance.AttendanceDate);
            cmd.Parameters.AddWithValue("@Status", studentAttendance.Status);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DeleteStudentAttendance
    public bool Delete(int attendanceId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_StudentAttendance_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DropDown For Teachers
    public IEnumerable<TeachersDropDownModel> GetTeachers()
    {
        var teachers = new List<TeachersDropDownModel>();
            
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            SqlCommand cmd = new SqlCommand("PR_Teacher_DropDown", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
                
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                teachers.Add(new TeachersDropDownModel
                {
                    TeacherId = Convert.ToInt32(reader["TeacherId"]),
                    TeacherName = reader["TeacherName"].ToString()
                });
            }
        }

        return teachers;
    }
    #endregion
    
    #region DropDown For Subjects
    public IEnumerable<SubjectsDropDownModel> GetSubjects()
    {
        var subjects = new List<SubjectsDropDownModel>();
            
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            SqlCommand cmd = new SqlCommand("PR_Subject_DropDown", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
                
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                subjects.Add(new SubjectsDropDownModel
                {
                    SubjectId = Convert.ToInt32(reader["SubjectId"]),
                    SubjectName = reader["SubjectName"].ToString()
                });
            }
        }

        return subjects;
    }
    #endregion
    
    #region DropDown For Students
    public IEnumerable<StudentsDropDownModel> GetStudents()
    {
        var students = new List<StudentsDropDownModel>();
            
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            SqlCommand cmd = new SqlCommand("PR_Student_DropDown", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
                
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new StudentsDropDownModel
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentName = reader["StudentName"].ToString()
                });
            }
        }

        return students;
    }
    #endregion
}