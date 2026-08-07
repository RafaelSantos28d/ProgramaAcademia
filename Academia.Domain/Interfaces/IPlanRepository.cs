using Academia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface IPlanRepository
    {
        Task<Plan> CreatePlan(Plan plan);
        Task<IEnumerable<Plan>> GetAll();
        Task<Plan> GetById(int id);
        Task<bool> Remove(int id);
        Plan Update(Plan plan);
    }
}
