using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Api.Controllers
{
    /// <summary>
    /// Classe abstrata base para impelmentação dos controllers
    /// </summary>
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ExecuteCommand(ModelResult result)
        {
            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
