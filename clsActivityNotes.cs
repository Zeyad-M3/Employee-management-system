using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsActivityNotes
    {
        public int  NoteId { get; set; }
        public int ActivityId { get; set; }
        public int? userid { get; set; }
        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
        // Constructor
        public clsActivityNotes(int noteId, int activityId, string note, DateTime noteDate, int? userid)
        {
            NoteId = noteId;
            ActivityId = activityId;
            Note = note;
            NoteDate = noteDate;
            this.userid = userid;
        }
        // constructor without noteId for adding new notes
        public clsActivityNotes( int activityId, string note, DateTime noteDate, int? userid)
        {
            ActivityId = activityId;
            Note = note;
            NoteDate = noteDate;
            this.userid = userid;
        }
        // Method to get all activity notes

        public static List<clsActivityNotes> GetAllActivityNotes()
        {
            return DataAccess.clsActivityNotesData.GetAllActivityNotes()
                .Select(n => new clsActivityNotes(n.noteid, n.activityid, n.note, n.notedate,n.userid)).ToList();
        }
        // Method to get an activity note by ID
        public static clsActivityNotes GetActivityNoteById(int noteId)
        {
            var data = DataAccess.clsActivityNotesData.GetActivityNoteById(noteId);
            if (data == null)
            {
                return null;
            }
            return new clsActivityNotes(data.noteid, data.activityid, data.note, data.notedate, data.userid);
        }
        // Method to add a new activity note
        public static bool AddActivityNote(clsActivityNotes activityNote)
        {
            try
            {
                DataAccess.clsActivityNotesData.AddActivityNote(new DataAccess.clsActivityNotesData(activityNote.NoteId, activityNote.ActivityId, activityNote.Note, activityNote.NoteDate, activityNote.userid));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Add Activity Note: {ex.Message}");
                return false;
            }

        }
        // Method to update an existing activity note
        public static void UpdateActivityNote(clsActivityNotes activityNote)
        {
            DataAccess.clsActivityNotesData.UpdateActivityNote(new DataAccess.clsActivityNotesData(activityNote.NoteId, activityNote.ActivityId, activityNote.Note, activityNote.NoteDate, activityNote.userid));
        }
        // Method to delete an activity note
        public static bool DeleteActivityNote(int noteId)
        {
            try
            {
                DataAccess.clsActivityNotesData.DeleteActivityNote(noteId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Delete Activity Note: {ex.Message}");
                return false;
            }

        }
    }
}
