using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private DateTime _appointmentDate;

    public Client(string name, DateTime appointmentDate, int id = 0)
    {
      _id = id;
      _name = name;
      _appointmentDate = appointmentDate;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = (this._name == newClient.GetName());
        bool dateEquality = (this._appointmentDate == newClient.GetAppointmentDate());
        return (nameEquality && dateEquality);
      }
    }
    public override int GetHashCode()
    {
      return this._name.GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public DateTime GetAppointmentDate()
    {
      return _appointmentDate;
    }
    public void Save()//
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, appointment_date) VALUES (@name, @appointment_date);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@newClientName";
      newName.Value = this._name;
      cmd.Parameters.Add(newName);
      MySqlParameter newDate = new MySqlParameter();
      newDate.ParameterName = "@newDate";
      newDate.Value = this._appointmentDate;
      cmd.Parameters.Add(newDate);
      cmd.ExecuteNonQuery();
      this._id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Client> GetAll()//
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        DateTime appointmentDate = rdr.GetDateTime(2);
        Client newClient = new Client(name, appointmentDate, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClient;
    }
    public static Client Find(int id)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @id;";
      cmd.Parameters.AddWithValue("@id", id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      Client enterClient = new Client("", DateTime.Now, id);
      while (rdr.Read())
      {
        enterClient.name = rdr.GetString(1);
        enterClient.appointmentDate = rdr.GetDateTime(2);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return enterClient;
    }
    public void Update(string newName)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newClient WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@name", newName);
      cmd.Parameters.AddWithValue("id", this.Id);
      cmd.ExecuteNonQuery();
      this.Name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void Delete(int id)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @id;";
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylist_client;";
      cmd.ExecuteNonQuery();
      cmd.CommandText = @"DELETE FROM stylists;";
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
