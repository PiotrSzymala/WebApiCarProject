using MediatR;
using WebApiCarProject.Application.Commands;
using WebApiCarProject.Application.Services;

namespace WebApiCarProject.Application.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAuthService _authService;
        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            bool result = await _authService.Register(command.Username, command.Password);

            return result;
        }
    }
}
