using Xunit;
using AuthTest.Controllers;
using PearApi.Services;
using PearApi.Test.TestDB;
using PearApi.Authentication;
using PearApi.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace PearApi.Test;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* Welcome new victims to the api unit tests, as you soon will notice, it is quite dificult to decouple       *
* the methods of the api from the aplication due to the hevy use of dependencies. You ither have to find out *
* and use the method that the aplication uses, create a class that mocks the behavure of the dependency you  *
* need using a library such as Moq,                                                                          *
*                                                                                                            *
* tutorial -> https://docs.educationsmediagroup.com/unit-testing-csharp/moq/quick-glance-at-moq),            *
*                                                                                                            *
* or create fake ones like I have done with the fake database and its clones in the TestDB folder.           *
*                                                                                                            *
* On the topic, if I haven't mentioned it somewhare else the other Controller tests workes in the maner that *
* thay create a database template and then clone it for every test class that is run and re-seeds the them   *
* on every test. The proes of this is that every test is isolated and wont affect the others but the conns   *
* are that it is verry slow, like half a minutefor all the tests to complete. If you want it faster maybe    *
* you could run all the tests in one class and skip the database cloning and maybe also skiå the ree seeding *
* on every test. Maybe you could create one test for every constructor and instead have all the individual   *
* methods as separate assertions?                                                                            *
*                                                                                                            *
* This would be quite un object oriented of you but it none the less could improve preformance. Do not       *
* under any circomstances swich to entity framework in memory database as database Context during the tests. *
* While it improves preformance it also causes them to fail 25% of the time entigherly randomly. Another     *
* faster database solution is the in memoryy SQLite but as it is a diferent database language than the real  *
* app wich uses postgres it can behave diferently in diferent circomstances.                                 *
*                                                                                                            *
* Below I have begun to create the tests for the Authcontroller but it has prooven highly dificult           *
* as it has several dependencies wich preform complex tasks. I am nonetheless confident that you will        *
* succeed. Just remember that it is usualy harder to read code than to wright it is none the less important  *
* that you understand the code you test. I tried to be lazy and do the bare minimum but I just ended up      *
* confused and with failing tests. Ideally try to make the people that wright the code to wright tests for   *
* it as well becouse ultimately they are the ones that understand it best and as the bottles of OOP book     *
* states tests are suposed to give information about the code. Information that most likely only the authot  *
* posess.                                                                                                    *
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


//In moq It.IsAnyType can be used to mock generic argument types.


//to future users if you want to set up postgress in github actions to test youre code on pull requests folow this link.
//https://remarkablemark.org/blog/2021/03/14/setup-postgresql-in-github-actions/

[Collection("Database")]
public class AuthApiTest : DatabaseTestCase
{
   private readonly AuthController controller;
   private readonly AuthService authService;
   private readonly JwtAuthManager jwtAuthManager;

   public AuthApiTest(DatabaseFixture databaseFixture)
       : base(databaseFixture)
   {
      /*
          What is needed to test the AuthController is the sfollowing
          *ILogger<AuthController>    
              Can be mocked.                
          *IAuthService
              *ILogger<Authservice>
              *PearContext -> DBContext
          *IJwtAuthManager
              *PearContext -> DBContext
              *jwtTokenConfig -> Model 
      */

      // Create a Mock of ILogger
      var LoggerForAuthControllerMock = new Mock<ILogger<AuthController>>();
      var LoggerForAuthService = new Mock<ILogger<AuthService>>();

      // Create a mock object of the JwtTokenConfig class
      var mockJwtTokenConfig = new Mock<JwtTokenConfig>();

      // Configure the mock object to return specific values when the properties are accessed
      mockJwtTokenConfig.SetupGet(x => x.Secret).Returns("SUUPER_DUUPER_SEEECRET_THAT_MABIE_HAS_TO_BE_SUPER_DUPER_LONGGGGGGGGGGGGGGGGG");
      mockJwtTokenConfig.SetupGet(x => x.Issuer).Returns("MyIssuer");
      mockJwtTokenConfig.SetupGet(x => x.Audience).Returns("MyAudience");
      mockJwtTokenConfig.SetupGet(x => x.AccessTokenExpiration).Returns(3600);
      mockJwtTokenConfig.SetupGet(x => x.RefreshTokenExpiration).Returns(86400);


      authService = new AuthService(LoggerForAuthService.Object, DbContext);
      jwtAuthManager = new JwtAuthManager(mockJwtTokenConfig.Object, DbContext);
      controller = new AuthController(LoggerForAuthControllerMock.Object, authService, jwtAuthManager);
   }

   [Fact]
   public async void AllFunctionsInAuthControllerShouldReturnOKWhenCalledCorrectly()
   {
      //Testing Register
      var result = await controller.Register(new UserRegisterCredentials
      {
         Name = "Bert",
         Email = "burgler@thief.com",
         Password = "password",
         Adress = "In yor mum",
         Phone = "68043",
      });

      Assert.IsType<OkObjectResult>(result);

      //Testing Login

      var LoginResult = await controller.Login(new UserLoginCredentials
      {
         Email = "burgler@thief.com",
         Password = "password",
      });

      Assert.IsType<OkObjectResult>(LoginResult);

      //Testing Refresh Token
      // The body seams to be null for some reason...

      //   if (result is OkObjectResult okResult)
      //   {
      //       var body = okResult.Value;
      //       result = await controller.RefreshToken(new AuthController.RefreshTokenRequest{RefreshToken = body.ToString()});

      //       Assert.Equal("Thing", body.ToString());
      //   }
   }
}