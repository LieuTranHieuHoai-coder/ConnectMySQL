using ConnectMySQL.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectMySQL.Business
{
    public class ProblemBusiness
    {
        readonly DatabaseContext connection = null;
        public ProblemBusiness()
        {
            connection = new DatabaseContext();
        }
        //
        public async Task<IEnumerable<Problem>> GetList()
        {
            try
            {
                return await connection.QueryAsync<Problem>("SELECT * FROM Problem;");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Problem> GetId(int id)
        {
            try
            {
                return await connection.FirstOrDefaultAsync<Problem>("SELECT * FROM Problem WHERE Id=?Id;", new { Id = id });
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Problem> GetProblemName(string name)
        {
            try
            {
                return await connection.FirstOrDefaultAsync<Problem>("SELECT * FROM Problem WHERE Name=?Name;", new { Name = name });
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Insert(Problem problem)
        {
            try
            {
                var found = await GetProblemName(problem.name);
                if (found != null)
                {
                    return 0;
                }
                var sql = @"INSERT INTO Problem (Name) VALUES (?Name);";
                return connection.Execute(sql, problem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Update(Problem problem)
        {
            try
            {
                var sql = @"UPDATE problem SET Name=?Name WHERE Id=?Id";
                return connection.Execute(sql, problem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Delete(int id)
        {
            try
            {
                var sql = @"DELETE FROM Problem WHERE Id=?Id;";
                return connection.Execute(sql, new { Id = id});
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
