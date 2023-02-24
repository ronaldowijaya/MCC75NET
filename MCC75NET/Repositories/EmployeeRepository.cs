using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories.Interface;
using MCC75NET.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Repositories
{
    public class EmployeeRepository : IRepository<string, Employee>
    {
        private readonly MyContext context;

        public EmployeeRepository(MyContext context)
        {
            this.context = context; 
        }
        public int Delete(string key)
        {
            //int result = 0;
            //var employee = GetById(key);
            //if (employee == null)
            //{
            //    return result;
            //}
            //context.Remove(employee);
            //result = context.SaveChanges();
            //return result;
            var entity = GetById(key);
            context.Remove(entity);
            return context.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            return context.Employees.ToList();
        }        

        public Employee GetById(string key)
        {
            return context.Employees.Find(key);

        }

        public int Insert(Employee entity)
        {
            int result = 0;
            context.Add(entity);
            result = context.SaveChanges();
            return result;
        }

        public int Update(Employee entity)
        {
            int result = 0;
            context.Entry(entity).State = EntityState.Modified;
            result = context.SaveChanges();
            return result;
        }

        public List<EmployeeVM> GetEmployees()
        {            
            var results = context.Employees.Select(e=> new EmployeeVM                           
                           {
                               Nik = e.Nik,
                               FirstName = e.FirstName,
                               LastName = e.LastName,
                               BirthDate = e.BirthDate,
                               Gender = e.Gender,
                               HiringDate = e.HiringDate,
                               Email = e.Email,
                               PhoneNumber = e.PhoneNumber
                           }).ToList();
            return results;
        }

        public EmployeeVM GetEmployeeById(string nik)
        {
            var key = GetById(nik);
            var results = new EmployeeVM
            {
                Nik = key.Nik,
                FirstName = key.FirstName,
                LastName = key.LastName,
                BirthDate = key.BirthDate,
                Gender = key.Gender,
                HiringDate = key.HiringDate,
                Email = key.Email,
                PhoneNumber = key.PhoneNumber
            };
            return results;
        }
    }
}
