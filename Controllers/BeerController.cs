using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBackend.DTOs;
using WebApplicationBackend.Interface;
using WebApplicationBackend.Models;

namespace WebApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {

        private IValidator<BeerInsertDto> _beerInsertValidator;

        private IValidator<BeerUpdateDto> _beerUpdateValidator;

        private ICommonService<BeerDto,BeerInsertDto,BeerUpdateDto> _berreservice;

        public BeerController(IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            [FromKeyedServices("beerService")]ICommonService<BeerDto,BeerInsertDto,BeerUpdateDto> beerService)
        {

            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _berreservice = beerService;
        }


        [HttpGet]
        public async Task<IEnumerable<BeerDto>> GetBeers() =>
            (IEnumerable<BeerDto>)await _berreservice.GetBeers();


        [HttpGet("{Id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _berreservice.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> CreateBeer(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_berreservice.Validate(beerInsertDto))
            {
                return BadRequest(_berreservice.Errors);
            }

            return Ok(await _berreservice.CreateBeer(beerInsertDto));

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> UpdateBeer(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if(!_berreservice.Validate(beerUpdateDto))
            {
                return BadRequest(_berreservice.Errors);
            }  

            var beerDto = await _berreservice.UpdateBeer(id, beerUpdateDto);

            return beerDto == null ? NotFound() : Ok(beerDto);



        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> DeleteBeer(int id)
        {


            var beerDto = await _berreservice.DeleteBeer(id);

            return beerDto == null ? NotFound() : Ok(beerDto);

        }
    }
}
