using Dapper;
using StendenCafe.Models;

namespace StendenCafe.Services
{
    public class CategoryRepository : BaseRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

		public async Task<IEnumerable<Category>> Get()
		{
			using var connection = Connect();

			return await connection.QueryAsync<Category>(@"SELECT *
															FROM Category
															ORDER BY Name");
		}

		public async Task<Category> Get(int id)
		{
			using var connection = Connect();

			return await connection.QuerySingleAsync<Category>(@"SELECT * 
																FROM Category
																WHERE Id = @id", 
																new { id });
		}

		public async Task<Category> Add(Category category)
		{
			using var connection = Connect();

			return await connection.QuerySingleAsync<Category>(@"INSERT INTO Category (Name)
																VALUES (@Name);
																SELECT * FROM Category
																WHERE Id = LAST_INSERT_ID()", 
																new { category.Name });
		}

		public async Task Delete(int id)
		{
			using var connection = Connect();

			await connection.ExecuteAsync(@"DELETE FROM Category WHERE Id = @id;
											DELETE FROM Product WHERE CategoryId = @id", 
											new { id });
		}

		public async Task<Category> Update(Category category)
		{
			using var connection = Connect();

			return await connection.QuerySingleAsync<Category>(@"UPDATE Category 
																SET
																	Name = @Name
																WHERE Id = @id;
																SELECT * FROM Category WHERE Id = @Id", 
																category);
		}
	}
}
