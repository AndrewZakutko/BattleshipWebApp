using Application.Entities;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GameDb, Game>();
            CreateMap<CellShipDb, CellShip>();
            CreateMap<FieldDb, Field>();
            CreateMap<CellDb, Cell>();
            CreateMap<ShipDb, Ship>();
            CreateMap<ShootDb, Shoot>();
        }
    }
}