using Academia.Application.DTOs.Enrollment;
using Academia.Application.DTOs.Plan;
using Academia.Application.DTOs.Student;
using Academia.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Mappings
{
    public class DomainMappingProfile :Profile
    {
        public DomainMappingProfile() 
        {
            CreateMap<Student, CreateStudent>().ReverseMap();
            CreateMap<ResponseStudent, CreateStudent>().ReverseMap();
            CreateMap<Student, ResponseStudent>().ReverseMap();
            CreateMap<Student, UpdateDTO>().ReverseMap(); CreateMap<ResponseStudent, UpdateDTO>();

            CreateMap<Enrollment, CreateEnrollment>().ReverseMap();
            CreateMap<ResponseEnrollment, CreateEnrollment>().ReverseMap();
            CreateMap<Enrollment, ResponseEnrollment>().ReverseMap();


            CreateMap<Plan, CreatePlan>().ReverseMap();
            CreateMap<ResponsePlan, CreatePlan>().ReverseMap();
            CreateMap<Plan, ResponsePlan>().ReverseMap();


        }
    }
}
