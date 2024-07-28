namespace Geo.DataAccess.Repositories
{
	public class AbstractRepository
	{
		protected readonly GeoApiDbContext _dbContext;
		public AbstractRepository(GeoApiDbContext dbContext) => _dbContext = dbContext;

		private bool _disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
				if (disposing)
					_dbContext.Dispose();
			_disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		//protected int GetOffsetAndSizePosition(int page, int pageSize) => pageSize * (page - 1 > 0 ? page - 1 : 0);
	}
}
