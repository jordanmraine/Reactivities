using System;
using System.Threading.Tasks;
using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator mediator;

        public ChatHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task SendComment(Create.Command command)
        {
            var comment = await mediator.Send(command);

            await Clients.Group(command.ActivityID.ToString())
                .SendAsync("ReceiveComment", comment.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var activityID = httpContext.Request.Query["activityID"];

            await Groups.AddToGroupAsync(Context.ConnectionId, activityID);

            var result = await mediator.Send(new List.Query{ActivityID = Guid.Parse(activityID)});

            await Clients.Caller.SendAsync("LoadComments", result.Value);
        }
    }
}