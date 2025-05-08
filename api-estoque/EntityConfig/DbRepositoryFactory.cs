﻿using api_estoque.Interface;
using api_estoque.Padroes.Memento;
using api_estoque.Repository;


namespace api_estoque.EntityConfig
{

    public class DbRepositoryFactory : IRepositoryFactory
    {

        private readonly AppDbContext _context;

        public DbRepositoryFactory(AppDbContext context)
        {
            _context = context;
     
        }

        public ICategoriaRepository CategoriaRepository() => new CategoriaRepository(_context);
        public IEstoqueRepository EstoqueRepository() => new EstoqueRepository(_context);
        public IUserRepository UserRepository() => new UserRepository(_context, EstoqueRepository());
        public IMovimentacaoRepository MovimentacaoRepository() => new MovimentacaoRepository(_context);
        public IValidadeRepository ValidadeRepository() => new ValidadeRepository(_context);
        public IProdutoRepository ProdutoRepository() => new ProdutoRepository(_context, MovimentacaoRepository(), ValidadeRepository());
        public IEstoqueProdutoRepository EstoqueProdutoRepository() => new EstoqueProdutoRepository(_context);
        public IProdutoMementoRepository ProdutoMementoRepository() => new ProdutoMementoRepository(_context);

    }
}
