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

        ServiceResult AddCalculator(Calculator calculator);
    }
}
