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
                bool idEquality = this.GetId() == newSpecialty.GetId();this.GetSpecialty() == newSpecialty.GetSpecialty();
                // (this.Id == newSpecialist.Id);
                bool nameEquality = this.GetSpecialist() == newSpecialty.GetSpecialist();
                // (this.Name == newSpecialist.Name);
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            string allHash = this.GetSpecialist();
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
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialists (name) VALUES (@nameSpecialst);";
            MySqlParameter newSpecialist = new MySqlParameter();
            newSpecialist.ParameterName = "@newSpecialty";
            newSpecialist.Value = this._name;
            cmd.Parameters.Add(newSpecialist);
            // cmd.Parameters.AddWithValue("@nameSpecialst", this.Name);
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
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
            cmd.CommandText = @"SELECT * FROM specialists WHERE id=@searchId;";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
            }
            Specialist newSpecialist = new Specialist(name, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newSpecialist;
        }
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialists WHERE id = @specialistId; DELETE FROM stylists_specialists WHERE specialist_id = @specialistId;";
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
            // cmd.CommandText = @"DELETE FROM stylists_specialists;";
            // cmd.ExecuteNonQuery();
            cmd.CommandText = @"DELETE FROM specialists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Stylist> GetStylist()
        {
            List <Stylist> allStylists = new List <Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialists
            JOIN stylists_specialists ON (specialists.id = employees_specialists.specialist_id)
            JOIN stylists ON (stylists_specialists.stylist_id = stylists.id)
            WHERE specialists.id=@specialistId;";
            MySqlParameter newSpecialist = new MySqlParameter();
            newSpecialist.ParameterName = "@specialistId";
            // cmd.Parameters.AddWithValue("@id", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;


            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newName = new Stylist(name, id);
                allStylists.Add(newName);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }
        // public void Addstylist(int stylistId)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"INSERT INTO stylists_specialists (stylist_id, specialist_id) VALUES (@stylist_id, @specialist_id);";
        //     cmd.Parameters.AddWithValue("@stylsit_id", stylistId);
        //     cmd.Parameters.AddWithValue("@specialist_id", this.Id);
        //     cmd.ExecuteNonQuery();
        //
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }
        // public void Edit(string newName)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"UPDATE specialists SET name = @newName WHERE id=@id;";
        //     cmd.Parameters.AddWithValue("@newName", newName);
        //     cmd.Parameters.AddWithValue("@id", this.Id);
        //     cmd.ExecuteNonQuery();
        //     this.Name = newName;
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }
    }
}
