using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccess;

namespace Buisness
{
    public class clsTimesheet
    {
        public int Id { get; set; }
        // add if null check for taskid if it null pass null
        public int? taskid { get; set; } // make it nullable

        public DateTime fdate { get; set; }
        public DateTime tdate { get; set; }
        public string notes { get; set; }
        // make constructor
        public clsTimesheet(int id, int? Taskid, DateTime fdate, DateTime tdate, string notes)
        {
            Id = id;
            this.taskid = Taskid; // assign taskid
            this.fdate = fdate;
            this.tdate = tdate;
            this.notes = notes;
        }

        static public clsTimesheet AddTimesheet(clsTimesheet AddTimesheet)
        {
            clsTimesheetData data = new clsTimesheetData(
                AddTimesheet.Id,
                AddTimesheet.taskid,
                AddTimesheet.fdate,
                AddTimesheet.tdate,
                AddTimesheet.notes
            );
            List<clsTimesheetData> result = clsTimesheetData.AddTimesheet(data);
            if (result.Count > 0)
            {
                return clsTimesheetMapper.MapToBusiness(result[0]);
            }
            else
            {
                return null;
            }
        }

        // Replace the return type of GetTimesheetByTaskId from List<clsTimesheetMapper> to List<clsTimesheet>
        static public List<clsTimesheet> GetTimesheetByTaskId(int id)
        {
            var dataObj = clsTimesheetData.GetTimesheetByTaskId(id);

            if (dataObj == null) return null;

            // Map the data objects to business objects
            // using LINQ to map the data objects to business objects
            // and return a list of clsTimesheet

            return dataObj.Select(data => clsTimesheetMapper.MapToBusiness(data)).ToList();
        }

        // GetAllTimesheets
        static public List<clsTimesheet> GetAllTimesheets()
        {
            var dataObj = clsTimesheetData.GetAllTimesheets();
            if (dataObj == null) return null;
            // Map the data objects to business objects
            // using LINQ to map the data objects to business objects
            // and return a list of clsTimesheet
            return dataObj.Select(data => clsTimesheetMapper.MapToBusiness(data)).ToList();
        }

        // Replace the return statement in DeleteTimesheet to match the void return type of clsTimesheetData.DeleteTimesheet
        static public bool DeleteTimesheet(int id)
        {
            var datatodelat =  clsTimesheetData.DeleteTimesheet(id);
            return datatodelat;

        }

        //UpdateTimesheet
        static public void UpdateTimesheet(clsTimesheet timesheet)
        {
            clsTimesheetData.UpdateTimesheet(new clsTimesheetData(
                timesheet.Id,
                timesheet.taskid,
                timesheet.fdate,
                timesheet.tdate,
                timesheet.notes
            ));

        }



        // map to clsTimesheetData
        public static class clsTimesheetMapper
        {
            public static clsTimesheet MapToBusiness(clsTimesheetData data)
            {
                return new clsTimesheet(
                    data.Id,
                    // Check if taskid is not null before converting
                    data.taskid.HasValue ? (int?)data.taskid.Value : null,
                    
                    data.fdate,
                    data.tdate,
                    data.notes
                );
            }
        }
    }
}
