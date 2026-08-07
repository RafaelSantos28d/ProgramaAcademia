using Academia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Interfaces
{
    public interface IPlanRepository
    {
        Task<Plan> CreatePlan(Plan Plan);
        Task<Plan> GetAll();
        Task<Plan> GetById(int id);
        Task<Plan> Remove(int id);
        Task<Plan> Update(Plan Plan);
    }
}
