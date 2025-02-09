# ConfigService

## About

ConfigService is a minimal ASP.NET Core web API designed to retrieve configuration secrets from HashiCorp Vault using VaultSharp. It serves as a foundational microservice for managing configuration secrets in a microservices architecture.  


## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Implementation Details](#implementation-details)
- [Usage](#usage)
- [Testing](#testing)
- [Contribution Guidelines](#contribution-guidelines)
- [License](#license)

---

## Prerequisites

Before setting up the project, ensure the following are installed:

1. **.NET SDK 8.0 or later**  
   Download and install the .NET SDK from the official [.NET Download Page](https://dotnet.microsoft.com/download).

2. **HashiCorp Vault**  
   - Install HashiCorp Vault locally: [Vault Installation Guide](https://developer.hashicorp.com/vault/docs/install).
   - Start a development server (for testing):  
     ```bash
     vault server -dev
     ```

3. **VaultSharp Package**  
   The project uses the VaultSharp NuGet package to communicate with HashiCorp Vault.

---

## Installation

1. **Clone the Repository:**

   ```bash
   git clone <repository-url>
   cd ConfigService
   ```

2. **Restore Dependencies:**

   Run the following command to restore all required dependencies:

   ```bash
   dotnet restore
   ```

3. **Build the Project:**

   Build the project using:

   ```bash
   dotnet build
   ```

---

## Configuration

The application retrieves configuration secrets from HashiCorp Vault. Ensure the following settings are properly configured:

- **Vault Address:**  
  Default: `http://127.0.0.1:8200`  
  Override by setting the environment variable: `Vault:Address`.

- **Vault Token:**  
  Default: `myroot`  
  Override by setting the environment variable: `Vault:Token`.

> **Note:** For production, sensitive information like Vault tokens should be managed securely through environment variables, Azure Key Vault, or Kubernetes secrets.

---

## Implementation Details

Key features of the ConfigService:

1. **VaultSharp Integration:**  
   The service integrates with VaultSharp to interact with the HashiCorp Vault API. It uses the Key/Value v2 secrets engine to retrieve secrets.

2. **Minimal API Design:**  
   ConfigService uses the ASP.NET Core minimal API framework for simplicity and lightweight performance.

3. **Error Handling:**  
   The service includes error handling for scenarios like invalid secret paths or Vault connectivity issues. It ensures clear error messages are returned to users.

For more details on the implementation, refer to the [`Program.cs`](Program.cs) file in the repository.

---

## Usage

1. **Run the Service:**

   Start the application with the following command:

   ```bash
   dotnet run
   ```

   By default, the service will be available at `http://localhost:5000`.

2. **Retrieve a Secret:**

   Use any HTTP client (e.g., Postman, curl) to fetch secrets from Vault. Example:

   ```bash
   curl http://localhost:5000/config/secret/data/my-secret
   ```

   Replace `secret/data/my-secret` with the appropriate secret path configured in your Vault.

3. **Expected Response:**

   For a secret stored in Vault as:

   ```json
   {
       "key1": "value1",
       "key2": "value2"
   }
   ```

   The API will return:

   ```json
   {
       "key1": "value1",
       "key2": "value2"
   }
   ```

---

## Testing

- **Verify API Response:**  
  Use test tools like Postman or curl to confirm that secrets are retrieved correctly.

- **Simulate Error Handling:**  
  Test the service with invalid secret paths or by stopping the Vault server to verify the error responses.

---

## Contribution Guidelines

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a feature branch.
3. Submit a pull request with a clear description of the changes.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
```
