using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Demo.Products.Interfaces.Model
{
    public interface IProduct
    {

        string Name { get; }
        string Description { get; }
        long ProductId { get; }
        string SKU { get; }
        IBrand Brand { get; }
                       
    }
}
