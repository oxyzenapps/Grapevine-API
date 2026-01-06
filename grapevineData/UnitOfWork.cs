using grapevineData.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineData
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlConnection _conn;
        private SqlTransaction? _tx;

        public ISqlExecutor Sql { get; }

        public UnitOfWork(IConfiguration config)
        {
            _conn = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            Sql = new SqlExecutor(config);
        }

        public async Task BeginAsync()
        {
            await _conn.OpenAsync();
            _tx = _conn.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            _tx?.Commit();
            await _conn.CloseAsync();
        }

        public async Task RollbackAsync()
        {
            _tx?.Rollback();
            await _conn.CloseAsync();
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
