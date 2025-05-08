using api_estoque.DTO;
using api_estoque.Models;

namespace api_estoque.Padroes.Facade
{
    public interface IProdutoFacade
    {
        ProdutoDTO EntradaProduto(EntradaDTO entrada);
        bool SaidaProduto(SaidaDTO saida);
        void Edit(ProdutoEditDTO produto);
        Produto SetLastVersion(int ProdutoId);
    }
}
