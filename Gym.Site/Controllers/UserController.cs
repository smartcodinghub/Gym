using Gym.Application.Services.UserManagement;
using Gym.Domain;
using MediatR;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Gym.Site.Controllers
{
    public class UserController : ApiController
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<HttpResponseMessage> Post([FromBody]InsertUser.InsertUserRequest request)
        {
            InsertUser.InsertUserResult response = await mediator.Send(request);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(response))
            };
        }
    }
}
