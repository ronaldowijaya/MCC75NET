﻿using MCC75NET.Contexts;
using MCC75NET.Controllers;
using MCC75NET.Models;
using MCC75NET.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Repositories
{
    public class UniversityRepository : IRepository<int, University>
    {
        private readonly MyContext context;
        public UniversityRepository(MyContext context)
        {
            this.context=context;
        }
        public int Delete(int key)
        {            
            int result = 0;
            var university = GetById(key);
            if (university == null)
            {
                return result;
            }
            context.Remove(university);
            result = context.SaveChanges();
            return result;                      
        }                

        public List<University> GetAll()
        {
           return context.Universities.ToList();
        }

        public University GetById(int key)
        {
            return context.Universities.Find(key);
        }

        public int Insert(University entity)
        {
            var result = 0;
            context.Add(entity);
            result = context.SaveChanges();
            return result;
        }

        public int Update(University entity)
        {
            var result = 0;
            context.Entry(entity).State = EntityState.Modified;
            result = context.SaveChanges();
            return result;

        }
    }
}
