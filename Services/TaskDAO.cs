using ByteDev.Reflection;
using magnadigi.Data;
using magnadigi.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace magnadigi.Services
{

    public class TaskDAO
    {
        public string? connectionString;
        public TaskDAO(){
            connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
        }

        /*
         * Gets all of the list items for a secific user
         * @param userIdIn a users email address 
         * @return returns a list of task items
         */
        public List<TaskModel> GetTaskItems(string userIdIn)
        {
            List<TaskModel> foundTasksList = new List<TaskModel>();
            
              
            MySqlConnection conn1 = new MySqlConnection(connectionString);
            conn1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM md.tabTask WHERE _assign LIKE @UserName", conn1);
            cmd1.Parameters.AddWithValue("@UserName", "%" + userIdIn + "%");
            MySqlDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                TaskModel groupTask = new TaskModel();
                groupTask.TaskRef = reader1.GetString(0);
                if (reader1.GetValue(7).Equals(DBNull.Value)) { } else { groupTask.Subject = reader1.GetString(7); }
                if (reader1.GetValue(8).Equals(DBNull.Value)) { groupTask.Project = ""; } else { groupTask.Project = reader1.GetString(8); }
                groupTask.Email = userIdIn;
                if (reader1.GetValue(27).Equals(DBNull.Value)) { groupTask.Details = ""; } else { groupTask.Details = reader1.GetString(27); }
                groupTask.Details.Replace("<div class=\"ql-editor read-mode\"><p>", "");//removes html tags
                groupTask.Details = Regex.Replace(groupTask.Details, "<.*?>", "<br>");//removes html tags
                groupTask.Details = Regex.Replace(groupTask.Details, "<.*?><.*?>", "<br>");//removes html tags
                groupTask.Details = Regex.Replace(groupTask.Details, "<br><br>", "<br>");//removes html tags
                groupTask.Details = Regex.Replace(groupTask.Details, "<br><br>", "<br>");//removes html tags
                if (reader1.GetValue(1).Equals(DBNull.Value)) { } else { groupTask.StartDateTime = Convert.ToDateTime(reader1.GetString(1)); }
                if (reader1.GetValue(2).Equals(DBNull.Value)) { } else { groupTask.ExpEndDate = Convert.ToDateTime(reader1.GetString(2)); }
                groupTask.Status = reader1.GetString(14); 
                groupTask.PriorityLevel = reader1.GetString(15); 
                groupTask.PriorStatus = groupTask.Status;//Indicates a status change has occured durring the update process.
                if (reader1.GetValue(24).Equals(DBNull.Value)) { } else { groupTask.Progress = reader1.GetString(24); }
                if (reader1.GetValue(19).Equals(DBNull.Value)) { } else { groupTask.CompletedOn = Convert.ToDateTime(reader1.GetString(19)); }
                foundTasksList.Add(groupTask);
            }
                //returns a task error if no records are found.           
                if (reader1.HasRows == false && foundTasksList.Count == 0)
                {
                    TaskModel groupTask2 = new TaskModel();
                    groupTask2.TaskRef = "TASK-0000";
                    groupTask2.Project = "PROJ-0000";
                    groupTask2.Email = userIdIn;
                    groupTask2.Details = "An auto generated email has been sent to support@magnadigi.com." +
                        " Either this account has not been confirmed by our staff or there are no tasks assigned to your email/user name.";
                    groupTask2.StartDateTime = DateTime.Now;
                    groupTask2.ExpEndDate = DateTime.Now;
                    groupTask2.Status = "In Progress";
                    groupTask2.PriorityLevel = "High";
                    foundTasksList.Add(groupTask2);
                    return foundTasksList;
                }
                conn1.Close();
                return foundTasksList;
        }
        public List<JsonTaskModel> GetJsonTaskItems(string userIdIn)
        {
            List<JsonTaskModel> foundTasksList = new List<JsonTaskModel>();

            MySqlConnection conn1 = new MySqlConnection(connectionString);
            conn1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM md.tabTask WHERE _assign LIKE @UserName", conn1);
            cmd1.Parameters.AddWithValue("@UserName", "%" + userIdIn + "%");
            MySqlDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                JsonTaskModel groupTask = new JsonTaskModel();
                groupTask.id = reader1.GetString(0);
                if (reader1.GetValue(7).Equals(DBNull.Value)) { } else { groupTask.name = reader1.GetString(7); }
                DateTime start = DateTime.Today;               
                if (reader1.GetValue(20).Equals(DBNull.Value)) { } else { start = Convert.ToDateTime(reader1.GetString(20)); }
                groupTask.start = start.ToString("yyyy/MM/dd");
                DateTime end = DateTime.Today;
                if (reader1.GetValue(23).Equals(DBNull.Value)) { } else { end = Convert.ToDateTime(reader1.GetString(23)); }
                groupTask.end = end.ToString("yyyy/MM/dd");
                if (reader1.GetValue(24).Equals(DBNull.Value)) { } else { groupTask.progress = reader1.GetString(24); }
                if (reader1.GetValue(28).Equals(DBNull.Value)) { } else { groupTask.dependencies = reader1.GetString(28); }
                foundTasksList.Add(groupTask);
            }
            //returns a task error if no records are found.           
            if (reader1.HasRows == false && foundTasksList.Count == 0)
            {
                JsonTaskModel groupTask2 = new JsonTaskModel();
                groupTask2.id = "TASK-0000";
                groupTask2.name = "No tasks found";
                DateTime start = DateTime.Today;
                groupTask2.start = start.ToString("yyyy/MM/dd");
                DateTime end = DateTime.Today;
                groupTask2.end = end.ToString("yyyy/MM/dd");
                groupTask2.progress = "0";
                foundTasksList.Add(groupTask2);
                return foundTasksList;
            }
            conn1.Close();
            return foundTasksList;

        }
            /*
             * Gets a single task details based on the task id. This method is needed to retrieved details without 
             * removing html tags.
             * @param taskIdIn a users TaskId 
             * @return returns a description as a string
             */
            public string GetTaskDetails(string taskRefIn)
            {
                string taskDescription = "";
                MySqlConnection conn3 = new MySqlConnection(connectionString);
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn3.Open();

                    string sql3 = "SELECT description FROM md.tabTask WHERE name=@TaskRef";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn3);
                    cmd3.Parameters.AddWithValue("@TaskRef", taskRefIn);

                    MySqlDataReader rdr = cmd3.ExecuteReader();

                    while (rdr.Read())
                    {
                        taskDescription = rdr.GetString(0); ;
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                conn3.Close();
                Console.WriteLine("Done.");


                return taskDescription;
            }
            /*
             * Gets a single task based on the task id.
             * @param taskIdIn a users TaskId 
             * @return returns a task model
             */
            public TaskModel GetTask(string taskRefIn, string userIdIn)
            {
                TaskModel returnTask = new TaskModel();

                MySqlConnection conn4 = new MySqlConnection(connectionString);
                conn4.Open();
                MySqlCommand cmd4 = new MySqlCommand("SELECT * FROM md.tabTask WHERE name=@TaskId", conn4);
                cmd4.Parameters.AddWithValue("@TaskId", taskRefIn);
                MySqlDataReader rdr4 = cmd4.ExecuteReader();

                while (rdr4.Read())
                {
                    returnTask.TaskRef = rdr4.GetString(0);
                    if (rdr4.GetValue(8).Equals(DBNull.Value)) { returnTask.Project = ""; } else { returnTask.Project = rdr4.GetString(8); }
                    returnTask.Email = userIdIn;
                    if (rdr4.GetValue(27).Equals(DBNull.Value)) { returnTask.Details = ""; } else { returnTask.Details = rdr4.GetString(27); }
                    returnTask.Details.Replace("<div class=\"ql-editor read-mode\"><p>", "");//removes html tags
                    returnTask.Details = Regex.Replace(returnTask.Details, "<.*?>", "<br>");//removes html tags
                    returnTask.Details = Regex.Replace(returnTask.Details, "<.*?><.*?>", "<br>");//removes html tags
                    returnTask.Details = Regex.Replace(returnTask.Details, "<br><br>", "<br>");//removes html tags
                    returnTask.Details = Regex.Replace(returnTask.Details, "<br><br>", "<br>");//removes html tags
                    returnTask.StartDateTime = Convert.ToDateTime(rdr4.GetString(2));
                    if (rdr4.GetValue(23).Equals(DBNull.Value)) { returnTask.ExpEndDate = DateTime.Today; } else { returnTask.ExpEndDate = Convert.ToDateTime(rdr4.GetString(23)); }
                    returnTask.Status = rdr4.GetString(14);
                    returnTask.PriorStatus = returnTask.Status;
                    returnTask.PriorityLevel = rdr4.GetString(15);
                    if (rdr4.GetValue(19).Equals(DBNull.Value)) { returnTask.CompletedOn = DateTime.Today; } else { returnTask.CompletedOn = Convert.ToDateTime(rdr4.GetString(19)); }

            }
                conn4.Close();
                return returnTask;
            }
            public bool PutTask(TaskModel taskIn, UserModel userIn, string previouseDescription)
            {
                bool success = false;
                string dateErp = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
                string dateDotNet = DateTime.Now.ToString("MM-dd-yyy' :'mm':'ss");
                string modifiedBy = "On " + dateDotNet + " - " + userIn.Email + ", added: ";
                MySqlConnection conn2 = new MySqlConnection(connectionString);
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn2.Open();

                    string sql = "UPDATE md.tabTask SET description=@Details, modified=@TodayDate, modified_by=@Email, status=@Status, completed_on=@CompletedOn WHERE name=@TaskRef";

                    MySqlCommand cmd2 = new MySqlCommand(sql, conn2);
                    cmd2.Parameters.AddWithValue("@TodayDate", dateErp);
                    cmd2.Parameters.AddWithValue("@Email", userIn.Email);
                    cmd2.Parameters.AddWithValue("@Details", previouseDescription + "<br>" + modifiedBy + taskIn.Details + "<br>");
                    cmd2.Parameters.AddWithValue("@modified", dateErp);
                    cmd2.Parameters.AddWithValue("@TaskRef", taskIn.TaskRef);
                    cmd2.Parameters.AddWithValue("@Status", taskIn.Status);
                    if (!taskIn.Status.Equals(taskIn.PriorStatus)) //if status changed
                    {
                        if (taskIn.Status.Equals("Completed")) //check to see if it is now completed
                        {
                            cmd2.Parameters.AddWithValue("@CompletedOn", DateTime.Now.ToString("yyyy-MM-dd")); //gives it todays date

                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@CompletedOn", taskIn.CompletedOn);//uses the erp obtained date

                            
                        }
                    }
                    else
                    {
                    cmd2.Parameters.AddWithValue("@CompletedOn", taskIn.CompletedOn);//uses the erp obtained date change nothing
                    }

                    cmd2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                conn2.Close();
                Console.WriteLine("Done.");

                return success;
            }
        }
    }


