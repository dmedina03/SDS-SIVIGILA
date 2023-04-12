using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SIVIGILA.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIVIGILATests.AdminSQLITE
{
    internal class ConnectionFactory: IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        public context CreateContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<context>().UseSqlite(connection).Options;

            var context = new context(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context!;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
