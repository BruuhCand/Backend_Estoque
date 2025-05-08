using api_estoque.Models;

namespace api_estoque.Padroes.Memento
{
    public class ProdutoMemento
    {
        public int? MementoId { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public int TipoProduto { get; set; }
        public double Preco { get; set; }


        public ProdutoMemento(Produto prod) {
            ProdutoId = (int) prod.Id;
            Nome = prod.Nome;
            Descricao = prod.Descricao;
            CategoriaId = prod.CategoriaId;
            TipoProduto = prod.TipoProduto;
            Preco = prod.EstoqueProduto != null ? prod.EstoqueProduto.Preco : 0;
        }

        public ProdutoMemento() { }

    }
}
