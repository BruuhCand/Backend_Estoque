﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_estoque.Models
{
    public class Validade
    {

        public int Id { get; set; }

        public int EstoqueProdutoId { get; set; }
        [JsonIgnore]
        public EstoqueProduto EstoqueProduto { get; set; }

        public int Quantidade { get; set; }
        public DateTime? DataValidade { get; set; }




        
    }
}
