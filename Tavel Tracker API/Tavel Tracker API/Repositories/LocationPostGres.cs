using Microsoft.Extensions.Configuration;
using Tavel_Tracker_API.Interfaces;
using Tavel_Tracker_API.Models;
using Dapper;
using Npgsql;
using static Dapper.SqlMapper;

namespace Tavel_Tracker_API.Repositories;

public class LocationPostGres : ILocationRepository
{
    private readonly string _connectionString;
    private readonly ILogger<LocationPostGres> _logger;

    public LocationPostGres(string connectionString, ILogger<LocationPostGres> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }
    public async Task<int> AddAsync(Location entity)
    {
    string sql = "Insert into public.locations (\"UserId\",\"Address1\",\"Address2\",\"City\",\"State\",\"Zip\",\"Time\",\"EntryType\") ";
    sql += "VALUES (@UserId,@Address1,@Address2,@City,@State,@Zip,@Time,@EntryType)";
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
      string sql = "Delete from public.locations where \"Id\"=@Id";
      using (var connection = new NpgsqlConnection(_connectionString))
      {
        connection.Open();
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result;
      }
    }

    public async Task<IReadOnlyList<Location>> GetAllAsync()
    {
      string sql = "Select * from public.locations";
      using (var connection = new NpgsqlConnection(_connectionString))
      {
        connection.Open();
        var result = await connection.QueryAsync<Location>(sql);
        return result.ToList();
      }
  }

    public async Task<Location> GetByIdAsync(int id)
    {
      string sql = "Select * from public.locations where \"Id\"=@Id";
      using (var connection = new NpgsqlConnection(_connectionString))
      {
        connection.Open();
        var result = await connection.QueryFirstAsync<Location>(sql, new {Id = id});
        return result;
      }
    }

    async Task<int> IRepository<Location>.UpdateAsync(Location entity)
    {
      string sql = "UPDATE public.locations SET \"Address1\" = @Address1, \"Address2\" = @Address2, \"City\" = @City, \"Zip\" = @Zip, \"EntryType\" = @EntryType  WHERE \"Id\" = @Id";
      using (var connection = new NpgsqlConnection(_connectionString))
      {
        connection.Open();
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
      }
    }
}
