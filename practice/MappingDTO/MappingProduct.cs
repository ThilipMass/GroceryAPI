using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using practice.models;


public class MappingProduct : Profile
{
    public MappingProduct()
    {
        CreateMap<ProductDTO, Products>();

    }
}
