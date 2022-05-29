using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDemo.ViewModels
{
    public class SingleEmploeeVm : ViewModelBase, IDisposable
    {
        public SingleEmploeeVm(Models.Employee model)
        {
            _EmployeeId = model.EmployeeId;
            _FirstName = model.FirstName;
            _LastName = model.LastName;
            _MiddleInitial = model.MiddleInitial;
            PropertyChanged += Stores.EmployeesStore.Empl_PropertyChanged; 
        }

        public SingleEmploeeVm()
        {
            _EmployeeId = -1;
            _FirstName = "xxx";
            _LastName = "xxx";
            _MiddleInitial = null;
            PropertyChanged += Stores.EmployeesStore.Empl_PropertyChanged;
        }
        
        public string AsString()
        {
            return $"{EmployeeId}{FirstName}{MiddleInitial}{LastName}";
        }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(_FirstName) || string.IsNullOrEmpty(_MiddleInitial) || string.IsNullOrEmpty(_LastName));
        }
        public override void Dispose()
        {
            PropertyChanged -= Stores.EmployeesStore.Empl_PropertyChanged; // memory leak?
            base.Dispose();
        }

        private int _EmployeeId;
        public int EmployeeId
        {
            get
            {
                return _EmployeeId;
            }
            set
            {
                _EmployeeId = value;
                this.OnPropertyChanged(nameof(EmployeeId));
            }
        }

        private string _FirstName;
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                this.OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _MiddleInitial;
        public string MiddleInitial
        {
            get
            {
                return _MiddleInitial;
            }
            set
            {
                _MiddleInitial = value;
                this.OnPropertyChanged(nameof(MiddleInitial));
            }
        }

        private string _LastName;
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                this.OnPropertyChanged(nameof(LastName));
            }
        }

    }

}
