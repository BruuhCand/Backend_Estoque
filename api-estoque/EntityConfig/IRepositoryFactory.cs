using api_estoque.Interface;
using api_estoque.Padroes.Memento;

namespace api_estoque.EntityConfig
{
    public interface IRepositoryFactory
    {
        IProdutoRepository ProdutoRepository();
        ICategoriaRepository CategoriaRepository();
        IEstoqueRepository EstoqueRepository();
        IUserRepository UserRepository();
        IMovimentacaoRepository MovimentacaoRepository();
        IValidadeRepository ValidadeRepository();
        IEstoqueProdutoRepository EstoqueProdutoRepository();
        IProdutoMementoRepository ProdutoMementoRepository();
    }
}
