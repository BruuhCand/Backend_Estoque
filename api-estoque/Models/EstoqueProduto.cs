using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_estoque.Models
{
    public class EstoqueProduto
    {
        public int Id { get; set; }
        public int EstoqueId { get; set; }
        public Estoque Estoque { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public double Preco { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<Validade>? Validades { get; set; }
    }
}
