using System.Data;
using Microsoft.Data.SqlClient;
using SchoolAPI.Models;

namespace SchoolAPI.Data;

public class SubjectRepository
{
    private readonly IConfiguration _configuration;
 
    public SubjectRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    #region GetAllSubjects
    public List<SubjectModel> GetAllSubjects()
    {
        string connectionstr = _configuration.GetConnectionString("ConnectionString");
        var subjects = new List<SubjectModel>();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Subjects_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    subjects.Add(new SubjectModel
                    {
                        SubjectId = Convert.ToInt32(reader["SubjectId"]),
                        SubjectName = reader["SubjectName"].ToString(),
                        SubjectCode = reader["SubjectCode"].ToString(),
                        Description = reader["Description"].ToString()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception("Error fetching subjects: " + ex.Message);
        }

        return subjects;
    }
    #endregion
    
    #region GetSubjectById
    public SubjectModel SelectByPk(int SubjectId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        SubjectModel subject = null;
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Subjects_SelectByPK", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                subject = new SubjectModel
                {
                    SubjectId = Convert.ToInt32(reader["SubjectId"]),
                    SubjectName = reader["SubjectName"].ToString(),
                    SubjectCode = reader["SubjectCode"].ToString(),
                    Description = reader["Description"].ToString()
                };
            }
        }
        return subject;
    }
    #endregion
    
    #region InsertSubject
    public bool Insert(SubjectModel subject)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Subjects_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
            cmd.Parameters.AddWithValue("@SubjectCode", subject.SubjectCode);
            cmd.Parameters.AddWithValue("@Description", subject.Description);
            
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region UpdateSubject
    public bool Update(SubjectModel subject)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Subjects_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectId", subject.SubjectId);
            cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
            cmd.Parameters.AddWithValue("@SubjectCode", subject.SubjectCode);
            cmd.Parameters.AddWithValue("@Description", subject.Description);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
    
    #region DeleteSubject
    public bool Delete(int subjectId)
    {
        string connectionstr = this._configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionstr))
        {
            SqlCommand cmd = new SqlCommand("PR_Subjects_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@SubjectId", subjectId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion
}