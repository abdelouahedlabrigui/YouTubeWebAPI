# YouTubeWebAPI

YouTubeWebAPI is a .NET 8.0 Core Web API project designed to integrate various external APIs (e.g., YouTube, Google Search, NewsAPI, Gemini AI, Alpaca Trading, NASA) with natural language processing (NLP) capabilities, text generation, and document management features. It provides endpoints for searching, generating content, managing prompts, and interacting with external services, all while leveraging Entity Framework Core for data persistence and JWT-based authentication for security.

The project is built with modularity and extensibility in mind, incorporating services like text generation, trading data retrieval, and multilingual NLP analysis.

---

## Table of Contents

- [Features](#features)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

---

## Features

- **YouTube API Integration:** Search for videos, shorts, and long-form content with filters like duration and date intervals.
- **Google Search & NewsAPI:** Perform searches and retrieve news articles.
- **Text Generation:** Generate text using external NLP services (e.g., Hugging Face, Mistral AI) and analyze it for entities, sentiments, and multilingual features.
- **Gemini API:** Generate prompts and save them as text or PDF files.
- **Alpaca Trading:** Retrieve trading data using the Alpaca API in a paper trading environment.
- **NASA API:** Integration for fetching NASA-related data.
- **Authentication:** JWT-based authentication for secure endpoint access.
- **CORS Policies:** Configurable CORS for multiple local development origins.
- **Database:** Entity Framework Core with SQL Server for data persistence.
- **Weather Forecast:** A sample endpoint for demonstration purposes.

---

## Project Structure

The project is organized into several key files and folders:

- `Program.cs`: Configures services, middleware, and the HTTP pipeline.
- `Controllers/`:
  - `TextGenerationController.cs`: Handles text generation, entity extraction, sentiment analysis, and question answering.
  - `GeminiApiController.cs`: Manages prompt generation and document handling using the Gemini API.
  - `GoogleSearchApiController.cs`: Provides Google Search functionality.
  - `NewsApiController.cs`: Manages news article retrieval and storage.
  - `YouTubeApiSearchController.cs`: Handles YouTube video searches and authentication.
- `Models/`: Contains data models for API responses, prompts, NLP features, and more.
- `Repository/`: Includes interfaces and implementations for data access and JWT management.
- `Middlewares/`: Custom middleware components (e.g., RouteProvider).

---

## Prerequisites

- .NET 8.0 SDK
- SQL Server (or LocalDB for development)
- API Keys for:
  - YouTube Data API
  - Google Search API
  - NewsAPI
  - Gemini API
  - Alpaca Trading API
  - NASA API
  - Hugging Face API
- Python service running on `http://127.0.0.1:5005` for NLP and text generation (if applicable).

---

## Installation

1. **Clone the Repository:**

```bash
git clone https://github.com/yourusername/YouTubeWebAPI.git
cd YouTubeWebAPI
```
   
2. **Restore Dependencies:**

```bash
dotnet restore
```

3. **Set Up the Database:**

- Update the connection string in `appsettings.json` to point to your SQL Server instance.
- Run migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. **Configure API Keys:**

- Create an `appsettings.json` file in the project root (see Configuration for details).

5. **Build and Run:**

```bash
dotnet build
dotnet run
```

The API will be available at `https://localhost:5082` (or your configured port).

---

## Configuration

- Create an `appsettings.json` file with the following structure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YouTubeWebAPI;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-secret-key-here"
  },
  "YouTubeApiSettings": {
    "ApiKey": "your-youtube-api-key"
  },
  "Gemini": {
    "ApiKey": "your-gemini-api-key"
  },
  "AlpacaApi": {
    "Key": "your-alpaca-key",
    "Secret": "your-alpaca-secret"
  },
  "NASA": {
    "ApiKey": "your-nasa-api-key"
  },
  "HuggingFace": {
    "ApiKey": "your-hugging-face-api-key"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

- Replace placeholders with your actual API keys and configuration values.

---

## Usage

1. Run the API:
```bash
dotnet run
```
2. Access Swagger UI (Development Mode):
- Open `https://localhost:5082/swagger` in your browser to explore and test endpoints.

3. Example Requests:
- Search YouTube Videos:

```bash
curl -X GET "https://localhost:5082/api/YouTubeApiSearch/search?query=cooking&maxResults=5" -H "Authorization: Bearer YOUR_JWT_TOKEN"
```
- Generate Text:

```bash
curl -X GET "https://localhost:5082/api/TextGeneration/generate-text?text=Hello&actor=User&response=Hi" -H "accept: application/json"
```

- Get News Articles:

```bash
curl -X GET "https://localhost:5082/api/NewsApi/get-news-articles" -H "accept: application/json"
```

---

## API Endpoints

Below is a summary of key endpoints (see Swagger for full details):

- **YouTubeApiSearchController**
    - GET `/api/YouTubeApiSearch/search`: Search YouTube videos (requires JWT).
    - GET `/api/YouTubeApiSearch/SearchShorts`: Search YouTube Shorts.
    - GET `/api/YouTubeApiSearch/GetVideoDetails`: Get details for a specific video.
    - GET `/api/YouTubeApiSearch/authenticate`: Generate a JWT token.
- **TextGenerationController**
    - GET `/api/TextGeneration/get-generated-texts`: Retrieve recent generated texts.
    - GET `/api/TextGeneration/generate-text`: Generate text with translation.
    - GET `/api/TextGeneration/generate-information-extraction-decision`: Extract entities and generate Q&A.
- **GeminiApiController**
    - GET `/api/GeminiApi/generate-prompt`: Generate a prompt and save as text.
    - GET `/api/GeminiApi/generate-prompt-pdf`: Generate a prompt and save as PDF.
    - GET `/api/GeminiApi/get-documents`: Retrieve stored prompts.
- **GoogleSearchApiController**
    - GET `/api/GoogleSearchApi/search`: Perform a Google search (requires JWT).
- **NewsApiController**
    - GET `/api/NewsApi/search`: Search news articles (requires JWT).
    - GET `/api/NewsApi/get-news-articles`: Retrieve stored news articles.

