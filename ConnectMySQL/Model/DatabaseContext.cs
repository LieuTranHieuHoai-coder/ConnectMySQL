using Dapper;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectMySQL.Model
{
    public class DatabaseContext
    {
        //MySqlConnection connection = null;
        SqlConnection connection = null;
        public DatabaseContext()
        {
            //connection = new MySqlConnection("server=localhost;database=test;uid=root;password=");
            connection = new SqlConnection("server=DESKTOP-H5IA8G8\\SQLEXPRESS01;database=test;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                return await connection.QueryAsync<T>(sql, param);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        public async Task<T> FirstOrDefaultAsync<T>(string sql, object param = null)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                return await connection.ExecuteScalarAsync<T>(sql, param);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        public int Execute(string sql, object param)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        int returnValue = connection.Execute(sql, param, trans);
                        trans.Commit();
                        return returnValue;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
