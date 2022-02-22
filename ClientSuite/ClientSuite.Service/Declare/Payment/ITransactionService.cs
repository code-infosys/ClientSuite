using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface ITransactionService
    {  
        DTResult<TransactionViewModel> GetGrid(DTParameters param);     
        Transaction Get(int id);
        IQueryable<Transaction> GetAll();
        void Insert(Transaction entity);
        void Update(Transaction entity);
        void Delete(Transaction entity);  
    }

}

