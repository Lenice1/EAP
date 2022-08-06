using AutoMapper;
using EmployeeAttendancePortal.Web.Data;
using EmployeeAttendancePortal.Web.Models;

namespace EmployeeAttendancePortal.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ELCriteria, ELCriteriaVM>().ReverseMap();
            CreateMap<Employee, EmployeeListVM>().ReverseMap();
            CreateMap<Employee, EmployeeAllocationVM>().ReverseMap();
            CreateMap<OrderAllocation, OrderAllocationVM>().ReverseMap();
            CreateMap<OrderAllocation, OrderAllocationEditVM>().ReverseMap();
            CreateMap<ItemRequest, ItemRequestCreateVM>().ReverseMap();
            CreateMap<ItemRequest, ItemRequestVM>().ReverseMap();


        }
    }
}
