using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDemo.Commands
{
    public class RegisterEmployeeCommand : CommandBase
    {
        private readonly ViewModels.EmployeesVm _employeesVm;
        public RegisterEmployeeCommand(ViewModels.EmployeesVm employeesVm)
        {
            _employeesVm = employeesVm;
        }

        public override void Execute(object parameter)
        {
            Stores.EmployeesStore.RegisterEmployee(new ViewModels.SingleEmploeeVm());
        }
    }
}
