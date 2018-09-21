using System;
using MySql.Data,MySqlClient;
using Collection.Generic;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private int _employee_Id;
    private string _customer;

    public Client(string customer, int employeeId, int Id =0)
    {
      _customer = customer;
      _employee_Id = employeeId;
      _id = Id;
    }
    public overide bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Category))
      {
        return false;
      }
      else
      {
        Category newCategory = (Category) otherClient;
        bool idEquality = this.GetId().Equals(newCategory.GetId());
        bool nameEquality = this.GetEmployeeId().Equals(newCategory.GetEmployeeId());
        return (idEquality && nameEquality)
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
      MySql
    }

  }
}
