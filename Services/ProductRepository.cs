using Dapper;
using StendenCafe.Models;

namespace StendenCafe.Services
{
	public class ProductRepository : BaseRepository
	{
		public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

		public async Task<IEnumerable<Product>> Get()
		{
			using var connection = Connect();

			return await connection.QueryAsync<Product, Category, Product>(@"SELECT *
																			FROM Product
																				INNER JOIN Category
																				ON Product.CategoryId = Category.Id",
																			(product, category) =>
																			{
																				product.Category = category; return product;
																			});
		
		}

		public async Task<Product> Get(int id)
		{
			using var connection = Connect();

			return await connection.QuerySingleAsync<Product>(@"SELECT * 
																FROM Product
																	INNER JOIN Category 
																		ON Product.CategoryId = Category.Id
																WHERE Product.Id = @id", 
																new { id });
		}

		public async Task<Product> Add(Product product)
		{
			using var connection = Connect();

			return await connection.QuerySingleAsync<Product>(@"INSERT INTO Product (Name, CategoryId, Price)
												VALUES (@Name, @CategoryId, @Price);
												SELECT * FROM Product
												WHERE Id = LAST_INSERT_ID()",
												product);
		}

		public async Task Delete (int id)
		{
			using var connection = Connect();

			await connection.ExecuteAsync(@"DELETE FROM Product WHERE Id = @id", new { id });
		}

		public async Task<Product> Update (Product product)
		{
			using var connection = Connect();

			return await connection.QuerySingleAsync<Product>(@"UPDATE Product SET
																	Name = @Name,
																	CategoryId = @CategoryId,
																	Price = @Price
																WHERE Id = @id
																SELECT * FROM product WHERE Id = @Id", 
																product);
		}
	}
}


