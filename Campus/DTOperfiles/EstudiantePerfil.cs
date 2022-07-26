﻿using AutoMapper;
using Campus.DTO;
using Campus.Models;

namespace Campus.DTOperfiles
{
    public class EstudiantePerfil : Profile
    {
        public EstudiantePerfil()
        {
            CreateMap<Estudiante, EstudianteReadDTO>();
            CreateMap<Perfil, PerfilReadDTO>();
            CreateMap<PerfilCreateDTO, Perfil>();
            CreateMap<EstudiantePublisherDTO, Estudiante>()
                .ForMember(
                    destino => destino.fci, 
                    opcion => opcion.MapFrom(fuente => fuente.ci)
                );
        }
    }
}
