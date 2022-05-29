using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDemo.Commands
{
    public class LoadEmployeesCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            await Stores.EmployeesStore.Load();
        }
    }
}
