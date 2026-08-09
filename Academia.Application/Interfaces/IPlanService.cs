using Academia.Application.DTOs.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<ResponsePlan>> GetAll();
        Task<ResponsePlan> GetById(int id);
        Task<ResponsePlan> Create(CreatePlan createPlan);
        Task<bool> Remove(int id);
        Task<ResponsePlan> Update(UpdatePlan updatePlan);
    }
}
