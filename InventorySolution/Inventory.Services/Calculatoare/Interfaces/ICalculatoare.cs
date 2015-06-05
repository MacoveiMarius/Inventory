using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{

    public interface ICalculatoare
    {
        List<Calculator> GetCalculatoare();

        Calculator GetCalculator(int id);

        ServiceResult AddCalculator(Calculator calculator);

        ServiceResult UpdateCalculator(Calculator calculator);
    }
}
