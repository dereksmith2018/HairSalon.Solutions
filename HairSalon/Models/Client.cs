using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    public int Id {get; set;}
    public string Name {get; set;}
    public DateTime AppointmentDate {get; set;}
    // private int _id;
    // private string _name;
    // private int _customer_Id;

    public Client(string name, int id = 0)
    {
      Id = id;
      Name = name;
      AppointmentDate = appointmentdate;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client)otherClient;
        bool idEquality = (this.Id == newClient.Id);
        bool nameEquality= (this.Name == newClient.Name);
        bool appointmentdateEquality = this.AppointmentDate.Equals(newClient.AppointmentDate);
        return (idEquality && nameEquality && appointmentdateEquality);
        // bool idEquality = this.GetId() == newClient.GetId();
        // bool nameEquality = this.GetName() == newClient.GetName();
        // return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }
    // public int GetId()
    // {
    //   return _id;
    // }
    // public string GetName()
    // {
    //   return _name;
    // }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients(name, appointment_date) VALUES (@name, @appointment_date);";
      cmd.Parameter.AddWithValue("@name", this.Name);
      cmd.Parameter.AddWithValue("@appointment_date", this.AppointmentDate);
      cmd.ExecuteNonQuery();
      this.Id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Client> GetAll()
    {
      List<Client> allClient = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        DateTime appointmentDate

        Client newClient = new Client(name, employeeId);
        allClient.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClient;
    }
    public void Edit(string updateName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE employees SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Client> GetClient()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"Select * FROM clients WHERE employee_id = @employee_id;";
      MySqlParameter employeeId = new MySqlParameter();
      employeeId.ParameterName = "@employee_id";
      employeeId.Value = this._id;
      cmd.Parameters.Add(employeeId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string client = rdr.GetString(1);
        int clientClientId = rdr.GetInt32(2);
        Client myClient = new Client(client,id,clientClientId);
        allClients.Add(myClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM employee_id;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
