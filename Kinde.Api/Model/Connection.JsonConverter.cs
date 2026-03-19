/*
 * Applies custom converter so Connection deserializes from both wrapped and flat API payloads.
 * Do not remove - required for GetConnections() on .NET 10 when API returns ConnectionConnection-shaped items.
 */

using Newtonsoft.Json;

namespace Kinde.Api.Model
{
    [JsonConverter(typeof(ConnectionConverter))]
    public partial class Connection
    {
    }
}
