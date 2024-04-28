using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD4.models;

namespace APBD4.services
{
    public class DbService : IDbService
    {
        private readonly string con = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18549;Integrated Security=True";

        public void DeleteAnimal(string idAnimal)
        {
            using (var connection = new SqlConnection(con))
            {
                SqlCommand con = new SqlCommand();
                con.Connection = connection;
                con.CommandText = "DELETE FROM Animal WHERE IdAnimal = @idAnimal";
                con.Parameters.AddWithValue("@idAnimal", idAnimal);
                connection.Open();
                con.ExecuteNonQuery();
            }
        }

        public void UpdateAnimal(string idAnimal, Animal animal)
        {
            using (var connection = new SqlConnection(con))
            {
                SqlCommand con = new SqlCommand();
                con.Connection = connection;
                con.CommandText = "UPDATE Animal SET Name = @Name ,Description = @Description, Category = @Category, Area = @Area WHERE idAnimal= @IdAnimal";
                con.Parameters.AddWithValue("@IdAnimal", idAnimal);
                con.Parameters.AddWithValue("@Name", animal.Name);
                con.Parameters.AddWithValue("@Description", animal.Description);
                con.Parameters.AddWithValue("@Category", animal.Category);
                con.Parameters.AddWithValue("@Area", animal.Area);
                connection.Open();
                con.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void AddAnimal(Animal animal)
        {
            using (var connection = new SqlConnection(con))
            {
                SqlCommand con = new SqlCommand();
                con.Connection = connection;
                con.CommandText = "INSERT INTO Animal VALUES(@Name,@Description,@Category,@Area)";
                con.Parameters.AddWithValue("@Name", animal.Name);
                con.Parameters.AddWithValue("@Description", animal.Description);
                con.Parameters.AddWithValue("@Category", animal.Category);
                con.Parameters.AddWithValue("@Area", animal.Area);
                connection.Open();
                con.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Animal> GetAnimal(string orderBy)
        {
            string orderByChecked = "";
            if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("idAnimal") || orderBy.Equals("IdAnimal"))
            {
                orderByChecked = "Name";
            }
            else
            {
                orderByChecked = orderBy;
            }
            var sql = "SELECT * FROM Animal ORDER BY " + orderByChecked;
            
            var output = new List<Animal>();
            using (var connection = new SqlConnection(con))
            {
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = sql;
                connection.Open();
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    output.Add(new Animal
                    {
                        IdAnimal = int.Parse(dr["IdAnimal"].ToString()),
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    });                   
                }
                connection.Close();
                return output;
            }
        }
    }
}
