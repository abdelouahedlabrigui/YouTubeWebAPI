using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuggingFace;
// using HuggingFace.Hub;
// using HuggingFace.Inference;
// using HuggingFace.Inference.Nlp;
using Microsoft.Extensions.Options;
using YouTubeWebAPI.Models.Prompts.Transformers;
using YouTubeWebAPI.Models.Trading.Alpaca;

namespace YouTubeWebAPI.Repository
{
    public class TextGenerationService
    {
        private readonly HuggingFaceClient _huggingFaceClient;
        private const string TextGenerationModel = "openai-community/gpt2";
        private const string TranslationModel = "Helsinki-NLP/opus-mt-tc-big-en-es";

        public TextGenerationService(IOptions<HuggingFaceSettings> settings, HttpClient httpClient)
        {
            var apiKey = settings.Value.ApiKey ?? throw new ArgumentNullException("Hugging Face API key is missing.");
            _huggingFaceClient = new HuggingFaceClient(apiKey, httpClient);            
        }

        public async Task<List<GeneratedText>> GenerateTextAsync(string text, string actor, string response){
            if(string.IsNullOrEmpty(text) || string.IsNullOrEmpty(actor) || string.IsNullOrEmpty(response))
            {
                throw new ArgumentException("Input(s) are empty.");
            }

            string prompt = $"{text} based on perspective of: {actor}: {response}";
            var generatedRequest = new GenerateTextRequest {
                Inputs = prompt, 
                Parameters = new GenerateTextRequestParameters {
                    MaxNewTokens = 250
                }
            };
            try
            {
                var result = await _huggingFaceClient.GenerateTextAsync(TextGenerationModel, generatedRequest);
                List<GeneratedText> generatedText = result.Select(s => new GeneratedText {Text = s.GeneratedText}).ToList();
                string output = generatedText[0].Text ?? "";
                List<GeneratedText> translatedText = await TranslateTextAsync(text);
                return translatedText ?? new List<GeneratedText>();
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException($"Error generating text: {ex.Message}");
            }
        }

        public async Task<List<GeneratedText>> TranslateTextAsync(string text) {
            if(string.IsNullOrEmpty(text)){
                throw new ArgumentNullException("Input text is empty.");
            }

            var translationRequest = new GenerateTextRequest {
                Inputs = text
            };

            try
            {
                var result = await _huggingFaceClient.GenerateTextAsync(TranslationModel, translationRequest);
                return result?.Select(s => new GeneratedText { Text = text, EsTranslation = s.GeneratedText }).ToList() ?? new List<GeneratedText>();   
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException($"Error translating text: {ex.Message}");
            }
        }   
    }
}