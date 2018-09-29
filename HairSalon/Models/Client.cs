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
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchid;";
      cmd.Parameters.AddWithValue("@searchid", id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int newClientId = 0;
      string newClientName = "";
      DateTime newAppointmentDate = new DateTime (2017,01,01);
      // Client enterClient = new Client("", DateTime.Now, id);
      while (rdr.Read())
      {
        newClientId = rdr.GetInt32(0);
        newClientName = rdr.GetString(1);
        newAppointmentDate = rdr.GetDateTime(2);
      }
      Client lookUpClient = new Client (newClientName, newAppointmentDate, newClientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return lookUpClient;
    }
    public void Update(string newName, DateTime newDate)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName, appointment_date = @newDate WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@newName", newName);
      cmd.Parameters.AddWithValue("newDate", newDate);
      cmd.Parameters.AddWithValue("searchid", _id);
      cmd.ExecuteNonQuery();
      _name = newName;
      _appointmentDate = newDate;
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
