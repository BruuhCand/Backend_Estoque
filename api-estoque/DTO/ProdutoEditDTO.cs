using api_estoque.Models;

namespace api_estoque.DTO
{
    public class ProdutoEditDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int CategoriaId { get; set; }
    }
}
