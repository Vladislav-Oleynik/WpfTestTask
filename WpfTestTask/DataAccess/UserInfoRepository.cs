using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using WpfTestTask.Model;

namespace WpfTestTask.DataAccess
{
    public class UserInfoRepository
    {

        public void ExecuteWrite(string query, Dictionary<string, object> args)
        {
            //setup the connection to the database
            using (var con = new SQLiteConnection(@"Data Source=./UsersInfo.db;"))
            {
                con.Open();

                //open a new command
                using (var cmd = new SQLiteCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable Execute(string query, Dictionary<string, object> args)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;

            using (var con = new SQLiteConnection(@"Data Source=./UsersInfo.db;"))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query, con))
                {
                    foreach (KeyValuePair<string, object> entry in args)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }

                    var da = new SQLiteDataAdapter(cmd);

                    var dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();
                    return dt;
                }
            }
        }

        public void AddUser(UserInfo user)
        {
            const string query = "INSERT INTO usersInfo(ApplicationName, UserName, Comment) VALUES(@userAppName, @userName, @userComment)";

            var args = new Dictionary<string, object>
            {
                {"@userAppName", user.userAppName},
                {"@userName", user.userName},
                {"@userComment", user.userComment}
            };

            ExecuteWrite(query, args);
        }

        public void UpdateUser(UserInfo user)
        {
            const string query = "UPDATE usersInfo SET ApplicationName = @userAppName, UserName = @userName, Comment = @userComment WHERE Id = @userId";

            var args = new Dictionary<string, object>
            {
                {"@userId", user.userId},
                {"@userAppName", user.userAppName},
                {"@userName", user.userName},
                {"@userComment", user.userComment}
            };

            ExecuteWrite(query, args);
        }

        public void RemoveUser(UserInfo user)
        {
            const string query = "Delete from usersInfo WHERE Id = @userId";

            var args = new Dictionary<string, object>
            {
                {"@userId", user.userId}
            };

            ExecuteWrite(query, args);
        }

        public UserInfo Get(int id)
        {
            var user = new UserInfo();

            var query = "SELECT * FROM usersInfo WHERE Id = @userId";

            var args = new Dictionary<string, object>
            {
                {"@userId", id}
            };
            Console.WriteLine(args);
            DataTable dt = Execute(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            user = new UserInfo
            {
                userId = Convert.ToInt32(dt.Rows[0]["Id"]),
                userAppName = Convert.ToString(dt.Rows[0]["ApplicationName"]),
                userName = Convert.ToString(dt.Rows[0]["UserName"]),
                userComment = Convert.ToString(dt.Rows[0]["Comment"])

            };

            return user;
        }

        public List<UserInfo> GetAll()
        {
            List<UserInfo> users = new List<UserInfo>();

            var user = new UserInfo();

            var query = "SELECT * FROM usersInfo";

            var args = new Dictionary<string, object>
            {
                {"@userId", user.userId}

            };

            DataTable dt = Execute(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                user = new UserInfo
                {
                    userId = Convert.ToInt32(dt.Rows[i]["Id"]),
                    userAppName = Convert.ToString(dt.Rows[i]["ApplicationName"]),
                    userName = Convert.ToString(dt.Rows[i]["UserName"]),
                    userComment = Convert.ToString(dt.Rows[i]["Comment"])
                };

                users.Add(user);
            }
            return users;
        }

    }
}