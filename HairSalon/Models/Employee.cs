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
    public static List<Item> GetAll()
    {
      List<Employee> allCustomers = new List<Employee> {};
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customer_list;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int CustomerId = rdr.GetInt32(0);
        string CustomerName = rdr.GetString(1);
        int EmployeeId = rdr.GetInt32(2);
        Employee newCustomer = new Employee(CustomerId, CustomerName, EmployeeId);
        allCustomers.Add(newCustomer);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allCustomers;
    }
  }
}
