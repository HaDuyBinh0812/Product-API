using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.MyHelper;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Core.Sharing;

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
        public async Task<ActionResult> Get([FromQuery] ProductParams productParams)
        {

            var src = await _uow.ProductRepository.GetAllAsync(productParams);
            var result = _mapper.Map<IReadOnlyList<ProductDto>>(src.ProductDtos);

            return Ok(new Pagination<ProductDto>(productParams.Pagesize, productParams.PageNumber, src.TotalItems, result));
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

        [HttpPut("update-exiting-product/{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.UpdateAsync(id, productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("delete-exiting-product/{id}")]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.DeleteAsync(id);
                    return res ? Ok(res) : BadRequest(res);
                }
                return NotFound($"this id={id} not found");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
