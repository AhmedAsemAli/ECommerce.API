using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Exceptions
{
    public abstract class NotFoundException(string message):Exception(message)
    {

    }
    public sealed class ProductNotFoundException(int id): NotFoundException($"product with Id:{id} is not found ") { }
    public sealed class BasketNotFoundException(string id): NotFoundException($"Basket with Id:{id} is not found ") { }
}
