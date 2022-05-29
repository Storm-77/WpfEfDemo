using Microsoft.EntityFrameworkCore;
using SqlDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SqlDemo.Stores
{
    public static class EmployeesStore
    {
        private static ObservableCollection<SingleEmploeeVm> _employees = new();
        private static Lazy<Task> _initializeLazy;

        static EmployeesStore()
        {
            _initializeLazy = new Lazy<Task>(Initialize);
            _employees.CollectionChanged += _employees_CollectionChanged;
        }

        private static void _employees_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var empl = e.OldItems[0] as ViewModels.SingleEmploeeVm;
                if (empl.IsValid())
                {
                    RemoveRecord(empl);
                }
            }
        }
        private async static Task RemoveRecord(SingleEmploeeVm record)
        {
            try
            {

                if (record.IsValid())
                {
                    using (DbContexts.salesdbContext context = new())
                    {
                        //var elem = await context.Employees.FindAsync(record.EmployeeId);
                        var elem = await context.Employees.FirstAsync(e => e.EmployeeId == record.EmployeeId);
                        context.Employees.Remove(elem);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Record cannot be deleated due to database integrity violation", "Error - cannot be deleated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
        public static async Task Load()
        {
            await _initializeLazy.Value;
            //handle errors
        }

        public static IEnumerable<ViewModels.SingleEmploeeVm> Employees
        {
            get => _employees;
        }

        public static void RegisterEmployee(ViewModels.SingleEmploeeVm empl)
        {
            empl.PropertyChanged += Empl_PropertyChanged;
            _employees.Add(empl);
        }

        public static void Empl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var empl = sender as SingleEmploeeVm;
            int a = 0;
            if (empl.EmployeeId == -1)
            {
                //insert
                using (DbContexts.salesdbContext context = new())
                {
                    var model = new Models.Employee();
                    model.FirstName = empl.FirstName;
                    model.LastName = empl.LastName;
                    model.MiddleInitial = empl?.MiddleInitial;
                    context.Employees.Add(model);
                    context.SaveChanges();
                    empl.EmployeeId = model.EmployeeId;
                }
            }
            else
            {
                //update
                using (DbContexts.salesdbContext context = new())
                {
                    var obj = context.Employees.First(e => e.EmployeeId == empl.EmployeeId);
                    obj.FirstName = empl.FirstName;
                    obj.MiddleInitial = empl.MiddleInitial;
                    obj.LastName = empl.LastName;
                    context.SaveChanges();
                }
            }
        }

        private static async Task Initialize()
        {
            //retrieve all records from the database

            using (DbContexts.salesdbContext context = new())
            {
                IEnumerable<Models.Employee> employees = await context.Employees.ToListAsync();
                foreach (Models.Employee e in employees)
                {
                    //RegisterEmployee(new SingleEmploeeVm(e));
                    _employees.Add(new SingleEmploeeVm(e));
                }
            }
        }
    }
}
