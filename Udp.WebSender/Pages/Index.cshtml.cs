
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Udp.Abstract.Contract;
using Udp.Abstract.Service;
using Udp.Extensions;

namespace Udp.WebSender.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IUdpSpeakerService speaker;

        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public string Status { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IUdpSpeakerService speaker)
        {
            this.logger = logger;
            this.speaker = speaker;
        }

        public IActionResult OnGet() => Page();

        public IActionResult OnPost()
        {
            logger.LogInformation($"Broadcasting message [{Message}] to port {speaker.SpeakerConfig.BroadcastTargetPort}");

            IUdpMessage message = UdpMessageFactory.CreateUdpMessage(Message);

            IUdpTransportMessage response = speaker.BroadcastWithResponse(message, speaker.SpeakerConfig.BroadcastTargetPort);

            Status = $"Received response from {response.Address}:{response.Port}: [{response.Message.Text}]";

            logger.LogInformation(Status);

            return Page();
        }
    }
}
