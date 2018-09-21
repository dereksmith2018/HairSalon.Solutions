using System;
using MySql.Data,MySqlClient;
using Collection.Generic;

namespace HairSalon.Models
{
  public class Customer
  {
    private int _id;
    private int _employee_Id;
    private string _customer;

    public Customer(string name, int employeeId, int id =0)
    {
      _customer = name;
      _employee_Id = employeeId;
      _id = id;
    }
    public overide bool Equals(System.Object otherCustomer)
    {
      if (!(otherCustomer is Customer))
      {
        return false;
      }
      else
      {
        Customer newCustomer = (Customer)otherCustomer;
        return this.GetId().Equals(newCustomer.GetId());
      }
    }
    public override int GetHaseCode()
    {
      return this.GetId().GetHaseCode();
    }
    public int GetId()
    {
      return _id;
    }
    public int GetEmployeeId()
    {
      return _employee_Id;
    }
    public string GetCustomer()
    {
      return _customer;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO customers(customer, employee_id) VALUES (@customer, @employee_id);";
      cmd.Parameter.Add(new MySqlParameter("@customer, this._customer"));
      cmd.Parameter.Add(new MySqlParameter("@employee_id", this._employee_Id));
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
      List<Customer> allCustomers = new List<Customer> {};
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
        Customer newCustomer = new Customer(CustomerId, CustomerName, EmployeeId);
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
