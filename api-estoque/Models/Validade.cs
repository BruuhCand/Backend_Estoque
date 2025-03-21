﻿using System.ComponentModel.DataAnnotations.Schema;

namespace api_estoque.Models
{
    public class Validade
    {

        public int Id { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }




        
    }
}
