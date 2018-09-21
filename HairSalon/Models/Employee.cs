using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Employee
  {
    private int _id;
    private string _employeeName;
    private int customerId;

    public Employee(string employeeName, int id = 0)
    {
      _id = id;
      _employeeName = employeeName;
    }
    public override bool; Equals(System.Onject otherEmployee)
    {
      if (!(otherEmployee is Employee))
      {
        return false;
      }
      else
      {
        Employee newEmployee = (Employee)otherEmployee;
        bool idEquality = this.GetId() == newEmployee.GetId();
        bool employeeNameEquality = this.GetEmployeeName() == newEmployee.GetEmployeeName();
        return (idEquality && employeeNameEquality)
      }
    }
    public overide int GetHashCode()
    {
      return this.GetEmployeeName().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetEmployeeName()
    {
      return _employeeName;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO employees(name) VALUES (@employee_name);";
      cmd.Parameter.Add(new MySqlParameter("@employee_name, this._employeeName"));
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
  }
}
