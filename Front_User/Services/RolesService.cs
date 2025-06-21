using System.Text.Json;

namespace Frontend.Services
{
    public class RolesService
    {
        public List<string> GetRolesFromToken(string jwtToken)
        {
            var parts = jwtToken.Split('.');
            if (parts.Length < 2)
                return new List<string>();

            var payload = parts[1];

            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=')
                             .Replace('-', '+')
                             .Replace('_', '/');

            var jsonBytes = Convert.FromBase64String(payload);
            var json = System.Text.Encoding.UTF8.GetString(jsonBytes);

            using JsonDocument doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("role", out var rolesProperty))
            {
                if (rolesProperty.ValueKind == JsonValueKind.Array)
                {
                    return rolesProperty.EnumerateArray().Select(r => r.GetString()!).ToList();
                }
                else if (rolesProperty.ValueKind == JsonValueKind.String)
                {
                    return new List<string> { rolesProperty.GetString()! };
                }
            }

            return new List<string>();
        }
    }
}
