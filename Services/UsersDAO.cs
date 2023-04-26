using magnadigi.Models;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace magnadigi.Services
{
public class UsersDAO{

    public string? connectionString;
    public UsersDAO()
    {
        connectionString = Environment.GetEnvironmentVariable("MariaDbConnectionStringRemote");
    }
    public UserModel getUser(UserModel userIn)
    {
        UserModel returnUserModel = new();
        MySqlConnection conn = new(connectionString);
        conn.Open();
        MySqlCommand cmd = new("SELECT * FROM magnadigi.AspNetUsers WHERE username = @Username", conn);
        cmd.Parameters.AddWithValue("@Username", userIn.UserName);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            returnUserModel.Id = reader.GetString(0);
            returnUserModel.FirstName = reader.GetString(1);
            returnUserModel.LastName = reader.GetString(2);
            returnUserModel.Business = reader.GetString(3);
            returnUserModel.UserName = reader.GetString(4);
            returnUserModel.Email = reader.GetString(6);
            returnUserModel.Phone = reader.GetString(12);
             
        }
        return returnUserModel;

    }
        
}
}

