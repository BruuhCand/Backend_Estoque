using api_estoque.EntityConfig;
using api_estoque.Interface;
using api_estoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_estoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        public MovimentacaoController(IRepositoryFactory repositoryFactory)
        {
            _movimentacaoRepository = repositoryFactory.MovimentacaoRepository();
        }

        [HttpGet]
        public ActionResult<List<Movimentacao>> GetAll()
        {
            var movimentacoes = _movimentacaoRepository.GetAll();
            return Ok(movimentacoes);
        }

        [HttpGet("produto/{idProduto}")]
        public ActionResult<List<Movimentacao>> GetByProduto(int idProduto)
        {
            var movimentacoes = _movimentacaoRepository.GetProduto(idProduto);
            return Ok(movimentacoes);
        }

        [HttpGet("tipo/{tipoMovimentacao}")]
        public ActionResult<List<Movimentacao>> GetByTipo(string tipoMovimentacao)
        {
            var movimentacoes = _movimentacaoRepository.GetTipo(tipoMovimentacao);
            return Ok(movimentacoes);
        }
    }
}
