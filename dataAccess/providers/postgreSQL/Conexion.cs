using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using chessAPI.models.player;

namespace chessAPI.dataAccess.providers.postgreSQL
{
    public class Conexion
    {
        NpgsqlConnection conexion = new NpgsqlConnection();

        public NpgsqlConnection conect()
        {
            try
            {
                conexion.ConnectionString = "Server=localhost;Port=5432;Database=chessDB;User Id=postgres;Password=misDatos2023!;Pooling=true;MinPoolSize=3;MaxPoolSize=20;Max Auto Prepare=15;Enlist=false;Auto Prepare Min Usages=3";
                conexion.Open();
                Console.WriteLine("Conexion exitosa");
            }
            catch (NpgsqlException e)
            {

                throw(e);
            }
            return conexion;
        }

        public async void ExecuteNonQuery(string query)
        {
            conect();
            await using var comando = new NpgsqlCommand(query, conexion);
            await comando.ExecuteNonQueryAsync();
            conexion.Close();

        }

        public async Task<List<clsNewPlayer>>ExecuteQuery (string query)
        {
            conect();
            await using var command = new NpgsqlCommand(query, conexion);
            await using var reader = await command.ExecuteReaderAsync();
            List<clsNewPlayer> querylist = new List<clsNewPlayer>();
            while(await reader.ReadAsync())
            {
                clsNewPlayer pl = ReadPlayer(reader);
                querylist.Add(pl);    
            }

            conexion.Close();
            return querylist;
        }

       
        private static clsNewPlayer ReadPlayer(NpgsqlDataReader reader)
        {
            string _email = reader["email"] as string;

            clsNewPlayer readedplayer = new clsNewPlayer
            {
                email = _email
            };

            return readedplayer;

        }

       
    }
}