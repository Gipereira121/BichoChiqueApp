using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using BichoChiqueApp.Models;

namespace BichoChiqueApp.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        readonly SQLiteAsyncConnection _connection;

        public SQLiteDatabaseHelpers(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Cliente>().Wait();
        }
        public Task<int> Insert(Cliente p)
        {
            return _connection.InsertAsync(p);
        }

        public Task<int> Update(Cliente p)
        {
            return _connection.ExecuteAsync("UPDATE Cliente SET nomeCliente=? WHERE idCliente=?", p.nomeCliente, p.idCliente);
        }
        public Task<int> Delete(int id)
        {
            return _connection.ExecuteAsync("DELETE FROM Cliente WHERE idCliente=?", id);
        }

        public Task<List<Cliente>> GetAll()
        {
            return _connection.Table<Cliente>().ToListAsync();
        }

        public Task<List<Cliente>> SearchCliente(string p)
        {
            string sql = "SELECT * FROM Cliente WHERE nomeCliente LIKE ?";
            return _connection.QueryAsync<Cliente>(sql, "%" + p + "%");
        }

        public Task<List<Cliente>> Search(string p)
        {
            string sql = "SELECT * FROM Cliente WHERE nomeCliente LIKE ?";
            return _connection.QueryAsync<Cliente>(sql, "%" + p + "%");
        }


    }

}
