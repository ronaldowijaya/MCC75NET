using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories.Interface;
using MCC75NET.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Repositories
{
    public class AccountRepository : IRepository<string, Account>
    {
        private readonly MyContext context;

        private readonly EmployeeRepository employeeRepository;


        public AccountRepository(MyContext context, EmployeeRepository employeeRepository)
        {
            this.context = context;
            this.employeeRepository = employeeRepository;

        }
        public int Delete(string key)
        {
            int result = 0;
            var account = GetById;
            if (account == null)
            {
                return result;
            }
            context.Remove(account);
            result = context.SaveChanges();
            return result;
        }

        public List<Account> GetAll()
        {
            return context.Accounts.ToList();
        }

        public Account GetById(string key)
        {
            return context.Accounts.Find(key);

        }

        public int Insert(Account entity)
        {
            int result = 0;
            context.Add(entity);
            result = context.SaveChanges();
            return result;
        }

        public int Update(Account entity)
        {
            int result = 0;
            context.Entry(entity).State = EntityState.Modified;
            result = context.SaveChanges();
            return result;
        }

        public List<AccountEmployeeVM> GetAccountEmployees()
        {
            // Method Syntax
            var results = (context.Accounts.Join(
                context.Employees,
                a => a.EmployeeNik,
                e => e.Nik,
                (a, e) => new AccountEmployeeVM
                {
                    Password = a.Password,
                    EmployeeEmail = e.Email,
                })).ToList();

            return results;
        }

        public AccountEmployeeVM GetByIdAccount(string key)
        {
            var accounts = GetById(key);
            var result = new AccountEmployeeVM
            {
                EmployeeEmail = employeeRepository.GetById(accounts.EmployeeNik).Email,
                Password = accounts.Password,
            };

            return result;
        }

        public bool Login(LoginVM loginVM)
        {
            var getAccounts = context.Employees.Join(
                context.Accounts,
                e => e.Nik,
                a => a.EmployeeNik,
                (e, a) => new LoginVM
                {
                    Email = e.Email,
                    Password = a.Password
                });
            return getAccounts.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password);
        }

        public int Register(RegisterVM registerVM)
        {
            int result = 0;
            University university = new University
            {
                Name = registerVM.UniversityName
            };

            // Bikin kondisi untuk mengecek apakah data university sudah ada
            if (context.Universities.Any(u => u.Name == university.Name))
            {
                university.Id = context.Universities
                    .FirstOrDefault(u => u.Name == university.Name)
                    .Id;
            }
            else
            {
                context.Universities.Add(university);
                result = context.SaveChanges();
            }

            Education education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.Gpa,
                UniversityId = university.Id,

            };
            context.Educations.Add(education);
            context.SaveChanges();

            Employee employee = new Employee
            {
                Nik = registerVM.Nik,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            Account account = new Account
            {
                EmployeeNik = registerVM.Nik,
                Password = registerVM.Password
            };
            context.Accounts.Add(account);
            context.SaveChanges();

            AccountRole accountRole = new AccountRole
            {
                AccountNik = registerVM.Nik,
                RoleId = 2
            };
            context.AccountRoles.Add(accountRole);
            context.SaveChanges();

            Profiling profiling = new Profiling
            {
                EmployeeNik = registerVM.Nik,
                EducationId = education.Id
            };
            context.Profilings.Add(profiling);
            context.SaveChanges();

            return result;
        }

        public UserDataVM GetUserdata(string email)
        {
            /*var userdataMethod = context.Employees
                .Join(context.Accounts,
                e => e.NIK,
                a => a.EmployeeNIK,
                (e, a) => new { e, a })
                .Join(context.AccountRoles,
                ea => ea.a.EmployeeNIK,
                ar => ar.AccountNIK,
                (ea, ar) => new { ea, ar })
                .Join(context.Roles,
                eaar => eaar.ar.RoleId,
                r => r.Id,
                (eaar, r) => new UserdataVM
                {
                    Email = eaar.ea.e.Email,
                    FullName = String.Concat(eaar.ea.e.FirstName, eaar.ea.e.LastName),
                    Role = r.Name
                }).FirstOrDefault(u => u.Email == email);*/

            var userdata = (from e in context.Employees
                            join a in context.Accounts
                            on e.Nik equals a.EmployeeNik
                            join ar in context.AccountRoles
                            on a.EmployeeNik equals ar.AccountNik
                            join r in context.Roles
                            on ar.RoleId equals r.Id
                            where e.Email == email
                            select new UserDataVM
                            {
                                Email = e.Email,
                                FullName = String.Concat(e.FirstName, " ", e.LastName),
                                Role = r.Name
                            }).FirstOrDefault();

            return userdata;
        }


    }
}
