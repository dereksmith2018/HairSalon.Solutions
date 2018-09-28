using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int Id {get; set;}
    public string StylistName{get; set;}
    public string ClientName {get; set;}
    public Stylist(string stylistName, string clientName, int id =0)
    {
      ClientName = clientName;
      StylistName = stylistName;
      Id = id;
    }
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist)otherStylist;
        bool idEquality = (this.Id == newClient.Id);
        bool stylistEquality = (this.StylistName == newStylist.Stylist);
        bool clientNameEquality = (this.ClientName == newStylist.ClientName);
        return (idEquality && stylistEquality && clientNameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.StylistName.GetHashCode();
    }
    // public int GetId()
    // {
    //   return _id;
    // }
    // public int GetEmployeeId()
    // {
    //   return _employee_Id;
    // }
    // public string GetClient()
    // {
    //   return _client;
    // }
    public void Save()//
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists(stylist, client_name) VALUES (@stylist, @client_name);";
      cmd.Parameter.Add(new MySqlParameter("@stylist", this.StylistName));
      cmd.Parameter.Add(new MySqlParameter("@client_name", this.ClientName));
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddClient(int clientId)//
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylist_client (client_Id, stylist_id) VALUES (@clientId, @stylistId);";

      cmd.Parameters.AddWithValue("@clientId", clientId);
      cmd.Parameters.AddWithValue("@stylistId", this.Id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Stylist> GetAll()//
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string getstylistName = rdr.GetString(1);
        string getclientName = rdr.GetInt32(2);
        Stylist newStylist = new Stylist(getstylistName, getclientName, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }
    public List<Client> GetAllClients()//
    {
      List <Client> allClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylist_client.id, stylist_client.client_id, clients.name AS client_name, clients.appointment_date, stylist_client.stylist_id, stylists.name AS stylist_name FROM clients JOIN stylist_client ON stylist_client.client_id = clients.id JOIN stylists ON student_client.stylist_id = stylists.id WHERE student_client.stylist_id = @stylistId;";
      cmd.Parameter.AddWithValue("@stylistId", this.Id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int client_id = rdr.GetInt32(1);
        string client_name = rdr.GetString(2);
        DateTime client_appointment_date = rdr.GetDateTime(3);
        Client enterClient = new Client(client_name, client_appointment_date, client_id);
        allClients.Add(enterClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    public static Stylist Find(int id)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @id;";
      cmd.Parameters.AddWithValue("@id", id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      Stylist enterStylist = new Stylist("", "");

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string stylist_name = rdr.GetString(1);
        string client_name = rdr.GetString(2);
        enterStylist.Id = id;
        enterStylist.Stylist = stylist;
        enterStylist.ClientName = client_name;
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return enterStylist;
    }
    public void Update(string newName)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @stylist WHERE id=@id;";
      cmd.Parameters.AddWithValue("@stylist", newName);
      cmd.Parameters.AddWithValue("@id", this.Id);
      cmd.ExecuteNonQuery();
      this.StylistName = newName;
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
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @id;";
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();
      cmd.CommandText = @"DELETE FROM stylist_client WHERE stylist_id = @id;";
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
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylist_client;";
      cmd.ExecuteNonQuery();
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
