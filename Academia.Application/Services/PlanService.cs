using Academia.Application.DTOs.Plan;
using Academia.Application.Interfaces;
using Academia.Domain.Entities;
using Academia.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Academia.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePlan> Create(CreatePlan createPlan)
        {
            var create =  _mapper.Map<Plan>(createPlan);

            var created = await _unitOfWork.PlanRepository.CreatePlan(create);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ResponsePlan>(created);
        }

        public async Task<IEnumerable<ResponsePlan>> GetAll()
        {
            var plans = await _unitOfWork.PlanRepository.GetAll();
            return _mapper.Map<IEnumerable<ResponsePlan>>(plans);
        }

        public async Task<ResponsePlan> GetById(int id)
        {
            var plan = await _unitOfWork.PlanRepository.GetById(id);
            return _mapper.Map<ResponsePlan>(plan);
        }

        public async Task<bool> Remove(int id)
        {
            var result = await _unitOfWork.PlanRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return result;
        }
        public async Task<ResponsePlan> Update(UpdatePlan updatePlan)
        {
            var plan = await _unitOfWork.PlanRepository.GetById(updatePlan.PlanId);

            plan.AlterarDados(
                updatePlan.Name,
                updatePlan.Price,
                updatePlan.DurationDays
                );

            await _unitOfWork.CommitAsync();
            return _mapper.Map<ResponsePlan>(plan);
        }
    }
}
