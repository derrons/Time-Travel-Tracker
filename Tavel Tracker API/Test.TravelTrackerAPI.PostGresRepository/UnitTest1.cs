using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;
using Tavel_Tracker_API.Models;
using Tavel_Tracker_API.Repositories;

namespace Test.TravelTrackerAPI.PostGresRepository;

public class Tests
{
  private ILoggerFactory _loggerFactory;
  private string _connectionString;
  private User _user;

  [SetUp]
  public void Setup()
  {
    var config = new  ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.test.json",false,true)
     .AddUserSecrets<Tests>()
     .AddEnvironmentVariables()
     .Build();

    _connectionString = string.Concat(config.GetConnectionString("PostGres"), config["PostGres"]);
   
    _loggerFactory = new NullLoggerFactory();

    _user = new User
    {
      FirstName = "James",
      LastName = "Doe",
      Email = "james.doe@gmail.com",
      Address1 = "1234 Hillside Drive",
      Address2 = "Apt 2",
      City = "Haslett",
      State = "MI",
      Zip = 12345
    };

  }

  [Test]
  public async Task Test1Async()
  {
    var userDb = new UserPostGres(_connectionString, _loggerFactory.CreateLogger<UserPostGres>());
    var locationDb = new LocationPostGres(_connectionString, _loggerFactory.CreateLogger<LocationPostGres>());
    int isFound;

    do
    {
      isFound = await userDb.IsUniqueEmail(_user.Email);
      if (isFound > 0)
      {
        _user.Email += "1";
      }
    }
    while (isFound > 0);

    var addResult = await userDb.AddAsync(_user);
    Assert.That(addResult, Is.EqualTo(1));
    
    
  }

}
