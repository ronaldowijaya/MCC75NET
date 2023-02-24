﻿using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories.Interface;
using MCC75NET.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Repositories
{
    public class EducationRepository : IRepository<int, Education>
    {
        private readonly MyContext context;
        private readonly UniversityRepository universityRepository;


        public EducationRepository(MyContext context, UniversityRepository universityRepository)
        {
            this.context = context;
            this.universityRepository = universityRepository;
        }

        public int Delete(int key)
        {
            int result = 0;
            var education = GetById(key);
            if (education == null)
            {
                return result;
            }
            context.Remove(education);
            result = context.SaveChanges();
            return result; ;
        }

        public List<Education> GetAll()
        {
            return context.Educations.ToList();
        }

        public Education GetById(int key)
        {
            return context.Educations.Find(key);

        }

        public int Insert(Education entity)
        {
            int result = 0;
            context.Add(entity);
            result = context.SaveChanges();
            return result;
        }

        public int Update(Education entity)
        {
            var result = 0;

            context.Entry(entity).State = EntityState.Modified;
            result = context.SaveChanges();
            return result; ;
        }

        public List<EducationUniversityVM> GetEducationUniversities()
        {
            var results = (from e in GetAll()
                           join u in universityRepository.GetAll()
                           on e.UniversityId equals u.Id
                           select new EducationUniversityVM
                           {
                               Id = e.Id,
                               Degree = e.Degree,
                               Gpa = e.Gpa,
                               Major = e.Major,
                               UniversityName = u.Name
                           }).ToList();

            return results;
        }

        public EducationUniversityVM GetEducationUniversitiesById(int id)
        {
            var key = GetById(id);
            var results = new EducationUniversityVM
            {
                Id = key.Id,
                Degree = key.Degree,
                Gpa = key.Gpa,
                Major = key.Major,
                UniversityName = universityRepository.GetById(key.UniversityId).Name
            };
            return results;
        }
    }
}
