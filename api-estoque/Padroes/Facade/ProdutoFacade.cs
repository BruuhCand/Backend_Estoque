using api_estoque.DTO;
using api_estoque.EntityConfig;
using api_estoque.Interface;
using api_estoque.Models;
using api_estoque.Padroes.Factory;
using api_estoque.Padroes.Memento;
using api_estoque.Padroes.Prototype;
using api_estoque.Padroes.Singleton;
using api_estoque.Padroes.Strategy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api_estoque.Padroes.Facade
{
    public class ProdutoFacade : IProdutoFacade
    {
            private readonly IProdutoRepository _produtoRepository;
            private readonly IEstoqueProdutoRepository _estoqueProdutoRepository;
            private readonly IValidadeRepository _validadeRepository;
            private readonly ICategoriaRepository _categoriaRepository;
            private readonly IProdutoMementoRepository _produtoMementoRepository;
            private AppDbContext _context;
        public ProdutoFacade( AppDbContext context, IProdutoRepository produto, IEstoqueProdutoRepository estoque, IValidadeRepository validade, IProdutoMementoRepository produtoMemento) {
            _context = context;
            _produtoRepository = produto;
            _estoqueProdutoRepository = estoque;
            _validadeRepository = validade;
            _produtoMementoRepository = produtoMemento;
        }

        public void Edit(ProdutoEditDTO produto)
        {
            try
            {

                Produto produtoBanco = _context.Produto.AsNoTracking().Include(e => e.EstoqueProduto).FirstOrDefault(p => p.Id == produto.Id);

                var prototipo = new ProdutoPrototype(produtoBanco);
                var editProd = prototipo.Clonar();

                editProd.Id = produto.Id;
                editProd.Descricao = produto.Descricao;
                editProd.CategoriaId = produto.CategoriaId;
                editProd.Nome = produto.Nome;

                Produto produtoAtt = _produtoRepository.EditProduto(editProd);
                EstoqueProduto estoqueprod = _estoqueProdutoRepository.Edit(produtoAtt.Id, produto.Preco);

                _produtoMementoRepository.SaveMemento(produtoBanco);


            }
            catch (Exception ex) {
                throw ex;
            }

        }

        

        public ProdutoDTO EntradaProduto(EntradaDTO entrada)
        {

            try
            {
                Produto newProduto = entrada.TipoProduto == 1 ? ProdutoFactory.CriarProduto("perecivel") : ProdutoFactory.CriarProduto("basic");

                newProduto.Id = entrada.Id;
                newProduto.Descricao = entrada.Descricao;
                newProduto.TipoProduto = entrada.TipoProduto;
                newProduto.CategoriaId = entrada.CategoriaId;
                newProduto.Nome = entrada.Nome;


                Produto prodcriado = _produtoRepository.EntradaProduto(newProduto);
                EstoqueProduto estoqueProduto = _estoqueProdutoRepository.Entrada(prodcriado.Id, entrada.Quantidade, entrada.Preco);

                Validade val = null;
                if (entrada.TipoProduto == 1)
                {
                    val = _validadeRepository.Save(estoqueProduto.Id, entrada.DataValidade, entrada.Quantidade);
                }


                var movimentacao = new MovimentacaoContext();
                movimentacao.SetStrategy(new MovimentacaoEntradaStrategy(_context));
                movimentacao.SalvarMovimentacao(estoqueProduto.Id, entrada.Quantidade); 

                return new ProdutoDTO
                {
                    Id = newProduto.Id,
                    Nome = newProduto.Nome,
                    Descricao = newProduto.Descricao,
                    CategoriaId = newProduto.CategoriaId,
                    Preco = estoqueProduto.Preco,
                    QuantTotal = estoqueProduto.Quantidade,
                    TipoProduto = newProduto.TipoProduto,
                    Validades = newProduto.TipoProduto == 1 ? _validadeRepository.GetValidadeList(estoqueProduto.Id) : null
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool SaidaProduto(SaidaDTO saida)
        {
            try
            {
                EstoqueProduto estoqProd = _context.EstoqueProdutos.FirstOrDefault(e => e.ProdutoId == saida.Id && e.EstoqueId == EstoqueSingleton.Instance.Estoque.Id);

                if (estoqProd != null && estoqProd.Quantidade >= saida.Quantidade) {

                    EstoqueProduto estprodAtt = _estoqueProdutoRepository.Saida(saida.Id, saida.Quantidade);
                    bool val = _validadeRepository.Saida(estoqProd.Id, saida.Quantidade);


                    var movimentacao = new MovimentacaoContext();
                    movimentacao.SetStrategy(new MovimentacaoSaidaStrategy(_context));
                    movimentacao.SalvarMovimentacao(saida.Id, saida.Quantidade);

                    return val;                
                }
                return false;
            }
            catch (Exception e) 
            {
                throw new Exception("Quantidade de Saida maior que quantidade no estoque", e);
            }
        }

        public Produto SetLastVersion(int ProdutoId)
        {
            try
            {
                ProdutoMemento lastVersion = _produtoMementoRepository.GetLastMemento(ProdutoId);

                if (lastVersion != null) {
                    Produto produto = alterTypeProduto(lastVersion);
                    Produto produtoAtt = _produtoRepository.EditProduto(produto);
                    EstoqueProduto estoqueprod = _estoqueProdutoRepository.Edit(produtoAtt.Id, lastVersion.Preco);

                    return produtoAtt;
                }
                else
                {
                    throw new ("Produto em sua ultima versão");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar ultima versão do produto", e);
            }
        }

        private Produto alterTypeProduto(ProdutoMemento produtoMemento)
        {
            try
            {
                Produto prod = produtoMemento.TipoProduto == 1 ? ProdutoFactory.CriarProduto("perecivel") : ProdutoFactory.CriarProduto("basic");

                prod.Id = produtoMemento.ProdutoId;
                prod.TipoProduto = produtoMemento.TipoProduto;
                prod.Nome = produtoMemento.Nome;
                prod.Descricao = produtoMemento.Descricao;
                prod.CategoriaId = produtoMemento.CategoriaId;

                return prod;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao alterar tipo do produto", e);
            }
        }
    }
}
