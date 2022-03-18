using MySql.Data.MySqlClient;

namespace StendenCafe.Services
{
    public abstract class BaseRepository
    {
		protected IConfiguration config;
		protected string connString;
		public BaseRepository(IConfiguration config)
		{
			this.config = config;
			connString = config.GetConnectionString("Default");
		}

		protected MySqlConnection Connect()
		{
			// MySql.Data has connection pooling,
			// keeping a native connection to the server independent of the creation and disposal of these objects.
			return new MySqlConnection(connString);
		}
	}
}
