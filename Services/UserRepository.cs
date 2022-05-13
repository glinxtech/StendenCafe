using System;
using Dapper;
using StendenCafe.Models;

namespace StendenCafe.Services
{
    using BCrypt = BCrypt.Net.BCrypt;

    public class UserRepository : BaseRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<CafeUser> Add(CafeUser cafeUser)
        {
            cafeUser.Id = Guid.NewGuid().ToString();
            cafeUser.Password = BCrypt.HashPassword(cafeUser.Password);
            using var connection = Connect();
            return await connection.QuerySingleAsync<CafeUser>(
                @"
                    INSERT INTO Cafeuser (Id, Username, Password, Location)
					VALUES (@Id, @Username, @Password, Location);
					SELECT *
                    FROM Cafeuser
					WHERE Id = @Id
                ",
                new
                {
                    cafeUser.Id,
                    cafeUser.Username,
                    cafeUser.Password,
                    cafeUser.Location
                }
            );
        }


        public async Task<CafeUser> GetByUser(string username)
        {
            using var connection = Connect();
            return await connection.QuerySingleAsync<CafeUser>(
                @"
                    SELECT *
                    FROM Cafeuser
                    WHERE Username = @username
                ",
                new { username }
            );
        }

        public async Task<CafeUser> GetById(string id)
        {
            using var connection = Connect();

            return await connection.QuerySingleAsync<CafeUser>(@"SELECT * 
																FROM Cafeuser
																WHERE Id = @id",
                                                                new { id });
        }

        public async Task<string?> Verify(CafeUser login)
        {
            using var connection = Connect();

            var attempted = await connection.QuerySingleAsync<CafeUser>(@"SELECT *
                                                                   FROM Cafeuser
                                                                   WHERE Username = @Username",
                                                                  new { login.Username });

            return BCrypt.Verify(login.Password, attempted.Password) ? attempted.Id : null;
        }

    }
}
