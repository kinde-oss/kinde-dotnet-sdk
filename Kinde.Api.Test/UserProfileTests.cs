using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;
using Newtonsoft.Json;
using Xunit;

namespace Kinde.Api.Test
{
    [Collection("Sequential")]
    public class UserProfileTests
    {
        public const string FullToken = "{\"access_token\": \"eyJhbGciOiJSUzI1NiIsImtpZCI6IjZkOjEyOjljOjQ4OmJkOmYyOjNmOjNlOmRjOmRkOmY4OjNlOjQyOmY2OjI5OmExIiwidHlwIjoiSldUIn0.eyJhdWQiOltdLCJhenAiOiI0NmI4YWI3NTk2ZmM0MzE1Yjk0N2IzMmI0NTAxMTRlYyIsImV4cCI6MTY4MzU2MTMxMSwiZmVhdHVyZV9mbGFncyI6eyJjb21wZXRpdGlvbnNfbGltaXQiOnsidCI6ImkiLCJ2Ijo1fSwiaXNfZGFya19tb2RlIjp7InQiOiJiIiwidiI6dHJ1ZX0sInRoZW1lIjp7InQiOiJzIiwidiI6InBpbmsifX0sImlhdCI6MTY4MzQ3NDkxMSwiaXNzIjoiaHR0cHM6Ly9zZ3RlY2hxdC1kZXZlbG9wbWVudC5hdS5raW5kZS5jb20iLCJqdGkiOiI2ZjBkM2MwNi1jMzAxLTQ1NDItYjBjNi1lODRmYmNhZTZkMGYiLCJvcmdfY29kZSI6Im9yZ18xNDg2NjIyYjJjZCIsInBlcm1pc3Npb25zIjpbXSwic2NwIjpbIm9wZW5pZCIsIm9mZmxpbmUiLCJwcm9maWxlIiwiZW1haWwiXSwic3ViIjoia3A6MTQ3MTQzZDBmYTRhNDZhOGFkNjIwYzJjYTFhNmMyOTAifQ.ROuQPhiYWQRP7vAyanlnSt-g-7qVh70HdINI_oU7HKsaYILkGJKcXNKCEsueFfo7nIr8FJgi5n0bXP5CNyKRRLstLL1huyrb7ug4aD9BfuPsh2reVzbcLQJ325xcqT2qIfwAHI-48SvJZ0AnqZ1Zx-OjYpRQF8T2QIv4CzVPQOZ37cWspB1bKieFVJr5kh5S_0Xl4Gb3IJdLauRY5VyfwzUdvppF50988RECPkRgdcWhf0cEqwLUqUHMPKj-nwJnOUC1sEUugl1IULR5_S1X6Uw9QcuLSiILnXmElKRvl21XWJ51MuJvpmwfFg8Gg7WSRAzCA4fxXMQKyJ9amatElg\",\"expires_in\":86399,\"scope\":\"openid offline profile email\",\"token_type\":\"bearer\",\"id_token\":\"eyJhbGciOiJSUzI1NiIsImtpZCI6IjZkOjEyOjljOjQ4OmJkOmYyOjNmOjNlOmRjOmRkOmY4OjNlOjQyOmY2OjI5OmExIiwidHlwIjoiSldUIn0.eyJhdF9oYXNoIjoiSl9zZXdiYWJaRHI2UERJYnZBSG5MQSIsImF1ZCI6WyJodHRwczovL3NndGVjaHF0LWRldmVsb3BtZW50LmF1LmtpbmRlLmNvbSIsIjQ2YjhhYjc1OTZmYzQzMTViOTQ3YjMyYjQ1MDExNGVjIl0sImF1dGhfdGltZSI6MTY4MzQ3NDkxMSwiYXpwIjoiNDZiOGFiNzU5NmZjNDMxNWI5NDdiMzJiNDUwMTE0ZWMiLCJlbWFpbCI6InF1b2N0cmFuMjEyNEBnbWFpbC5jb20iLCJleHAiOjE2ODM0Nzg1MTEsImZhbWlseV9uYW1lIjoiU2VsdG9uIiwiZ2l2ZW5fbmFtZSI6IkFuZHJlIiwiaWF0IjoxNjgzNDc0OTExLCJpc3MiOiJodHRwczovL3NndGVjaHF0LWRldmVsb3BtZW50LmF1LmtpbmRlLmNvbSIsImp0aSI6IjEwN2E1NGE3LWNmMmQtNDc0MC04YzJlLTM1ZDIzOWIwZmY5NSIsIm5hbWUiOiJxdW9jIHRyYW4iLCJvcmdfY29kZXMiOlsib3JnXzE0ODY2MjJiMmNkIl0sInBpY3R1cmUiOiJodHRwczovL2dvb2dsZS1hdmF0YXIuY29tLzEyNDU1Iiwic3ViIjoia3A6MTQ3MTQzZDBmYTRhNDZhOGFkNjIwYzJjYTFhNmMyOTAiLCJ1cGRhdGVkX2F0IjoxNjgzNDQ0Njg4fQ.xbD1hRtqBCrExAFz5fu6tvNd1gYGK90ibnYUyw8TJBtEcHpjnWdLJXyBzgorv5ZHBjCMpbVrQKlDasA7zEtDKwZcbx5p2E0cD-9G56lerA_iH6udFMioju4vkH-vZ6kykJGGSantI98jkhXqFDb3v-_cUkOmgC6-B-lhlpL_E-beWMkqUCF0pBPejDKn61EzAEIGDMZFch3WrW_uVXgg6w4L9uJMXk1VuigUiaOXRilpPa_JICFku0SQRCWve0twQBb3HyCOQVxCMeDrQYr_Ig6qj0JGHG5wuC5LyMkbDbLwTQGRPp_-2_he6w7SkUS9IdDv6FmC3ZWr2zO5jmma-A\",\"refresh_token\":\"QXrHVkI13TAiMY6VDpfRtkYru43G2iZcaGTtH9AZrtM.MoMc-q_ZcUut5YtyVdSdgevtnU1lh0VmLKvP_zZkArM\",\"Recieved\":\"2023-05-07T22:55:04.1334672+07:00\",\"IsExpired\":false,\"Duration\":\"23:59:22.6228475\"}";
        public const string NoIdToken = "{\"access_token\": \"eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc1OmIyOjU5OjQ4OjdkOjEwOjg1OjQzOmY4OjkxOjUyOjJlOmFkOjllOmZhOjJhIiwidHlwIjoiSldUIn0.eyJhdWQiOltdLCJhenAiOiJyZWdAbGl2ZSIsImV4cCI6MTY3MzA5MjkxNCwiaWF0IjoxNjczMDA2NTE0LCJpc3MiOiJodHRwczovL3Rlc3RhdXRoLmtpbmRlLmNvbSIsImp0aSI6ImYxNGEzMmU1LTNjNmYtNDAwZS04MjdjLTQyZDMwNmUyYmQ1MSIsIm9yZ19jb2RlIjoib3JnXzRkYTBiOTE4ZjZiIiwicGVybWlzc2lvbnMiOm51bGwsInNjcCI6WyJvcGVuaWQiLCJvZmZsaW5lIiwicHJvZmlsZSIsImVtYWlsIl0sInN1YiI6ImtwOmI0MzVhODkyZTMzODRhZWFhYzJlYmFiNTJhYWY1MjZlIn0.Sg--HghqbB7HQgvH1FmUASqy_HetAoFCyqT9g3igg61oAP0uWChsDgWcg0GHjPZqJleSHw9309UV34MN3RyjjxB91g56l-WlJhDJaOz6sMR2q2siE8Rg7oQ_pACQixC6m_i6SNVfcKxcvlCCWsKzcID3Syy6N1rBsgThq4dHv-MT8awz7Ze5prEIej_5MjvqKo9wVer9kZEWy6EuCq38YbVzIaRwQAixBq997fgzmL9Yr33G1xnmQg9VzfZwJMReWIujcUelaG_P8GI3ZsTIekM6u8rkBplRXCAWIJteIhKbC5_RSNg8zBuKiIdYOg4eX10jbSQUa6vM0bZ4IHUzZw\",\"expires_in\": 86399,\"refresh_token\": \"5olDoXlsMgXLtsZ7hWGApw4JPV7gwOtNW2hgYhI9Ncg.Cb_inZRu1X57sPhA1SL24D838MSmQXVJ1K1yyOTAS_A\",\"scope\": \"openid offline profile email\",\"token_type\": \"bearer\"}";

