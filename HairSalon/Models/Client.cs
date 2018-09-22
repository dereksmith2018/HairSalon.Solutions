using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private int _employee_Id;
    private string _client;

    public Client(string name, int employeeId, int id =0)
    {
      _client = name;
      _employee_Id = employeeId;
      _id = id;
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
        return this.GetId().Equals(newClient.GetId());
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public int GetEmployeeId()
    {
      return _employee_Id;
    }
    public string GetClient()
    {
      return _client;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO client(client, employee_id) VALUES (@client, @employee_id);";
      cmd.Parameter.Add(new MySqlParameter("@client, this._client"));
      cmd.Parameter.Add(new MySqlParameter("@employee_id", this._employee_Id));
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientName = rdr.GetString(1);
        int EmployeeId = rdr.GetInt32(2);
        Client newClient = new Client(ClientId, ClientName, EmployeeId);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }


  }
}
