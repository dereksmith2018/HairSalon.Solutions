using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Employee
  {
    private int _id;
    private string _name;
    private int _customer_Id;

    public Employee(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
    public override bool Equals(System.Object otherEmployee)
    {
      if (!(otherEmployee is Employee))
      {
        return false;
      }
      else
      {
        Employee newEmployee = (Employee)otherEmployee;
        bool idEquality = this.GetId() == newEmployee.GetId();
        bool nameEquality = this.GetName() == newEmployee.GetName();
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylist(name) VALUES (@stylist_name);";
      cmd.Parameter.Add(new MySqlParameter("@stylistName, this._name"));
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Employee> GetAll()
    {
      List<Employee> allEmployee = new List<Employee> {};
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int employeeId = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Employee newEmployee = new Employee(name, employeeId);
        allEmployee.Add(newEmployee);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allEmployee;
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
      List<Client> allClients = new List<Client> { };
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
        int clientEmployeeId = rdr.GetInt32(2);
        Client myClient = new Client(client,id,clientEmployeeId);
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
      cmd.CommandText = @"DELETE FROM EMPLOYEES;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
