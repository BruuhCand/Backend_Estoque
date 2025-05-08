namespace api_estoque.Padroes.Prototype
{
    public interface IPrototype<T>
    {
        T Clonar();
    }
}
