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
    public class TrailsManager : ITrailsManager
    {
        private readonly ITrailsRepository _trailsRepository;
        private readonly IMapper _mapper;

        public TrailsManager(ITrailsRepository trailsRepository, IMapper mapper)
        {
            _trailsRepository = trailsRepository;
            _mapper = mapper;
        }

        public ICollection<TrailDTO> GetTrails()
        {
            ICollection<Trail> trailsList = _trailsRepository.GetTrails();

            ICollection<TrailDTO> trailsDTOList = _mapper.Map<ICollection<TrailDTO>>(trailsList);

            return trailsDTOList;
        }

        public TrailDTO GetTrail(int id)
        {
            Trail trail = _trailsRepository.GetTrail(id);

            TrailDTO trailDTO = _mapper.Map<TrailDTO>(trail);

            return trailDTO;
        }

        public TrailDTO CreateTrail(TrailDTO traildDTO)
        {
            try
            {
                Trail trail = _mapper.Map<Trail>(traildDTO);

                if (_trailsRepository.TrailExists(trail.Name))
                {
                    throw new Exception("Trail Exists!");
                }

                if (!_trailsRepository.CreateTrail(trail))
                {
                    throw new Exception($"Something went wrong when saving the record { traildDTO.Name }");
                }

                traildDTO = _mapper.Map<TrailDTO>(_trailsRepository.GetTrail(trail.Name));

                return traildDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateTrail(TrailDTO trailDTO)
        {
            try
            {
                Trail trail = _mapper.Map<Trail>(trailDTO);

                if (_trailsRepository.TrailExists(trail.Name))
                {
                    throw new Exception("National Park Exists!");
                }

                if (!_trailsRepository.UpdateTrail(trail))
                {
                    throw new Exception($"Something went wrong when updating the record { trailDTO.Name }");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TrailExists(int id)
        {
            return _trailsRepository.TrailExists(id);
        }

        public void DeleteTrail(int id)
        {
            Trail trail = _trailsRepository.GetTrail(id);

            if (!_trailsRepository.DeleteTrail(trail))
            {
                throw new Exception($"Something went wrong when deleting the record { trail.Id }");
            }
        }
    }
}
