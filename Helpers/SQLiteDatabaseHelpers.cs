using BichoChiqueApp.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BichoChiqueApp.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelpers(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            // _conn.CreateTableAsync<BichoChiqueApp.Models.Modelo>().Wait();
            _conn.CreateTableAsync<Especies>().Wait();
            // _conn.CreateTableAsync<Veiculo>().Wait();
            // _conn.CreateTableAsync<Usuario>().Wait();
        }

        //Especies
        public Task<int> InsertEspecie(Especies p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Especies>> UpdateEspecie(Especies p)
        {
            string sql = "UPDATE Especies SET espNome=?, espObs=? WHERE espId=?";

            return _conn.QueryAsync<Especies>(sql, p.espNome, p.espObs, p.espId);
        }
        public Task<int> DeleteEspecie(int p)
        {
            return _conn.Table<Especies>().DeleteAsync(i => i.espId == p);
        }
        public Task<List<Especies>> GetAllEspecie()
        {
            return _conn.Table<Especies>().ToListAsync();
        }
        public Task<List<Especies>> SearchEspecie(string p)
        {
            string sql = "SELECT * FROM Especies WHERE espNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<Especies>(sql);
        }

    }
}
