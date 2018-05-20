using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskApi.Dtos;
using TaskApi.Entities;

namespace TaskApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TaskEntity, TaskDto>().ForMember(dest => dest.DaysRemaining,
                opt => opt.MapFrom(src => src.DeadLine.GetDeadLine()));
        }
    }
}