        [Theory]
        [InlineData(FullToken, true)]
        [InlineData(NoIdToken, false)]
        public void ProfileNullReferencesTest(string token, bool shouldHaveValue)
        {
            //Arrange
            var oauthToken = JsonConvert.DeserializeObject<OauthToken>(token);

            //Act
            var user = KindeSSOUser.FromToken(oauthToken);

            //Assert
            Assert.NotNull(user);
            if (user.IdToken != null)
            {
                Assert.NotNull(user.Id);
                Assert.NotNull(user.GivenName);
                Assert.NotNull(user.FamilyName);
                Assert.NotNull(user.Email);
                Assert.NotNull(user.Picture);
            }
            try
            {
                var id = user.Id;
                var gName = user.GivenName;
                var fName = user.FamilyName;
                var email = user.Email;
                var picture = user.Picture;
                var anotherClaim = user.GetClaim("iat");
                var claim = user.GetClaim("sub", "id_token"); //get claim
                var organizations = user.GetOrganizations(); ; //get avaliable organizations
                var organization = user.GetOrganization();  //get single organization
                var permissions = user.GetPermissions(); //get all permissions
                var permission = user.GetPermission("something"); //get permission
                var unknownClaim = user.GetClaim("something");

                if (shouldHaveValue)
                {
                    Assert.NotNull(id);
                    Assert.NotNull(gName);
                    Assert.NotNull(fName);
                    Assert.NotNull(claim);
                    Assert.NotNull(email);
                    Assert.NotNull(anotherClaim);
                    Assert.NotNull(organizations);
                    Assert.NotNull(picture);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Theory]
        [InlineData(FullToken, "id_token", "given_name", "Andre")]
        [InlineData(FullToken, "id_token", "family_name", "Selton")]
        [InlineData(FullToken, "id_token", "picture", "https://google-avatar.com/12455")]
        public void GetClaim_FullInfoToken_ShouldReturnClaimObject(string jsonToken, string tokenType, string key, string expectedValue)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var claim = user.GetClaim(key, tokenType);

            //Assert
            Assert.NotNull(claim);
            Assert.NotNull(claim.Name);
            Assert.NotNull(claim.Value);
            Assert.Equal(key, claim.Name);
            Assert.Equal(expectedValue, claim.Value);
        }

        [Theory]
        [InlineData(FullToken, "theme", "pink", "string")]
        [InlineData(FullToken, "is_dark_mode", true, "boolean")]
        [InlineData(FullToken, "competitions_limit", 5, "integer")]
        public void GetFlag_FullInfoToken_ShouldReturnCorrectFlagObject(string jsonToken, string code, object expectedValue, string expectedType)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var flag = user.GetFlag(code);

            //Assert
            Assert.NotNull(flag);
            Assert.Equal(code, flag.Code);
            Assert.Equal(expectedType, flag.Type);
            Assert.Equal(false, flag.Is_default);
            Assert.Equal(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(flag.Value));
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetFlag_FlagNotExist_ShouldReturnProvidedDefaultValue(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var flag = user.GetFlag("create_competition", new FeatureFlagValue { DefaultValue = false });

            //Assert
            Assert.NotNull(flag);
            Assert.Equal("create_competition", flag.Code);
            Assert.Equal(false, flag.Value);
            Assert.Equal(true, flag.Is_default);
            Assert.Null(flag.Type);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetFlag_InvalidFlagType_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetFlag("competitions_limit", new FeatureFlagValue { DefaultValue = 3 }, "s"));

            //Assert
            Assert.Equal("Flag \"competitions_limit\" is type integer - requested type string", exception.Message);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetFlag_FlagNotExist_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetFlag("new_feature"));

