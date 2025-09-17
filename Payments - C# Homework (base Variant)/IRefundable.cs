using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments___C__Homework__base_Variant_
{
    public interface IRefundable
    {
        void Refund(decimal amount);
    }
}
