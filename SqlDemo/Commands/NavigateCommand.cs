using SqlDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDemo.Commands
{
    public class NavigateCommand : CommandBase
    {
        public Func<ViewModelBase> CreateViewModel { get; }
        public NavigateCommand(Func<ViewModels.ViewModelBase> createViewModel)
        {
            CreateViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            Stores.NavigationStore.CurrentViewModel = CreateViewModel?.Invoke();
        }
    }
}
