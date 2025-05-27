using Common.Repositories;
using Dapper;
using DotNetTraining.Domains.Entities;
using System.Data;

namespace DotNetTraining.Repositories
{
    public class TokenRepository(IDbConnection connection) : SimpleCrudRepository<Token, Guid>(connection)
    {

        public async Task<Token?> Create(Token token)
        {
            return await CreateAsync(token);
        }
        public async Task<Token?> GetByRefreshToken(string refreshToken)
        {
            var sql = "SELECT * FROM Tokens WHERE RefreshToken = @RefreshToken";
            return await connection.QueryFirstOrDefaultAsync<Token>(sql, new { RefreshToken = refreshToken });
        }

        public async Task<Token?> Update(Token token)
        {
            return await UpdateAsync(token); // SimpleCrud
        }


    }
}
