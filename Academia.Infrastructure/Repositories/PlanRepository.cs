using Academia.Domain.Entities;
using Academia.Domain.Interfaces;
using Academia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly BancoContext _context;
        public PlanRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<Plan> CreatePlan(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
            return plan;
        }

        public async Task<IEnumerable<Plan>> GetAll()
        {
            var plans = await _context.Plans.ToListAsync();
            return plans;
        }

        public async Task<Plan> GetById(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            return plan;

        }

        public async Task<bool> Remove(int id)
        {
            var plan = await GetById(id);
            if(plan == null)
            {
                return false;
            }

            _context.Plans.Remove(plan);
            return true;

        }

        public Plan Update(Plan plan)
        {
            _context.Plans.Update(plan);
            return plan;
        }
    }
}
