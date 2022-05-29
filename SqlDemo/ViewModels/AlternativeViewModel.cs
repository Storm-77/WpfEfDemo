using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SqlDemo.ViewModels
{
    public class AlternativeViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; }
        public AlternativeViewModel()
        {
            GoBackCommand = new Commands.NavigateCommand(() => new EmployeesVm());
        }

    }
}
