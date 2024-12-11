using System.ComponentModel.DataAnnotations;

namespace Codility_Test
{
    public interface IRepository
    {
        ICollection<Store> GetStores(Func<Store, bool> filter, bool includeCustomers = false);
        ICollection<Customer> GetCustomers(int storeId);
        Customer AddCustomer(Customer customer);
    }
    public class Store
    {
        public int StoreId { get; set; }
        public string CountryCode { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
    public class Repository : IRepository
    {
        Customer IRepository.AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        ICollection<Customer> IRepository.GetCustomers(int storeId)
        {
            throw new NotImplementedException();
        }

        ICollection<Store> IRepository.GetStores(Func<Store, bool> filter, bool includeCustomers)
        {
            throw new NotImplementedException();
        }
    }
}