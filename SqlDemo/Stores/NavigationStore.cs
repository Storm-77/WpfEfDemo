using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDemo.Stores
{
    public static class NavigationStore
    {
        private static ViewModels.ViewModelBase _currentViewModel;

        public static ViewModels.ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public static event Action CurrentViewModelChanged;
    }
}
