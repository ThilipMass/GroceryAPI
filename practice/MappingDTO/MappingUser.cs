using AutoMapper;
using practice.models;

public class MappingUser:Profile
{
    public MappingUser()
    {
        CreateMap<RegisterUserDTO,User>();
        
    }
}