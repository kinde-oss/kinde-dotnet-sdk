using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mocks.Flows;

namespace UnitTests
{
    [TestClass]
    public class UserProfileTests
    {
        public const string FullToken = "{\"access_token\": \"eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc1OmIyOjU5OjQ4OjdkOjEwOjg1OjQzOmY4OjkxOjUyOjJlOmFkOjllOmZhOjJhIiwidHlwIjoiSldUIn0.eyJhdWQiOltdLCJhenAiOiJyZWdAbGl2ZSIsImV4cCI6MTY3MzA5MjkxNCwiaWF0IjoxNjczMDA2NTE0LCJpc3MiOiJodHRwczovL3Rlc3RhdXRoLmtpbmRlLmNvbSIsImp0aSI6ImYxNGEzMmU1LTNjNmYtNDAwZS04MjdjLTQyZDMwNmUyYmQ1MSIsIm9yZ19jb2RlIjoib3JnXzRkYTBiOTE4ZjZiIiwicGVybWlzc2lvbnMiOm51bGwsInNjcCI6WyJvcGVuaWQiLCJvZmZsaW5lIiwicHJvZmlsZSIsImVtYWlsIl0sInN1YiI6ImtwOmI0MzVhODkyZTMzODRhZWFhYzJlYmFiNTJhYWY1MjZlIn0.Sg--HghqbB7HQgvH1FmUASqy_HetAoFCyqT9g3igg61oAP0uWChsDgWcg0GHjPZqJleSHw9309UV34MN3RyjjxB91g56l-WlJhDJaOz6sMR2q2siE8Rg7oQ_pACQixC6m_i6SNVfcKxcvlCCWsKzcID3Syy6N1rBsgThq4dHv-MT8awz7Ze5prEIej_5MjvqKo9wVer9kZEWy6EuCq38YbVzIaRwQAixBq997fgzmL9Yr33G1xnmQg9VzfZwJMReWIujcUelaG_P8GI3ZsTIekM6u8rkBplRXCAWIJteIhKbC5_RSNg8zBuKiIdYOg4eX10jbSQUa6vM0bZ4IHUzZw\",\"expires_in\": 86399,\"id_token\": \"eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc1OmIyOjU5OjQ4OjdkOjEwOjg1OjQzOmY4OjkxOjUyOjJlOmFkOjllOmZhOjJhIiwidHlwIjoiSldUIn0.eyJhdF9oYXNoIjoiMXpWdlRrMHJhRGg3VXNYRTBlZElRQSIsImF1ZCI6WyJodHRwczovL3Rlc3RhdXRoLmtpbmRlLmNvbSIsIjc0YzdmNGFmNDZiZTQwMGU5OWQ3NjFjNDg0ZjIyM2Q5Il0sImF1dGhfdGltZSI6MTY3MzAwNjUxMiwiYXpwIjoicmVnQGxpdmUiLCJlbWFpbCI6ImFuZGthbWFzaGV2QGdtYWlsLmNvbSIsImV4cCI6MTY3MzAyODExMiwiZmFtaWx5X25hbWUiOiJLYW1hc2hldiIsImdpdmVuX25hbWUiOiJBbmRyZXciLCJpYXQiOjE2NzMwMDY1MTQsImlzcyI6Imh0dHBzOi8vdGVzdGF1dGgua2luZGUuY29tIiwianRpIjoiODQyNDdjOGItN2YzNy00NmI2LWE3NjEtN2RkYWVmMDU5MWY3IiwibmFtZSI6IkFuZHJldyBLYW1hc2hldiIsIm9yZ19jb2RlcyI6WyJvcmdfNGRhMGI5MThmNmIiXSwicmF0IjoxNjczMDA2NTEyLCJzdWIiOiJrcDpiNDM1YTg5MmUzMzg0YWVhYWMyZWJhYjUyYWFmNTI2ZSIsInVwZGF0ZWRfYXQiOjEuNjczMDA2MTM1ZSswOX0.qs8T6tZQ1TLh47ITn-0RbQDG3HrH-FF38dEwTWQBjM40hRcxnowV6T-0e6CG-gwYcPJR6w459QeS8Sm7iKmk5JO8E7vCNBJMs7FcvrsIlPuLtewXtlt12oExG6Koabm6OvywU3-3HDWaDoMjE6A4v21h0Uqx5fIbEmUvkrEnaceeFQzogF6Y2XtAMoWqQeII_oNkHy3P9rOz4J1asXmI_jQxTC6WEk7D-oFTR8JXvoMdu443bG1vhwE3v51iiECtVkf-obTEvClfQQMGH5eXQ3uV06PRl2ebNQn0Jm9fTJQI506qI_X_KjNafOsRBItrP1Mqb214tNSaSUuEcy5O4w\",\"refresh_token\": \"5olDoXlsMgXLtsZ7hWGApw4JPV7gwOtNW2hgYhI9Ncg.Cb_inZRu1X57sPhA1SL24D838MSmQXVJ1K1yyOTAS_A\",\"scope\": \"openid offline profile email\",\"token_type\": \"bearer\"}";
        public const string NoIdToken = "{\"access_token\": \"eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc1OmIyOjU5OjQ4OjdkOjEwOjg1OjQzOmY4OjkxOjUyOjJlOmFkOjllOmZhOjJhIiwidHlwIjoiSldUIn0.eyJhdWQiOltdLCJhenAiOiJyZWdAbGl2ZSIsImV4cCI6MTY3MzA5MjkxNCwiaWF0IjoxNjczMDA2NTE0LCJpc3MiOiJodHRwczovL3Rlc3RhdXRoLmtpbmRlLmNvbSIsImp0aSI6ImYxNGEzMmU1LTNjNmYtNDAwZS04MjdjLTQyZDMwNmUyYmQ1MSIsIm9yZ19jb2RlIjoib3JnXzRkYTBiOTE4ZjZiIiwicGVybWlzc2lvbnMiOm51bGwsInNjcCI6WyJvcGVuaWQiLCJvZmZsaW5lIiwicHJvZmlsZSIsImVtYWlsIl0sInN1YiI6ImtwOmI0MzVhODkyZTMzODRhZWFhYzJlYmFiNTJhYWY1MjZlIn0.Sg--HghqbB7HQgvH1FmUASqy_HetAoFCyqT9g3igg61oAP0uWChsDgWcg0GHjPZqJleSHw9309UV34MN3RyjjxB91g56l-WlJhDJaOz6sMR2q2siE8Rg7oQ_pACQixC6m_i6SNVfcKxcvlCCWsKzcID3Syy6N1rBsgThq4dHv-MT8awz7Ze5prEIej_5MjvqKo9wVer9kZEWy6EuCq38YbVzIaRwQAixBq997fgzmL9Yr33G1xnmQg9VzfZwJMReWIujcUelaG_P8GI3ZsTIekM6u8rkBplRXCAWIJteIhKbC5_RSNg8zBuKiIdYOg4eX10jbSQUa6vM0bZ4IHUzZw\",\"expires_in\": 86399,\"refresh_token\": \"5olDoXlsMgXLtsZ7hWGApw4JPV7gwOtNW2hgYhI9Ncg.Cb_inZRu1X57sPhA1SL24D838MSmQXVJ1K1yyOTAS_A\",\"scope\": \"openid offline profile email\",\"token_type\": \"bearer\"}";
       
        [TestMethod]
        [DataRow(FullToken)]
        [DataRow(NoIdToken)]
        public async Task ProfileNullReferencesTest(string token)
        {
            //Arrange
            var oauthToken = JsonConvert.DeserializeObject<OauthToken>(token);
            //Act
            var user =  KindeSSOUser.FromToken(oauthToken);
            //Assert
            
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Id);
            if (user.IdToken != null )
            {
                Assert.IsNotNull(user.GivenName);
                Assert.IsNotNull(user.FamilyName);
                Assert.IsNotNull(user.Email);
            }
            try
            {
                var id = user.Id;
                var gName = user.GivenName;
                var fName = user.FamilyName;
                var email = user.Email;
                var claim = user.GetClaim("sub");
                var anotherClaim = user.GetClaim("iat");
                var unknownClaim = user.GetClaim("something");
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        
        }

    }
}
