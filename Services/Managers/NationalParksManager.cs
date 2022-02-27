using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Services.DTOs;
using Services.Managers.IManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Managers
{
    public class NationalParksManager : INationalParksManager
    {
        private readonly INationalParksRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParksManager(INationalParksRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }

        public ICollection<NationalParkDTO> GetNationalParks()
        {
            ICollection<NationalPark> parksList = _nationalParkRepository.GetNationalParks();

            ICollection<NationalParkDTO> parksDTOList = _mapper.Map<ICollection<NationalParkDTO>>(parksList);

            return parksDTOList;
        }

        public NationalParkDTO GetNationalPark(int id)
        {
            NationalPark park = _nationalParkRepository.GetNationalPark(id);

            NationalParkDTO parkDTO = _mapper.Map<NationalParkDTO>(park);

            return parkDTO;
        }

        public NationalParkDTO CreateNationalPark(NationalParkDTO parkDTO)
        {
            try
            {
                NationalPark park = _mapper.Map<NationalPark>(parkDTO);

                if (_nationalParkRepository.NationalParkExists(park.Name))
                {
                    throw new Exception("National Park Exists!");
                }

                if (!_nationalParkRepository.CreateNationalPark(park))
                {
                    throw new Exception($"Something went wrong when saving the record { parkDTO.Name }");
                }

                parkDTO = _mapper.Map<NationalParkDTO>(_nationalParkRepository.GetNationalPark(park.Name));

                return parkDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateNationalPark(NationalParkDTO parkDTO)
        {
            try
            {
                NationalPark park = _mapper.Map<NationalPark>(parkDTO);

                if (_nationalParkRepository.NationalParkExists(park.Name))
                {
                    throw new Exception("National Park Exists!");
                }

                if (!_nationalParkRepository.UpdateNationalPark(park))
                {
                    throw new Exception($"Something went wrong when updating the record { parkDTO.Name }");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool NationalParkExists(int id)
        {
            return _nationalParkRepository.NationalParkExists(id);
        }

        public void DeleteNationalPark(int id)
        {
            NationalPark nationalPark = _nationalParkRepository.GetNationalPark(id);

            if (!_nationalParkRepository.DeleteNationalPark(nationalPark))
            {
                throw new Exception($"Something went wrong when deleting the record { nationalPark.Id }");
            }
        }
    }
}
