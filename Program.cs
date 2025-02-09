using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VaultSharp;
using VaultSharp.V1;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

var builder = WebApplication.CreateBuilder(args);

// Retrieve Vault configuration from environment variables or fallback defaults.
string vaultAddress = builder.Configuration["Vault:Address"] ?? "http://127.0.0.1:8200";
string vaultToken = builder.Configuration["Vault:Token"] ?? "myroot";

// Initialize Vault client settings using the Token Authentication Method.
var vaultClientSettings = new VaultClientSettings(vaultAddress, new TokenAuthMethodInfo(vaultToken));

// Register the Vault client as a singleton for dependency injection.
builder.Services.AddSingleton<IVaultClient>(new VaultClient(vaultClientSettings));

var app = builder.Build();

// Define an endpoint to retrieve secrets from Vault.
// The endpoint format: GET /config/{secretPath}
app.MapGet("/config/{secretPath}", async (string secretPath, IVaultClient vaultClient) =>
{
    try
    {
        // Use the Key/Value secrets engine version 2.
        var secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretPath);
        return Results.Ok(secret.Data.Data);
    }
    catch (Exception ex)
    {
        // In production, log the exception and return a meaningful error message.
        return Results.Problem($"Error retrieving secret from Vault: {ex.Message}");
    }
});

app.Run();
