using Application.Cqrs.Commands.Auth;
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet(nameof(GoogleLogin))]
        //public async Task GoogleLogin()
        //{
        //    await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
        //        new AuthenticationProperties { RedirectUri = "/callback" });
        //}

        //[Authorize]
        //[HttpGet(nameof(OnGoogleGetCallback))]
        //public async Task<IActionResult> OnGoogleGetCallback([FromServices] UserManager<Account> userManager, [FromServices] SignInManager<Account> signInManager, [FromServices] ITokenService tokenService)
        //{
        //    var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        //    var resultDTO = await ExternalAuthService.CheckGoogleCallback(authenticateResult, userManager, signInManager, tokenService);
        //    return Ok(resultDTO);
        //}

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] UserLoginAuthCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost(nameof(RegisterPrivateUser))]
        public async Task<IActionResult> RegisterPrivateUser([FromBody] PrivateUserRegisterAuthCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost(nameof(RegisterCompanyUser))]
        public async Task<IActionResult> RegisterCompanyUser([FromBody] CompanyUserRegisterAuthCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}