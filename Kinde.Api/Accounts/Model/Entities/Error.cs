#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents an error
    /// </summary>
    public partial class Error
    {
        [JsonConstructor]
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
            OnCreated();
        }

        partial void OnCreated();

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Error {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
