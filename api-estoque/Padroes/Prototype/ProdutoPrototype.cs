using api_estoque.Models;
using api_estoque.Padroes.Factory;

namespace api_estoque.Padroes.Prototype
{
    public class ProdutoPrototype : IPrototype<Produto>
    {
        private readonly Produto _produtoBase;

        public ProdutoPrototype(Produto produtoBase)
        {
            _produtoBase = produtoBase;
        }

        public Produto Clonar()
        {
            Produto clone = _produtoBase.TipoProduto == 1 ? ProdutoFactory.CriarProduto("perecivel") : ProdutoFactory.CriarProduto("basic");

            clone.Nome = _produtoBase.Nome;
            clone.Descricao = _produtoBase.Descricao;
            clone.CategoriaId = _produtoBase.CategoriaId;
            clone.TipoProduto = _produtoBase.TipoProduto;

            return clone;
        }
    }
}
