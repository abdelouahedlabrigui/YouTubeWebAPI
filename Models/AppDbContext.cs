using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models.Climate;
using YouTubeWebAPI.Models.Documents;
using YouTubeWebAPI.Models.Documents.LanguageFeatures;
using YouTubeWebAPI.Models.Documents.PdfDocuments;
using YouTubeWebAPI.Models.Economics;
using YouTubeWebAPI.Models.NLP;
using YouTubeWebAPI.Models.NLP.Llms;
using YouTubeWebAPI.Models.NLP.Llms.Questions;
using YouTubeWebAPI.Models.Prompts;
using YouTubeWebAPI.Models.Prompts.NewsPrompts;
using YouTubeWebAPI.Models.Prompts.Transformers;
using YouTubeWebAPI.Models.Search;
using YouTubeWebAPI.Models.Search.NewsAPI;
using YouTubeWebAPI.Models.Search.Weather;
using YouTubeWebAPI.Models.Search.Weather.Staistics;
using YouTubeWebAPI.Models.Trading.StockPrice;
using YouTubeWebAPI.Models.Trading.StockPrice.Indicators;
using YouTubeWebAPI.Models.Trading.StockPrice.SARIMA;
using YouTubeWebAPI.Models.NLP.RecursiveSearch.ContextSearch;
using YouTubeWebAPI.Models.NLP.RecursiveSearch.MatchSearch;
using YouTubeWebAPI.Models.NLP.RecursiveSearch;

namespace YouTubeWebAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<SearchLog>? SearchLogs { get; set; }
        public DbSet<SearchedDocument>? SearchedDocuments { get; set; }
        public DbSet<Prompt>? Prompts { get; set; }
        public DbSet<PromptRawResult>? PromptRawResults { get; set; }
        public DbSet<Person>? People { get; set; }
        public DbSet<CityModel>? Cities { get; set; }
        public DbSet<WeatherVisualization>? Weathers { get; set; }
        public DbSet<WindSpeed>? WindSpeeds { get; set; }
        public DbSet<WeatherTemperature>? Temperatures { get; set; }
        public DbSet<Humidity>? Humidities { get; set; }
        public DbSet<LatestNews>? LatestNewsArticles { get; set; }
        public DbSet<TextDocument>? TextDocuments { get; set; }
        public DbSet<StockPrice>? StockPrices { get; set; }
        public DbSet<ArimaVisualization>? ArimaVisualizations { get; set; }
        public DbSet<StockPriceAdfTestCriticalValue>? StockPriceAdfTestCriticalValues { get; set; }
        public DbSet<StockPriceAdfTestResult>? StockPriceAdfTestResults { get; set; }
        public DbSet<StockPriceArimaCoefficient>? StockPriceArimaCoefficients { get; set; }
        public DbSet<StockPriceArimaMetric>? StockPriceArimaMetrics { get; set; }
        public DbSet<FredAPIEndpoint>? FredAPIEndpoints { get; set; }
        public DbSet<SarimaVisualization>? SarimaVisualizations { get; set; }
        public DbSet<StockPriceSarimaCoefficient>? StockPriceSarimaCoefficients { get; set; }
        public DbSet<SarimaMetric>? SarimaMetrics { get; set; }
        public DbSet<TextSearchMatch>? TextSearchMatches { get; set; }
        public DbSet<NounChunk>? NounChunks { get; set; }
        public DbSet<Entity>? Entities { get; set; }
        public DbSet<Sentiment>? Sentiments { get; set; }
        public DbSet<NewsPrompt>? NewsPrompts { get; set; }
        public DbSet<TextSearchConversation> TextSearchConversations { get; set; }
        public DbSet<DbTechnicalIndicatorsVisualization> TechnicalIndicatorsVisualizations { get; set; }
        public DbSet<DbTechnicalIndicator> TechnicalIndicators { get; set; }
        public DbSet<SentimentSearch> SentimentSearches { get; set; }
        public DbSet<StockPricePdfDocument> StockPricePdfDocuments { get; set; }

        public DbSet<LlmTextClassification> LlmTextClassifications { get; set; }
        public DbSet<LlmNamedEntityRecognition> LlmNamedEntityRecognitions { get; set; }
        public DbSet<LlmQuestionAnswering> LlmQuestionAnswerings { get; set; }
        public DbSet<LlmTextSummarization> LlmTextSummarizations { get; set; }
        public DbSet<LlmTextTranslation> LlmTextTranslations { get; set; }
        public DbSet<LlmTextGeneration> LlmTextGenerations { get; set; }
        public DbSet<QuestionsStore> QuestionsStores { get; set; }
        public DbSet<GeneratedText> GeneratedTexts { get; set; }

        public DbSet<BuildingContext>? BuildingContexts { get; set; }
        public DbSet<CompanyContext>? CompanyContexts { get; set; }
        public DbSet<CountryContext>? CountryContexts { get; set; }
        public DbSet<NationalityContext>? NationalityContexts { get; set; }
        public DbSet<NonGpeContext>? NonGpeContexts { get; set; }
        public DbSet<PersonContext>? PersonContexts { get; set; }
        public DbSet<MatchBuildingContext>? MatchBuildingContexts { get; set; }
        public DbSet<MatchCompanyContext>? MatchCompanyContexts { get; set; }
        public DbSet<MatchCountryContext>? MatchCountryContexts { get; set; }
        public DbSet<MatchNationalityContext>? MatchNationalityContexts { get; set; }
        public DbSet<MatchNonGpeContext>? MatchNonGpeContexts { get; set; }
        public DbSet<MatchPersonContext>? MatchPersonContexts { get; set; }
        public DbSet<ExtractEntity>? ExtractEntities { get; set; }
        public DbSet<FeatureExtraction>? FeatureExtractions { get; set; }
        public DbSet<WikiSearch>? WikiSearchs { get; set; }
        public DbSet<QueryResult> QueryResults { get; set; }
        public DbSet<QuestionAnswering> QuestionAnswerings { get; set; }

    }
}