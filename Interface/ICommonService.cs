using WebApplicationBackend.DTOs;

namespace WebApplicationBackend.Interface
{
    public interface ICommonService<T,TI,TU>
    {
        public List<string> Errors { get; }

        Task<IEnumerable<T>> GetBeers();
        Task<T> GetById(int id);
        Task<TI> CreateBeer(TI beerInsertDto);
        Task<TI> UpdateBeer(int id, TU beerUpdateDto);
        Task<IEnumerable<T>> DeleteBeer(int id);

        bool Validate(TI dto);
        bool Validate(TU dto);
    }
}
