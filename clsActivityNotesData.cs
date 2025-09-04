using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsActivityNotesData
    {
       public int noteid { get; set; }
        public int activityid { get; set; }
        public int? userid { get; set; }
        public string note { get; set; }
        public DateTime notedate { get; set; }

        // Constructor
        public clsActivityNotesData (int noteid, int activityid, string note, DateTime notedate, int? userid)
        {
            this.noteid = noteid;
            this.activityid = activityid;
            this.note = note;
            this.notedate = notedate;
            this.userid = userid;
        }
        // Method to get all activity notes
        static public List<clsActivityNotesData> GetAllActivityNotes()
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllActivityNotes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsActivityNotesData> activityNotes = new List<clsActivityNotesData>();
                while (reader.Read())
                {
                    int noteid = reader.GetInt32(0);
                    int activityid = reader.GetInt32(1);
                    int? userid = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    string note = reader.GetString(3);
                    DateTime notedate = reader.GetDateTime(4);

                    clsActivityNotesData activityNote = new clsActivityNotesData(noteid, activityid, note, notedate, userid);
                    activityNotes.Add(activityNote);
                }
                return activityNotes;
            }
        }
        // Method to get an activity note by ID
        static public clsActivityNotesData GetActivityNoteById(int noteid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetActivityNoteById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", noteid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    int activityid = reader.GetInt32(1);
                    int? userid = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    string note = reader.GetString(3);
                    DateTime notedate = reader.GetDateTime(4);
                    return new clsActivityNotesData(noteid, activityid, note, notedate, userid);
                }
                return null;
            }
        }
        // Method to add a new activity note
        static public bool AddActivityNote(clsActivityNotesData activityNote)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {   try
                {
                    SqlCommand cmd = new SqlCommand("sp_AddActivityNote", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@taskid", activityNote.noteid);
                    cmd.Parameters.AddWithValue("@ActivityId", activityNote.activityid);
                    cmd.Parameters.AddWithValue("@Note", activityNote.note);
                    cmd.Parameters.AddWithValue("@notedate", activityNote.notedate);
                    cmd.Parameters.AddWithValue("@userid", activityNote.userid.HasValue ? (object)activityNote.userid.Value : DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Add Activity Note: {ex.Message}");
                    return false;
                }

            }
        }
        // Method to update an existing activity note
        static public void UpdateActivityNote(clsActivityNotesData activityNote)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateActivityNote", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", activityNote.noteid);
                cmd.Parameters.AddWithValue("@taskid", activityNote.activityid);
                cmd.Parameters.AddWithValue("@Note", activityNote.note);
                cmd.Parameters.AddWithValue("@createddate", activityNote.notedate);
                cmd.Parameters.AddWithValue("@userid", activityNote.userid.HasValue ? (object)activityNote.userid.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // Method to delete an activity note
        static public void DeleteActivityNote(int noteid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteActivityNote", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NoteId", noteid);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
