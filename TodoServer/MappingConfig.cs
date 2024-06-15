using AutoMapper;
using DAL.Entites;
using DAL.Entites.DTO;
using DAL.Entities.DTO;

namespace TodoServer
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserTask, TaskDTO>().ReverseMap();
            CreateMap<UserTask, TaskCreateDTO>().ReverseMap();
            CreateMap<UserTask, UpdateTaskDTO>().ReverseMap();
        }
    }
}
