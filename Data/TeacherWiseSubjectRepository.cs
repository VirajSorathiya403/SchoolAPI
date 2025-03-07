using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Models;

namespace SchoolAPI.Data;

public class TeacherWiseSubjectRepository
{
    private readonly IConfiguration _configuration;
 
    public TeacherWiseSubjectRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    #region GetAllTeacherWiseSubject
    public List<TeacherWiseSubjectModel> GetAllTeacherWiseSubject()
    {
        string connectionstr = _configuration.GetConnectionString("ConnectionString");
        var teacherWiseSubjects = new List<TeacherWiseSubjectModel>();

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_TeacherWiseSubject_SelectAllWithDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    teacherWiseSubjects.Add(new TeacherWiseSubjectModel
                    {
                        TeacherWiseSubjectId = Convert.ToInt32(reader["TeacherWiseSubjectId"]),
                        TeacherName = reader["TeacherName"].ToString(),
                        SubjectName = reader["SubjectName"].ToString(),
                        AcademicYear = reader["AcademicYear"].ToString(),
                    });
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception("Error fetching teacherWiseSubjects: " + ex.Message);
        }

        return teacherWiseSubjects;
    }
    #endregion
    
    #region InsertTeacherWiseSubject
    public bool Insert(TeacherWiseSubjectModel teacherWiseSubject)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_TeacherWiseSubject_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@TeacherId", teacherWiseSubject.TeacherId);
            cmd.Parameters.AddWithValue("@SubjectId", teacherWiseSubject.SubjectId);
            cmd.Parameters.AddWithValue("@AcademicYearId", teacherWiseSubject.AcademicYearId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DeleteTeacherWiseSubject
    public bool Delete(int TeacherWiseSubjectId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_TeacherWiseSubject_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@TeacherWiseSubjectId", TeacherWiseSubjectId);

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
    
    #region DropDown For AcademicYears
    public IEnumerable<AcademicYearsDropDownModel> GetAcademicYears()
    {
        var academicYears = new List<AcademicYearsDropDownModel>();
            
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            SqlCommand cmd = new SqlCommand("PR_AcademicYear_DropDown", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
                
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                academicYears.Add(new AcademicYearsDropDownModel
                {
                    AcademicYearId = Convert.ToInt32(reader["AcademicYearId"]),
                    YearName = reader["YearName"].ToString()
                });
            }
        }

        return academicYears;
    }
    #endregion
}