using Backend.Models;

namespace Backend.Interfaces;

interface ICustomerRepository
{
    void Create(Customer? customer);
    List<Customer> GetAll();
    Customer? GetById(Guid id);
    void Update(Customer customer);
    void Delete(Guid id);
}

