using MediatR;
using WebApiCar.Application.Commands;
using WebApiCar.Application.Services;

namespace WebApiCar.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly IAuthService _authService;
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            bool result = await _authService.Login(command.Username, command.Password);

            return result;
        }
    }
}
