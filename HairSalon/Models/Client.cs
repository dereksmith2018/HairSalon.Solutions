using System;
using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private sint _stylistId;
    // private DateTime _appointmentDate;
    public Client(string name, int stylistId, int id = 0)
    {
      _id = id;
      _name = name;
      _stylistId = stylistId;
      // _appointmentDate = appointmentDate;
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
        bool stylistIdEquality = (this._stylistId == newClient.GetStylist());
        // bool dateEquality = (this._appointmentDate == newClient.GetAppointmentDate());
        return (nameEquality && stylistIdEquality);
      }
    }
    public override int GetHashCode()
    {
      string combinedHash = this.GetName() + this.GetStylistId();
      return combinedHash.GetHashCode();
      // return this._name.GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    // public DateTime GetAppointmentDate()
    // {
    //   return _appointmentDate;
    // }
    public void Save()//
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@newName, @newStylist);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@newName";
      newName.Value = this._name;
      cmd.Parameters.Add(newName);
      MySqlParameter newStylist = new MySqlParameter();
      newStylistId.ParameterName = "@newStylist";
      newStylistId.Value = this._stylistId;
      cmd.Parameters.Add(newStylist);
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
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistClientId = rdr.GetInt32(2);
        // DateTime appointmentDate = rdr.GetDateTime(2);
        Client newClient = new Client(cleintName, stylistClientId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    public static Client Find(int id)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchid;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int newClientId = 0;
      string newClientName = "";
      int newStylistClient = 0;
      // cmd.Parameters.AddWithValue("@searchid", id);
      // MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      // int newClientId = 0;
      // string newClientName = "";
      // DateTime newAppointmentDate = new DateTime (2017,01,01);
      // Client enterClient = new Client("", DateTime.Now, id);
      while (rdr.Read())
      {
        newClientId = rdr.GetInt32(0);
        newClientName = rdr.GetString(1);
        newStylistClient = rdr.GetIdInt32(2);
        // newAppointmentDate = rdr.GetDateTime(2);
      }
      Client findClient = new Client (newClientName, newStylistClient, newClientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return findClient;
    }
    public void EDIT(string newName, int newStylist)//DateTime newDate
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName, stylist_id= @newStylist WHERE id = @searchId;";
      // cmd.Parameters.AddWithValue("@newName", newName);
      // cmd.Parameters.AddWithValue("newDate", newDate);
      // cmd.Parameters.AddWithValue("searchid", _id);
    MySqlParameter searchId = new MySqlParameter();
    searchId.ParameterName = "@searchId";
    searchId.Value = _id;
    cmd.Parameters.Add(searchId);
    MySqlParameter clientName = new MySqlParameter();
    clientName.ParameterName = "@newName";
    clientName.Value = newName;
    cmd.Parameters.Add(clientName);
    MySqlParameter stylistClient = new MySqlParameter();
    stylistClient.ParameterName = "@newStylist";
    stylistClient.Value = newStylist;
    cmd.Parameters.Add(stylistClient);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public Stylist FindStylist()
    {
      MySqlConnection conn =  DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _stylistId;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int stylistId = 0;
      string stylistName = "";
      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }
      Stylist newStylist = new Stylist(stylistName, stylistId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }
    public static void Delete(int id)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @clientId";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@clientId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      // cmd.CommandText = @"DELETE FROM clients WHERE id = @id;";
      // cmd.Parameters.AddWithValue("@id", id);
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
      cmd.CommandText = @"TRUNCATE TABLE clients;";
      cmd.ExecuteNonQuery();
      // cmd.CommandText = @"DELETE FROM stylist_client;";
      // cmd.ExecuteNonQuery();
      // cmd.CommandText = @"DELETE FROM stylists;";
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
