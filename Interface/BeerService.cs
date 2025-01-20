using Microsoft.EntityFrameworkCore;
using WebApplicationBackend.DTOs;
using WebApplicationBackend.Models;
using WebApplicationBackend.Interface;
using WebApplicationBackend.Repository;
using AutoMapper;

namespace WebApplicationBackend.Interface
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {


        private IRepository<Beer> _beerRepository;

        private IMapper _mapper;

        public List<string> Errors { get; }

        public BeerService(StoreContext context,
            IRepository<Beer> berRepository,
            IMapper mapper)
        {
            
            _beerRepository = berRepository;
            _mapper = mapper;
            Errors= new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> GetBeers()
        {
            var beers= await _beerRepository.GetAsync();

            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);

                return beerDto;
            }

            return null;
        }

        public async Task<BeerInsertDto> CreateBeer(BeerInsertDto beerInsertDto)
        {

            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

                   
            var berrInsertDto = _mapper.Map<BeerInsertDto>(beer);

            return berrInsertDto;
        }

        public async Task<BeerInsertDto> UpdateBeer(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                beer=_mapper.Map<BeerUpdateDto,Beer>(beerUpdateDto, beer);

                _beerRepository.UpdateAsync(beer);

                await _beerRepository.Save();

                var beerInsertDto = _mapper.Map<BeerInsertDto>(beer);

                return beerInsertDto;
            }

            return null;

        }


        public async Task<IEnumerable<BeerDto>>DeleteBeer(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);


                _beerRepository.DeleteAsync(beer);
                await _beerRepository.Save();

                return (IEnumerable<BeerDto>)beerDto;
            }

            return null;
        }

        public bool Validate(BeerInsertDto beerInsertDto)
        {
            if(_beerRepository.Search(b=> b.Name == beerInsertDto.Name).Count()>0)
            {
                Errors.Add("Beer already exists");
                return false;
            }
            return true;
        }

        public bool Validate(BeerUpdateDto beerUpdateDto)
        {
            if (_beerRepository.Search(b => b.Name == beerUpdateDto.Name && beerUpdateDto.Id != b.BeerId).Count() > 0)
            {
                Errors.Add("Beer already exists");
                return false;
            }
            return true;
        }


    }
}
