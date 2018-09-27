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
        bool appointmentDateEquality = this.AppointmentDate.Equals(newClient.AppointmentDate);
        return (idEquality && nameEquality && appointmentDateEquality);
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
    public void Save()//
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, appointment_date) VALUES (@name, @appointment_date);";
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
        enterClient.Name = rdr.GetString(1);
        enterClient.ApppointmentDate = rdr.GetDateTime(2);
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
