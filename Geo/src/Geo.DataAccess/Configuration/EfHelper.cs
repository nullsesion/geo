using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Geo.DataAccess.Configuration
{
	public static class EfHelper
	{
		public static async Task<string> Truncate<T>(this DbSet<T> dbSet) where T : class
		{
			var context = dbSet.GetService<ICurrentDbContext>().Context;
			string cmd = $"TRUNCATE TABLE {AnnotationHelper.TableName(dbSet)} CASCADE"; //TABLE
			using (var command = context.Database.GetDbConnection().CreateCommand())
			{
				if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				command.CommandText = cmd;
				await command.ExecuteNonQueryAsync();
			}
			return cmd;
		}
	}
}
