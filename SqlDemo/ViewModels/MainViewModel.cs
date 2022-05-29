using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SqlDemo.Stores;

namespace SqlDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel => NavigationStore.CurrentViewModel;
        public ICommand NavigateEmployeesCommand { get; }
        public ICommand NavigateProductsCommand { get; }

        public MainViewModel()
        {
            NavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;
            NavigateEmployeesCommand = new Commands.NavigateCommand(() => new EmployeesVm());
            NavigateProductsCommand = new Commands.NavigateCommand(() => new AlternativeViewModel());
        }
        private void CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}

