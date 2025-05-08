﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_estoque.Models
{
    public abstract class Produto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public int TipoProduto { get; set; }


        public abstract string Tipo();

        [NotMapped]
        [JsonIgnore]
        public EstoqueProduto EstoqueProduto { get; set; }


    }
}
