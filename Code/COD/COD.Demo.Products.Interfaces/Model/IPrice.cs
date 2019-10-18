using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Demo.Products.Interfaces.Model
{
    public interface IPrice
    {
        decimal Amount { get; }
        ICurrency Currency { get; }
    }
}
