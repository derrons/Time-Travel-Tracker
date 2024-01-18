using Npgsql;
using Dapper;
using Tavel_Tracker_API.Interfaces;
using Tavel_Tracker_API.Models;

namespace Tavel_Tracker_API.Repositories;

public class UserPostGres : IUserRepository
{
  private readonly string _connectionString;
  private readonly ILogger<UserPostGres> _logger;

  public UserPostGres(string connectionString, ILogger<UserPostGres> logger)
  {
    _connectionString = connectionString;
    _logger = logger;
  }

  public async Task<int> AddAsync(User entity)
  {
    string sql = "Insert into public.users (\"Email\",\"FirstName\",\"LastName\",\"Address1\",\"Address2\",\"City\",\"State\",\"Zip\") ";
    sql += "VALUES (@Email,@FirstName, @LastName,@Address1,@Address2,@City,@State,@Zip)";
    using (var connection = new NpgsqlConnection(_connectionString))
    {
      connection.Open();
      var result = await connection.ExecuteAsync(sql, entity);
      return result;
    }
  }

  public async Task<int> DeleteAsync(int id)
  {
    string sql = "Delete from public.users where \"Id\"=@Id";
    using (var connection = new NpgsqlConnection(_connectionString))
    {
      connection.Open();
      var result = await connection.ExecuteAsync(sql, new { Id = id });
      return result;
    }
  }

  public async Task<IReadOnlyList<User>> GetAllAsync()
  {
    string sql = "Select * from public.users";
    using (var connection = new NpgsqlConnection(_connectionString))
    {
      connection.Open();
      var result = await connection.QueryAsync<User>(sql);
      return result.ToList();
    }
  }

  public async Task<User> GetByIdAsync(int id)
  {
    string sql = "Select * from public.users where \"Id\"=@Id";
    using (var connection = new NpgsqlConnection(_connectionString))
    {
      connection.Open();
      var result = await connection.QueryFirstAsync<User>(sql,new { Id = id });
      return result;
    }
  }

  public async Task<int> UpdateAsync(User entity)
  {
    string sql = "UPDATE public.users SET \"Email\" = @Email, \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Address1\" = @Address1, ";
    sql += "\"Address2\" = @Address2, \"City\" = @City, \"Zip\" = @Zip WHERE \"Id\" = @Id";
    using (var connection = new NpgsqlConnection(_connectionString))
    {
      connection.Open();
      var result = await connection.ExecuteAsync(sql, entity);
      return result;
    }
  }

  public async Task<int> IsUniqueEmail(string email)
  {
    string sql = "select count(distinct \"Email\") from public.users where \"Email\"=@Email";
    using (var connection = new NpgsqlConnection(_connectionString))
    {
      connection.Open();
      var result = await connection.ExecuteScalarAsync<int>(sql, new { Email = email });
      return result;
    }
  }

  public async Task<User> GetByEmailAsync(string email)
  {
    string sql = "Select * from public.users where \"Email\"=@Email";
    using (var connection = new NpgsqlConnection(_connectionString))
    {
      connection.Open();
      var result = await connection.QueryFirstAsync<User>(sql, new { Email = email });
      return result;
    }
  }
}
