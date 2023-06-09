﻿using AutoMapper;
using Data.DTOs.Abrigos;
using Data.DTOs.Pets;
using Models.Models;

namespace AdoPet.Profiles;

/// <summary>
/// Classe de profile para Abrigo
/// </summary>
public class AbrigoProfile : Profile
{
    /// <summary>
    /// Metodo construtor para mapeamento entre os DTOs e o cadastro de Abrigo
    /// </summary>
    public AbrigoProfile()
    {
        CreateMap<CreateAbrigoDto, Abrigo>();
        CreateMap<UpdateAbrigoDto, Abrigo>();
        CreateMap<Abrigo, UpdateAbrigoDto>();
        CreateMap<Abrigo, ReadAbrigoDto>();
    }
}
