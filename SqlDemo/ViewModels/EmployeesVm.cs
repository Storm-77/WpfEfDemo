using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SqlDemo.ViewModels
{
    public class EmployeesVm : ViewModelBase
    {
        public string HeaderText => "employees";

        private ICollectionView _employeesView = CollectionViewSource.GetDefaultView(Stores.EmployeesStore.Employees);
        public ICollectionView Employees => _employeesView;
        public ICommand SaveCommand {get;}
        public ICommand LoadEmployeesCommand { get; }


        private bool EmployeeFilter(object item)
        {
            if (string.IsNullOrEmpty(_searchTerm))
                return true;

            var employee = item as SingleEmploeeVm;
            return employee.AsString().ToUpper().Contains(_searchTerm.ToUpper());
        }
      
        public EmployeesVm()
        {
            SaveCommand = new Commands.NavigateCommand(()=> new AlternativeViewModel());
            LoadEmployeesCommand = new Commands.LoadEmployeesCommand();

            _employeesView.Filter = EmployeeFilter;
            PropertyChanged += EmployeesVm_PropertyChanged;
            LoadEmployeesCommand?.Execute(null);
        }

        public override void Dispose()
        {
            PropertyChanged -= EmployeesVm_PropertyChanged;
            base.Dispose();
        }     

        private void EmployeesVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SearchTerm))
            {
                _employeesView.Refresh();
            }
        }      

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                this?.OnPropertyChanged(nameof(SearchTerm));
            }
        }
    }
}
