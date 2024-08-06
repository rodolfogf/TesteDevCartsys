using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Services;

namespace TesteDevCartsys.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
        /*esse DTO será convertido para um usuário efetivamente, 
         o que será feito usando o AutoMaper e os profiles*/
        {
            await _usuarioService.Cadastra(dto);
            return Ok("Usuário Cadastrado!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
        {
            var token = await _usuarioService.Login(dto);
            return Ok(token);
        }
    }
}

