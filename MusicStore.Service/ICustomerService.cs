using MusicStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
    }
}
