using AutoMapper;
using DAL.Entities;
using BLL.DTO;

namespace TodoServer
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserTask, TaskDTO>().ReverseMap();
            CreateMap<UserTask, TaskCreateDTO>().ReverseMap();
            CreateMap<UserTask, UpdateTaskDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
