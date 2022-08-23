namespace Kinde.Api.Hashing
{
    public interface ICodeVerifier<HashMethod> where HashMethod : System.Security.Cryptography.HashAlgorithm
    {

        public Task<bool> VerifyCode(string original, string hash);

        public Task<string> Compute(string code);
    }
}
