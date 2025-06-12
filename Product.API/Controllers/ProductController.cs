using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Interface;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitWork _uow;
        private readonly IMapper _mapper;
        public ProductController(IUnitWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet("get-all-products")]
        public async Task<ActionResult> get()
        {
            var res = await _uow.ProductRepository.GetAllAsync(x => x.Category);
            var result = _mapper.Map<List<ProductDto>>(res);
            return Ok(result);
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<ActionResult> get(int id)
        {
            var res = await _uow.ProductRepository.GetAllAsync(x => x.Category);
            var result = _mapper.Map<List<ProductDto>>(res);
            return Ok(result);
        }

        [HttpPost("add-new-product")]
        public async Task<ActionResult> Post([FromForm] CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var res = await _uow.ProductRepository.AddAsync(productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
