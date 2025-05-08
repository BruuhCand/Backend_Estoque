using api_estoque.EntityConfig;
using api_estoque.Models;

namespace api_estoque.Padroes.Memento
{
    public class ProdutoMementoRepository : IProdutoMementoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoMementoRepository(AppDbContext context) { 
        _context = context;
        }

        public ProdutoMemento GetLastMemento(int id)
        {
            return _context.ProdutoMemento
            .Where(m => m.ProdutoId == id)
            .OrderBy(m => m.MementoId)  
            .LastOrDefault();
        }

        public void SaveMemento(Produto produto)
        {
            try
            {
                ProdutoMemento prod = new ProdutoMemento(produto);

                _context.ProdutoMemento.Add(prod);
                _context.SaveChanges();

            }
            catch 
            {
                throw new Exception("Erro ao salvar memento");
            }
        }
    }
}
