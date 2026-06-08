using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public interface IDataIntializer
    {
        Task IntializeAsync();
    }
}
