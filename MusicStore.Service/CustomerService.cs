using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service
{
    public class CustomerService : ICustomerService
    {
        private MusicStoreDBContext dbContext;
        public CustomerService(MusicStoreDBContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return this.dbContext.Customer;
        }
    }
}
