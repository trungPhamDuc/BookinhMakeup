using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Pages.Chat
{
    public class ChatMessage
    {
        public string Role { get; set; }
        public string Text { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly string _systemPrompt;

        public IndexModel(IHttpClientFactory httpClientFactory,
                          IOptions<GeminiApiSettings> googleApiSettings,
                          IOptions<GeminiPromptSettings> promptSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = googleApiSettings.Value.ApiKey;
            _systemPrompt = promptSettings.Value.SystemPrompt;
        }

        [BindProperty]
        public string UserInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<ChatMessage> ConversationHistory { get; set; } = new();
        public void OnGet()
        {
            if (ConversationHistory == null || ConversationHistory.Count == 0)
            {
                ConversationHistory = new List<ChatMessage>
                {
                    new ChatMessage { Role = "model", Text = "Xin chào, tôi có thể giúp gì cho bạn?" }
                };
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(UserInput))
                return Page();

            ConversationHistory.Add(new ChatMessage
            {
                Role = "user",
                Text = UserInput
            });

            try
            {
                var client = _httpClientFactory.CreateClient();
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";

                var contents = new List<object>
                {
                    new
                    {
                        role = "user",
                        parts = new[] { new { text = _systemPrompt } }
                    }
                 };

                contents.AddRange(ConversationHistory.Select(msg => new
                {
                    role = msg.Role,
                    parts = new[] { new { text = msg.Text } }
                }));

                var requestData = new { contents };

                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ConversationHistory.Add(new ChatMessage
                    {
                        Role = "model",
                        Text = "Xin lỗi, chúng tôi không thể phản hồi ngay lúc này. Vui lòng thử lại sau."
                    });
                    return Page();
                }

                var jsonObj = JObject.Parse(responseBody);
                var aiText = jsonObj["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString()
                             ?? "Không có phản hồi từ AI.";

                ConversationHistory.Add(new ChatMessage
                {
                    Role = "model",
                    Text = aiText
                });

                UserInput = string.Empty;
            }
            catch (Exception)
            {
                ConversationHistory.Add(new ChatMessage
                {
                    Role = "model",
                    Text = "Xin lỗi, chúng tôi không thể phản hồi ngay lúc này. Vui lòng thử lại sau."
                });
            }

            return Page();
        }

    }
}
