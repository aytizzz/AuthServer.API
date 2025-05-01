//using AuthServer.Core.DTOs.Product;
//using AuthServer.Core.Repositories;
//using AuthServer.Core.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace AuthServer.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductsController : CustomBaseController
//    {
//        private readonly IGenericService<Product, ProductDto> _productService;

//        public ProductsController(IGenericService<Product, ProductDto> productService)
//        {
//            _productService = productService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetProducts()
//        {
//            return ActionResultInstance(await _productService.GetAllAsync());
//        }
//        [HttpPost]
//        public async Task<IActionResult>SaveProduct(ProductDto productDto)
//        {
//            return ActionResultInstance(await _productService.AddAsync(productDto));
//        }

//        [HttpPut]
//        public async Task<IActionResult>UpdateProducts(ProductDto productDto)
//        {
//            return ActionResultInstance(await _productService.Update(productDto,productDto.Id));// bax
//        }

//    }
//}
