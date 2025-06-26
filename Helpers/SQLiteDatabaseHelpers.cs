using BichoChiqueApp.Models;
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
            _conn.CreateTableAsync<BichoChiqueApp.Models.Cliente>().Wait();
            _conn.CreateTableAsync<Especies>().Wait();
            _conn.CreateTableAsync<Animal>().Wait();
            // _conn.CreateTableAsync<Usuario>().Wait();
        }


        //Cliente
        public Task<int> InsertCliente(BichoChiqueApp.Models.Cliente p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<BichoChiqueApp.Models.Cliente>> UpdateCliente(BichoChiqueApp.Models.Cliente p)
        {
       
            string sql = "UPDATE Cliente SET cliNome=?, cliObs=?, cliCPF=?, cliEmail=?, cliDataCadastro=? WHERE cliId=?";
            return _conn.QueryAsync<BichoChiqueApp.Models.Cliente>(sql, p.cliNome, p.cliObs, p.cliCPF, p.cliEmail, p.cliDataCadastro, p.cliId);
        }


        public Task<int> DeleteCliente(int p)
        {
            return _conn.Table<BichoChiqueApp.Models.Cliente>().DeleteAsync(i => i.cliId == p);

            /* ou
            string sql = "DELETE Cliente WHERE cliId=?";
            return _conn.QueryAsync<BichoChiqueApp.Models.Cliente>(sql, p); */
        }

        public Task<List<BichoChiqueApp.Models.Cliente>> GetAllCliente()
        {
            return _conn.Table<BichoChiqueApp.Models.Cliente>().ToListAsync();
        }

        public Task<List<BichoChiqueApp.Models.Cliente>> SearchCliente(string p)
        {
            string sql = "SELECT * FROM Cliente WHERE cliNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<BichoChiqueApp.Models.Cliente>(sql);
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

        // Animal
        public Task<int> InsertAnimal(Animal p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Animal>> UpdateAnimal(Animal p)
        {
            string sql = "UPDATE Animal SET aniNome=?, aniApe=?, aniDataNasc=?, aniObs=?, codEsp=?, codCli=? WHERE aniId=?";

            return _conn.QueryAsync<Animal>(sql, p.aniNome, p.aniApe, p.aniDataNasc, p.aniObs, p.codEsp, p.codCli, p.aniId);
        }

        public Task<int> DeleteAnimal(int p)
        {
            return _conn.Table<Animal>().DeleteAsync(i => i.aniId == p);
        }

        public Task<List<Animal>> GetAllAnimal()
        {
            return _conn.Table<Animal>().ToListAsync();
        }

        public Task<List<Animal>> SearchAnimal(string p)
        {
            string sql = "SELECT * FROM Animal WHERE aniNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<Animal>(sql);
        }


    }
}
