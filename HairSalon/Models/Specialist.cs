using System;
using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Specialist
    {
        private int _id;
        private string _name;
        // public int Id { get; set; }
        // public string Name { get; set; }
        public Specialist(string name, int id = 0)
        {
            _id = id;
            _name = name;
        }
        public override bool Equals(System.Object otherSpecialist)
        {
            if (!(otherSpecialist is Specialist))
            {
                return false;
            }
            else
            {
                Specialist newSpecialist = (Specialist) otherSpecialist;
                bool idEquality = (this.Id == newSpecialist.Id);
                bool nameEquality = (this.Name == newSpecialist.Name);
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
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
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialists (name) VALUES (@name);";
            cmd.Parameters.AddWithValue("@name", this.Name);
            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Specialist> GetAll()
        {
            List <Specialist> allSpecialists = new List <Specialist>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Specialist newSpecialist = new Specialist(name, id);
                allSpecialists.Add(newSpecialist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialists;
        }
        public static Specialist Find(int searchId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialists WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", searchId);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            Specialist enterSpecialist = new Specialist("");
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                enterSpecialist.Id = id;
                enterSpecialist.Name = name;
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return enterSpecialist;
        }
        public static Specialist Find(string specialistName)
        {
           MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialists WHERE name=@name;";
            cmd.Parameters.AddWithValue("@name", specialistName);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            Specialist enterSpecialist = new Specialist("");
            if (rdr.HasRows == false)//interesting. look into more
            {
                return null;
            }
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                enterSpecialist.Id = id;
                enterSpecialist.Name = specialistName;
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return enterSpecialist;
        }
        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE specialists SET name = @newName WHERE id=@id;";
            cmd.Parameters.AddWithValue("@newName", newName);
            cmd.Parameters.AddWithValue("@id", this.Id);
            cmd.ExecuteNonQuery();
            this.Name = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialists WHERE id = @specialistid; DELETE FROM stylists_specialists WHERE specialist_id = @specialistId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@specialistId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            // cmd.Parameters.AddWithValue("@id", searchId);
            // cmd.ExecuteNonQuery();
            // cmd.CommandText = @"DELETE FROM stylists_specialists WHERE specialist_id=@id;";
            // cmd.Parameters.AddWithValue("@id", searchId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists_specialists;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"DELETE FROM specialists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Addstylist(int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialists (stylist_id, specialist_id) VALUES (@stylist_id, @specialist_id);";
            cmd.Parameters.AddWithValue("@stylsit_id", stylistId);
            cmd.Parameters.AddWithValue("@specialist_id", this.Id);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Stylist> GetStylists()
        {
            List <Stylist> allStylists = new List <Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists_specialists.id, stylists_specialists.stylist_id, stylists.name,  stylists_specialists.specialist_id, specialists.name FROM stylists JOIN stylists_specialists ON stylists.id = stylists_specialists.id JOIN specialists ON stylists_specialists.specialist_id = specialists.id WHERE stylists_specialists.id = @id";
            cmd.Parameters.AddWithValue("@id", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            Specialist enterSpecialist = new Specialist("");
            while (rdr.Read())
            {
                int id = rdr.GetInt32(1);
                string name = rdr.GetString(2);
                Stylist foundRecipe = new Stylist(name, id);
                allStylists.Add(enterSpecialist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }
    }
}