            //Assert
            Assert.Equal("This flag was not found, and no default value has been provided", exception.Message);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetBooleanFlag_FlagExist_ShouldReturnFlagValueRegardlessDefaultValue(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var actual = user.GetBooleanFlag("is_dark_mode");
            var actualWithDefaultValue = user.GetBooleanFlag("is_dark_mode", false);

            //Assert
            Assert.True(actual);
            Assert.True(actualWithDefaultValue);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetBooleanFlag_FlagNotExist_ShouldReturnProvidedDefaultValue(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var actual = user.GetBooleanFlag("new_feature", false);

            //Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetBooleanFlag_FlagNotExist_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetBooleanFlag("new_feature"));

            //Assert
            Assert.Equal("This flag was not found, and no default value has been provided", exception.Message);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetBooleanFlag_InvalidFlagType_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetBooleanFlag("theme"));

            //Assert
            Assert.Equal("Flag \"theme\" is type string - requested type boolean", exception.Message);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetStringFlag_FlagExist_ShouldReturnFlagValueRegardlessDefaultValue(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var actual = user.GetStringFlag("theme");
            var actualWithDefaultValue = user.GetStringFlag("theme", "orange");

            //Assert
            Assert.Equal("pink", actual);
            Assert.Equal("pink", actualWithDefaultValue);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetStringFlag_FlagNotExist_ShouldReturnProvidedDefaultValue(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var actual = user.GetStringFlag("cta_color", "blue");

            //Assert
            Assert.Equal("blue", actual);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetStringFlag_FlagNotExist_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetStringFlag("cta_color"));

            //Assert
            Assert.Equal("This flag was not found, and no default value has been provided", exception.Message);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetStringFlag_InvalidFlagType_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetStringFlag("is_dark_mode"));

            //Assert
            Assert.Equal("Flag \"is_dark_mode\" is type boolean - requested type string", exception.Message);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetIntegerFlag_FlagExist_ShouldReturnFlagValueRegardlessDefaultValue(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var actual = user.GetIntegerFlag("competitions_limit");
            var actualWithDefaultValue = user.GetIntegerFlag("competitions_limit", 3);

            //Assert
            Assert.Equal(5, actual);
            Assert.Equal(5, actualWithDefaultValue);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetIntegerFlag_FlagNotExist_ShouldReturnProvidedDefaultValue(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var actual = user.GetIntegerFlag("team_count", 5);

            //Assert
            Assert.Equal(5, actual);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetIntegerFlag_FlagNotExist_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetIntegerFlag("team_count"));

            //Assert
            Assert.Equal("This flag was not found, and no default value has been provided", exception.Message);
        }

        [Theory]
        [InlineData(FullToken)]
        public void GetIntegerFlag_InvalidFlagType_ShouldThrowException(string jsonToken)
        {
            //Arrange
            var token = JsonConvert.DeserializeObject<OauthToken>(jsonToken);
            var user = KindeSSOUser.FromToken(token);

            //Act
            var exception = Assert.Throws<ApplicationException>(() => user.GetIntegerFlag("is_dark_mode"));

            //Assert
            Assert.Equal("Flag \"is_dark_mode\" is type boolean - requested type integer", exception.Message);
        }
    }
}
