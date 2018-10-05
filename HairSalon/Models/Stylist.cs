using System;
using HairSalon;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;
    // public int Id {get; set;}
    // public string StylistName{get; set;}
    // public string ClientName {get; set;}
    // public Stylist(string stylistName, string clientName, int id =0)
    // {
    //   ClientName = clientName;
    //   StylistName = stylistName;
    //   Id = id;
    // }
    public Stylist(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        bool idEquality = this.GetId() == newStylist.GetStylistId();
        bool nameEquality = this.GetStylist() == newStylist.GetStylist();
        return (idEquality && nameEquality);
        // Stylist newStylist = (Stylist)otherStylist;
        // bool idEquality = (this.Id == newClient.Id);
        // bool stylistEquality = (this.StylistName == newStylist.Stylist);
        // bool clientNameEquality = (this.ClientName == newStylist.ClientName);
        // return (idEquality && stylistEquality && clientNameEquality);
      }
    }
    public override int GetHashCode()
    {
      string allHash = this.GetStylist();
      return allHash.GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void Save()//
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"INSERT INTO stylists (name) VALUES (@newName);";
      MySqlParameter name = new MySwlParameter();
      name.ParameterName = "@newName";
      name.Value = this.GetStylist();
      cmd.Parameter.Add(name);
      // cmd.CommandText = @"INSERT INTO stylists(stylist, client_name) VALUES (@stylist, @client_name);";
      // cmd.Parameter.Add(new MySqlParameter("@stylist", this.StylistName));
      // cmd.Parameter.Add(new MySqlParameter("@client_name", this.ClientName));
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
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
        string getName = rdr.GetString(1);
        Stylist newStylist = new Stylist(getName, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }
    public static Stylist Find(int id)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;//private
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int newId = 0;
      string newName = "";
      // cmd.Parameters.AddWithValue("@Searchid", id);
      // MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      // Stylist enterStylist = new Stylist("", "");
      while (rdr.Read())
      {
        newId = rdr.GetInt32(0);
        newName = newName.GetString(1);
        // int id = rdr.GetInt32(0);
        // string stylist_name = rdr.GetString(1);
        // string client_name = rdr.GetString(2);
        // enterStylist.Id = id;
        // enterStylist.Stylist = stylist;
        // enterStylist.ClientName = client_name;
      }
      Stylist newStylist = new Stylist(newName, newId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }
    public void Edit(string newName)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@newName";
      stylistName.Value = newName;
      cmd.Parameters.Add(stylistName);
      // cmd.CommandText = @"UPDATE stylists SET name = @stylist WHERE id=@id;";
      // cmd.Parameters.AddWithValue("@stylist", newName);
      // cmd.Parameters.AddWithValue("@id", this.Id);
      cmd.ExecuteNonQuery();
      _name = newName;
      // this.StylistName = newName;
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
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @stylistNameId; DELETE FROM stylists_specialists WHERE stylist_id = @stylistNameId; DELETE FROM stylists_clients WHERE stylist_id = @stylistNameId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@stylistNameId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      // MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      // cmd.CommandText = @"DELETE FROM stylists WHERE id = @id;";
      // cmd.Parameters.AddWithValue("@id", id);
      // cmd.ExecuteNonQuery();
      // cmd.CommandText = @"DELETE FROM stylists_clients WHERE stylist_id = @id;";
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
      cmd.CommandText = @"DELETE FROM stlyists;";
      // cmd.CommandText = @"TRUNCATE TABLE stylists; TRUNCATE TABLE stylists_specialists;";
      // MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      // cmd.CommandText = @"DELETE FROM stylist_client;";
      // cmd.ExecuteNonQuery();
      // cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    // public void AddClient(int clientId)//
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"INSERT INTO stylist_client (client_Id, stylist_id) VALUES (@clientId, @stylistId);";
    //
    //   cmd.Parameters.AddWithValue("@clientId", clientId);
    //   cmd.Parameters.AddWithValue("@stylistId", this.Id);
    //   cmd.ExecuteNonQuery();
    //   conn.Close();
    //   if(conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //}
    public void AddClient(Client newClient)
    {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO stylists_clients(stylist_id, client_id) VALUES (@stylistId, @clientId);";
    MySqlParameter stylistId = new MySqlParameter();
    stylistId.ParameterName = "@stylistId";
    stylistId.Value = _stylistId;
    cmd.Parameters.Add(stylistId);
    MySqlParameter clientId = new MySqlParameter();
    clientId.ParameterName = "clientId";
    clientId.Value = newClient.GetClientId();
    cmd.Parameters.Add(clientId);
    cmd.ExecuteNonQuery();
    conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public List<Client> GetClient()//
    {
      List <Client> allClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT clients.* FROM stylists JOIN stylists_clients ON (stylist.id = stylists_clients.stylist_id)
      JOIN clients ON (stylists_clients.client_id = clients.id)
      WHERE stylist.id = @stylistId;";
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      string clientName = "";
      // MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      // cmd.CommandText = @"SELECT stylist_client.id, stylist_client.client_id, clients.name AS client_name, clients.appointment_date, stylist_client.stylist_id, stylists.name AS stylist_name FROM clients JOIN stylist_client ON stylist_client.client_id = clients.id JOIN stylists ON student_client.stylist_id = stylists.id WHERE student_client.stylist_id = @stylistId;";
      // cmd.Parameter.AddWithValue("@stylistId", this.Id);
      // MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int clientId = rdr.GetInt32(1);
        string clientName = rdr.GetString(2);
        // int stylistClientId = rdr.GetInt32();
        // DateTime client_appointment_date = rdr.GetDateTime(3);
        Client newClient = new Client(clientName, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public void AddSpecialist(Specialist newSpecialist)//
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialists (stylist_id, specialist_id) VALUES (@stylistId, @specialistId);";
      MySqlParameter specialist_id = new MySqlParameter();
      specialist_id.ParameterName = "@specialistId";
      specialist_id.Value = newSpecialist.GetId();
      cmd.Parameters.Add(specialist_id);
      MySqlParameter StylistId = new MySqlParameter();
      StylistId.ParameterName = "@stylistId";
      StylistId.Value = _employeeId;
      cmd.Parameters.Add(StylistId);
      // cmd.Parameters.AddWithValue(@"stylistId", this._id);
      // cmd.Parameters.AddWithValue(@"specialistId", newSpecialist.GetId());
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Specialist> GetSpecialist()//
    {
      List<Specialist> allSpecialists = new List<Specialist>() {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialists.* FROM stylists JOIN stylists_specialists ON (stylists.id = stylists_specialists.stylist_id) JOIN specialists ON (stylists_specialists.specialist_id = specialists.id) WHERE stylists.id = @stylistNameId;";
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@employeesIdParameter";
      stylistId.Value = this._Id;
      cmd.Parameters.Add(stylistId);
      // cmd.Parameters.AddWithValue("@stylistNameId", this._id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int newId = rdr.GetInt32(0);
        string newName = rdr.GetString(1);
        Specialist newSpecialists = new Specialist(newName, newId);
        allSpecialists.Add(newSpecialists);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allSpecialists;
    }
  }
}
