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
                bool idEquality = this.GetSpecialistId() == newSpecialist.GetSpecialistId();
                bool nameEquality = this.GetSpecialist() == newSpecialist.GetSpecialist();
                // (this.Id == newSpecialist.Id);
                // (this.Name == newSpecialist.Name);
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            string allHash = this.GetSpecialist();
            return allHash.GetHashCode();
        }
        public int GetSpecialistId()
        {
          return _id;
        }
        public string GetSpecialist()
        {
          return _name;
        }
        public void Save()//
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialists (name) VALUES (@newSpecialist);";
            MySqlParameter newSpecialist = new MySqlParameter();
            newSpecialist.ParameterName = "@newSpecialist";
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
        public static List<Specialist> GetAll()//
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
        public static Specialist Find(int id)//
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialists WHERE id=@searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int specialistId = 0;
            string specialistName = "";
            while (rdr.Read())
            {
                 specialistId = rdr.GetInt32(0);
                 specialistName = rdr.GetString(1);
            }
            Specialist newspecialist = new Specialist(specialistName, specialistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newspecialist;
        }
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialists WHERE id = @specialistId; DELETE FROM stylists_specialists WHERE specialist_id = @specialistId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@specialistId";
            searchId.Value = _id;
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
            JOIN stylists_specialists ON (specialists.id = stylists_specialists.specialist_id)
            JOIN stylists ON (stylists_specialists.stylist_id = stylists.id)
            WHERE specialists.id=@specialistId;";
            MySqlParameter newSpecialist = new MySqlParameter();
            newSpecialist.ParameterName = "@specialistId";
            newSpecialist.Value = this._id;
            cmd.Parameters.Add(newSpecialist);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;


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
        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialists (stylist_id, specialist_id) VALUES (@stylistId, @specialistId);";
            MySqlParameter specialist_id = new MySqlParameter();
            specialist_id.ParameterName = "@specialistId";
            specialist_id.Value = _id;
            cmd.Parameters.Add(specialist_id);

            MySqlParameter Stylist_id = new MySqlParameter();
            Stylist_id.ParameterName = "@stylistId";
            Stylist_id.Value = newStylist.GetStylistId();
            cmd.Parameters.Add(Stylist_id);
            // cmd.Parameters.AddWithValue("@stylsit_id", stylistId);
            // cmd.Parameters.AddWithValue("@specialist_id", this.Id);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
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
