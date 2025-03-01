using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Speech.Synthesis;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Models.Metadata;

namespace YouTubeWebAPI.Controllers.TextToSpeech
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextToSpeechApiController : ControllerBase
    {
        private readonly SpeechSynthesizer? _speechSynthesizer;

        public TextToSpeechApiController()
        {
            _speechSynthesizer = new SpeechSynthesizer();
        }

        [HttpGet("get-files")]
        public IActionResult GetFiles(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !Directory.Exists(filePath))
            {
                return BadRequest("Invalid or non-existent file path.");
            }
            try
            {
                string[] files = Directory.GetFiles(filePath);
                List<PlainTextMetadata> filesList = new List<PlainTextMetadata>();
                foreach (string item in files)
                {
                    if (item.Contains(".txt")){
                        FileInfo fileInfo = new FileInfo(item);
                        filesList.Add(new PlainTextMetadata
                        {
                            FullAddress = fileInfo.FullName,
                            Name = fileInfo.Name,
                            LastWriteTime = fileInfo.LastWriteTime.ToString(),
                            LastAccessTime = fileInfo.LastAccessTime.ToString(),
                            DirectoryName = fileInfo.DirectoryName?.ToString(),
                            Length = fileInfo.Length.ToString()
                        });
                    }
                }
                return Ok(filesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("text-to-speech")]
        public IActionResult TextToSpeech(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return BadRequest(new { Error = "Text is required and cannot be empty." });
            }

            try
            {
                using (var synthesizer = new SpeechSynthesizer())
                {
                    // Set the voice to "Microsoft David Desktop" and configure properties
                    synthesizer.SelectVoice("Microsoft David Desktop");
                    synthesizer.Rate = 4; // Speed of speech
                    synthesizer.Volume = 100; // Volume level (0-100)
                    synthesizer.Speak(text); // Synthesize and play the text
                }

                return Ok(new { Message = "Completed reading the text." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while processing the text-to-speech request.", Details = ex.Message });
            }
        }

        [HttpGet("spanish-text-to-speech")]
        public IActionResult ESTextToSpeech(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return BadRequest(new { Error = "Text is required and cannot be empty." });
            }

            try
            {
                using (var synthesizer = new SpeechSynthesizer())
                {
                    // Set the voice to "Microsoft David Desktop" and configure properties
                    synthesizer.SelectVoice("Microsoft Helena Desktop");
                    synthesizer.Rate = 4; // Speed of speech
                    synthesizer.Volume = 100; // Volume level (0-100)
                    synthesizer.Speak(text); // Synthesize and play the text
                }

                return Ok(new { Message = "Completed reading the text." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while processing the text-to-speech request.", Details = ex.Message });
            }
        }
  

    }
}