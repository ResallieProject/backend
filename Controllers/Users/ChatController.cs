using Microsoft.AspNetCore.Mvc;

namespace Resallie.Controllers.Users;

public class ChatController: BaseController
{
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            // await Echo(webSocket);
        }
        else
        {
            BadRequest();
        }
    }
}