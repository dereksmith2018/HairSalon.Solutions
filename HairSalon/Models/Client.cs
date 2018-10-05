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

    // private DateTime _appointmentDate;
    public Client(string name, int id = 0)
    {
      _id = id;
      _name = name;

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
        bool idEquality = this.GetId() == newClient.GetId();
        // (this._name == newClient.GetId());
        bool nameEquality = this.GetClient() == newClient()
        // (this._stylistId == newClient.GetClient());
        // bool dateEquality = (this._appointmentDate == newClient.GetAppointmentDate());
        return (nameEquality && idEquality);
      }
    }
    public override int GetHashCode()
    {
      string allHash = this.GetClient();
      return allHash.GetHashCode();
      // return this._name.GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetClient()
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
      cmd.CommandText = @"INSERT INTO clients (name) VALUES (@newName);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@newName";
      newName.Value = this._name;
      cmd.Parameters.Add(newName);
      // MySqlParameter newStylist = new MySqlParameter();
      // newStylistId.ParameterName = "@newStylist";
      // newStylistId.Value = this._stylistId;
      // cmd.Parameters.Add(newStylist);
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
        // int stylistClientId = rdr.GetInt32(2);
        // DateTime appointmentDate = rdr.GetDateTime(2);
        Client newClient = new Client(clientName, clientId);
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
      // int newStylistClient = 0;
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
        // newStylistClient = rdr.GetIdInt32(2);
        // newAppointmentDate = rdr.GetDateTime(2);
      }
      Client findClient = new Client (newClientName, newClientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return findClient;
    }
    public void Edit(string newName)//DateTime newDate
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";
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
    // MySqlParameter stylistClient = new MySqlParameter();
    // stylistClient.ParameterName = "@newStylist";
    // stylistClient.Value = newStylist;
    // cmd.Parameters.Add(stylistClient);
      cmd.ExecuteNonQuery();
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
      cmd.CommandText = @"DELETE FROM clients WHERE id = @clientId; DELETE from stylist_clients WHERE client_id = @clientId";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@clientId";
      searchId.Value = _id;
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
      cmd.CommandText = @"DELETE FROM clients;";
      // cmd.CommandText = @"TRUNCATE TABLE clients;";
      cmd.ExecuteNonQuery();
      // cmd.CommandText = @"DELETE FROM stylist_client;";
      // cmd.ExecuteNonQuery();
      // cmd.CommandText = @"DELETE FROM stylists;";
      //cmd.CommandText = @"DELETE FROM appointment_date"
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddStylist(Stylist newStylist)
    {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO stylists_clients (client_id, stylist_id) VALUES (@ClientId, @StylistId);";
       MySqlParameter clientId = new MySqlParameter();
       clientId.ParameterName = "@ClientId";
       clientId.Value = _id;
       cmd.Parameters.Add(clientId);
       MySqlParameter stylistId = new MySqlParameter();
       stylistId.ParameterName = "@stylistId";
       StylistId.Value = newStylist.GetStylistId();//llok at path
       cmd.Parameters.Add(stylistId);
       cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

    }
    public List<Stylist> GetStylist()
    {
      MySqlConnection conn =  DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM clients
      JOIN stylists_clients ON (clients.id = stylist_clients.client_id)
      JOIN stylist ON (stylist_clients.stylist_id = stylist.id)
      WHERE clients.id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _stylistId;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List <Stylist> newStylist = new List <Stylist> {};
      // int stylistId = 0;
      // string stylistName = "";
      while (rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }
  }
}
