using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Npgsql;

// Leemos la conexión que ya usa la API, sin copiar la contraseña a este archivo.
var settingsPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
    "../../../../../lab10.Api/appsettings.json"));
using var settings = JsonDocument.Parse(await File.ReadAllTextAsync(settingsPath));
var connectionString = settings.RootElement.GetProperty("ConnectionStrings")
    .GetProperty("DefaultConnection").GetString()
    ?? throw new InvalidOperationException("Falta la conexión de la base de datos.");

await using var connection = new NpgsqlConnection(connectionString);
await connection.OpenAsync();
if (args.Contains("--inspect"))
{
    await using var check = new NpgsqlCommand(
        "SELECT pg_get_constraintdef(oid) FROM pg_constraint WHERE conname = 'tickets_status_check'", connection);
    Console.WriteLine(await check.ExecuteScalarAsync());
    return;
}
await using var transaction = await connection.BeginTransactionAsync();

// Los mismos ID permiten repetir la carga sin crear copias.
static Guid Id(string type, int number)
{
    var hash = SHA256.HashData(Encoding.UTF8.GetBytes($"lab10-seed:{type}:{number}"));
    return new Guid(hash.AsSpan(0, 16));
}

static async Task Insert(NpgsqlConnection connection, NpgsqlTransaction transaction,
    string sql, params object[] values)
{
    await using var command = new NpgsqlCommand(sql, connection, transaction);
    for (var i = 0; i < values.Length; i++)
        command.Parameters.AddWithValue($"p{i}", values[i]);
    await command.ExecuteNonQueryAsync();
}

var roles = new[] { "Admin", "Soporte", "Analista", "Supervisor", "Consulta" };
var titles = new[]
{
    "No puedo iniciar sesión", "La página tarda en abrir", "Error al subir archivo",
    "Actualizar datos del perfil", "Consulta sobre el reporte"
};
var statuses = new[] { "abierto", "en_proceso", "cerrado", "abierto", "cerrado" };

for (var i = 1; i <= 5; i++)
{
    // Usamos contraseñas aleatorias que no se comparten: estos usuarios son solo de muestra.
    var randomPassword = RandomNumberGenerator.GetBytes(32);
    var salt = RandomNumberGenerator.GetBytes(16);
    var hash = Rfc2898DeriveBytes.Pbkdf2(randomPassword, salt, 100_000,
        HashAlgorithmName.SHA256, 32);
    var storedHash = $"pbkdf2-sha256$100000${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";

    await Insert(connection, transaction,
        "INSERT INTO roles (role_id, role_name) VALUES (@p0, @p1) ON CONFLICT (role_id) DO NOTHING",
        Id("role", i), $"Demo Lab10 {roles[i - 1]}");
    await Insert(connection, transaction,
        "INSERT INTO users (user_id, username, email, password_hash) VALUES (@p0, @p1, @p2, @p3) ON CONFLICT (user_id) DO NOTHING",
        Id("user", i), $"demo_lab10_{i}", $"demo_lab10_{i}@example.invalid", storedHash);
    await Insert(connection, transaction,
        "INSERT INTO user_roles (user_id, role_id) VALUES (@p0, @p1) ON CONFLICT (user_id, role_id) DO NOTHING",
        Id("user", i), Id("role", i));
    await Insert(connection, transaction,
        "INSERT INTO tickets (ticket_id, user_id, title, description, status, closed_at) VALUES (@p0, @p1, @p2, @p3, @p4, @p5) ON CONFLICT (ticket_id) DO NOTHING",
        Id("ticket", i), Id("user", i), titles[i - 1],
        "Registro de ejemplo para practicar los reportes Excel.", statuses[i - 1],
        statuses[i - 1] == "cerrado"
            ? (object)DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            : DBNull.Value);
    await Insert(connection, transaction,
        "INSERT INTO responses (response_id, ticket_id, responder_id, message) VALUES (@p0, @p1, @p2, @p3) ON CONFLICT (response_id) DO NOTHING",
        Id("response", i), Id("ticket", i), Id("user", i),
        "Respuesta de ejemplo para el ticket.");
}

await transaction.CommitAsync();

// Comprobamos que quedaron cinco filas de muestra en cada tabla.
foreach (var (table, idColumn, type) in new[]
{
    ("roles", "role_id", "role"), ("users", "user_id", "user"),
    ("tickets", "ticket_id", "ticket"), ("responses", "response_id", "response")
})
{
    await using var command = new NpgsqlCommand(
        $"SELECT count(*) FROM {table} WHERE {idColumn} = ANY(@ids)", connection);
    command.Parameters.AddWithValue("ids", Enumerable.Range(1, 5).Select(i => Id(type, i)).ToArray());
    Console.WriteLine($"{table}: {await command.ExecuteScalarAsync()} registros de ejemplo");
}

await using (var command = new NpgsqlCommand(
    "SELECT count(*) FROM user_roles WHERE user_id = ANY(@ids)", connection))
{
    command.Parameters.AddWithValue("ids", Enumerable.Range(1, 5).Select(i => Id("user", i)).ToArray());
    Console.WriteLine($"user_roles: {await command.ExecuteScalarAsync()} registros de ejemplo");
}
