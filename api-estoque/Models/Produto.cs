﻿namespace api_estoque.Models
{
    public abstract class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QuantTotal { get; set; }
        public double Preco { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public string TipoProduto { get; set; }


        public abstract string Tipo();

    }
}
