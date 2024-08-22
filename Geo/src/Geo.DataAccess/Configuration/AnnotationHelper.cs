using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Geo.DataAccess.Configuration
{
	public class AnnotationHelper
	{
		private static string GetName(IEntityType entityType, string defaultSchemaName = "public")
		{
			var schema = entityType.FindAnnotation("Relational:Schema").Value;
			string tableName = entityType.GetAnnotation("Relational:TableName").Value.ToString();
			
			string schemaName = schema == null ? defaultSchemaName : schema.ToString();
			//string name = string.Format("[{0}].[{1}]", schemaName, tableName);
			string name = string.Format(""" {0}."{1}" """, schemaName, tableName);
			
			//string name = string.Format("{0}", tableName);
			return name;
		}

		public static string TableName<T>(DbSet<T> dbSet) where T : class
		{
			var entityType = dbSet.EntityType;
			return GetName(entityType);
		}
	}
}
