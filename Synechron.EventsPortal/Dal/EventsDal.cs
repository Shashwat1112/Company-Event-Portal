using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Synechron.EventsPortal.Models;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Synechron.EventsPortal.Dal
{
    public class EventsDal
    {
        public List<Event> GetAllEvents() 
        {
            using (MySqlConnection CN = new MySqlConnection(ConfigurationManager.ConnectionStrings["SynechronEventsConStr"].ConnectionString))
            {
                using (MySqlCommand CMD = new MySqlCommand())
                {
                    CN.Open();
                    CMD.Connection = CN;
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.CommandText = "ShowAllEvents";
                    MySqlDataReader DR = CMD.ExecuteReader();
                    List<Event> events = new List<Event>();
                    while (DR.Read())
                    {
                        events.Add(new Event()
                        {
                            EventId = Convert.ToInt32(DR["EventId"]),
                            EventCode = Convert.ToString(DR["EventCode"]),
                            EventName = Convert.ToString(DR["EventName"]),
                            Description = Convert.ToString(DR["Description"]),
                            StartDate = Convert.ToDateTime(DR["StartDate"]),
                            EndDate = Convert.ToDateTime(DR["EndDate"]),
                            Fees = Convert.ToDecimal(DR["Fees"]),
                            TotalSeatsFilled = Convert.ToInt32(DR["TotalSeatsFilled"]),
                            Logo = Convert.ToString(DR["Logo"]),
                        });
                    }
                    DR.Close();
                    CN.Close();
                    return events;
                }
            }
        }

        public Event GetEventDetails(int eventId)
        {
            using (MySqlConnection CN = new MySqlConnection(ConfigurationManager.ConnectionStrings["SynechronEventsConStr"].ConnectionString))
            {
                using (MySqlCommand CMD = new MySqlCommand())
                {
                    CN.Open();
                    CMD.Connection = CN;
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.CommandText = "ShowEventDetails";
                    CMD.Parameters.AddWithValue("p_EventId", eventId);
                    MySqlDataReader DR = CMD.ExecuteReader();
                    DR.Read();
                    Event events = new Event()
                    {
                        EventId = Convert.ToInt32(DR["EventId"]),
                        EventCode = Convert.ToString(DR["EventCode"]),
                        EventName = Convert.ToString(DR["EventName"]),
                        Description = Convert.ToString(DR["Description"]),
                        StartDate = Convert.ToDateTime(DR["StartDate"]),
                        EndDate = Convert.ToDateTime(DR["EndDate"]),
                        Fees = Convert.ToDecimal(DR["Fees"]),
                        TotalSeatsFilled = Convert.ToInt32(DR["TotalSeatsFilled"]),
                        Logo = Convert.ToString(DR["Logo"])
                    };

                    DR.Close();
                    CN.Close();
                    return events;
                }
            }
        }
        public int InsertEvent(Event events)
        {
            using (MySqlConnection CN = new MySqlConnection(ConfigurationManager.ConnectionStrings["SynechronEventsConStr"].ConnectionString))
            {
                using (MySqlCommand CMD = new MySqlCommand())
                {
                    CN.Open();
                    CMD.Connection = CN;
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.CommandText = "InsertEvent";
                    CMD.Parameters.AddWithValue("p_EventId", events.EventId);
                    CMD.Parameters.AddWithValue("p_EventCode", events.EventCode);
                    CMD.Parameters.AddWithValue("p_EventName", events.EventName);
                    CMD.Parameters.AddWithValue("p_Description", events.Description);
                    CMD.Parameters.AddWithValue("p_StartDate", events.StartDate);
                    CMD.Parameters.AddWithValue("p_EndDate", events.EndDate);
                    CMD.Parameters.AddWithValue("p_Fees", events.Fees);
                    CMD.Parameters.AddWithValue("p_TotalSeatsFilled", events.TotalSeatsFilled);
                    CMD.Parameters.AddWithValue("p_Logo", events.Logo);
                    int result = CMD.ExecuteNonQuery();
                    CN.Close();
                    return result;
                }
            }
        }
    }
}