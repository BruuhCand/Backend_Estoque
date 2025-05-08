using api_estoque.Models;

namespace api_estoque.Padroes.Memento
{
    public interface IProdutoMementoRepository
    {
        void SaveMemento(Produto produto);
        ProdutoMemento GetLastMemento(int id);
    }
}
