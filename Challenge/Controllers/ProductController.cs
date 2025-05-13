using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using MyAppDemo.Helpers;
using MyAppDemo.Helpers.Document;
using MyAppDemo.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace MyAppDemo.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<Product> products = new List<Product>();
        /// <summary>
        /// Get all products
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     GET /api/products
        /// </remarks>
        /// <response code="200">Get list products successfully</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Product>>), 200)]
        [SwaggerResponseExample(200, typeof(ListProductResponseExample))]
        public ActionResult<ApiResponse<List<Product>>> getAll()
        {
            return Ok(new ApiResponse<List<Product>>
            {
                Status = HttpStatusCode.OK,
                Success = true,
                Message = "Get list products successfully",
                Data = products
            });
        }

        /// <summary>
        /// Get a product by id.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     GET /api/products/{id}
        /// 
        /// Example:
        ///     GET /api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <param name="id">The GUID of the product.</param>
        /// <returns>Returns a product if found, or an error message if not.</returns>
        /// <response code="200">Product found successfully</response>
        /// <response code="400">Invalid product ID format</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Product>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Product>), 400)]
        [ProducesResponseType(typeof(ApiResponse<Product>), 404)]
        [SwaggerResponseExample(200, typeof(ProductResponseExample))]
        [SwaggerResponseExample(400, typeof(BadrequestProductResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundProductResponseExample))]
        public ActionResult<Product> GetById(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {

                return BadRequest(new ApiResponse<object>
                {
                    Status = HttpStatusCode.BadRequest,
                    Success = false,
                    Message = "Invalid product ID format",
                    Data = null
                });
            }

            var product = products.SingleOrDefault(p => p.Id == guid);
            if (product == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Status = HttpStatusCode.NotFound,
                    Success = false,
                    Message = "Product not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<Product>
            {
                Status = HttpStatusCode.OK,
                Success = true,
                Message = "Get product successfully",
                Data = product
            });
        }
        /// <summary>
        /// Add a new product.
        /// </summary>
        /// <param name="productVM">Product data to be added.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Product>), 201)]
        [SwaggerRequestExample(typeof(ProductVM), typeof(ProductVMExample))]
        [SwaggerResponseExample(201, typeof(AddProductResponseExample))]
        public IActionResult addProduct(ProductVM productVM)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productVM.Name,
                Price = productVM.Price,
            };
            products.Add(product);
            return Ok(new
            {
                status = HttpStatusCode.Accepted,
                success = true,
                message = "Created product successfully",
                data = product
            });
        }
        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id">The GUID of the product.</param>
        /// <param name="productEdit">The new product for update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Product>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Product>), 404)]
        [SwaggerRequestExample(typeof(ProductVM), typeof(ProductVMExample))]
        [SwaggerResponseExample(201, typeof(UpdateProductResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundProductResponseExample))]
        public IActionResult updateProduct(string id, ProductVM productEdit)
        {
            var product = products.SingleOrDefault(p => p.Id == Guid.Parse(id));
            if(product == null)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    success = false,
                    message = "Product Not Found"
                });
            }

            product.Name = productEdit.Name;
            product.Price = productEdit.Price;

            return Ok(new
            {
                status = HttpStatusCode.Accepted,
                success = true,
                message = "Updated product successfully",
                data = product
            });
        }

        /// <summary>
        /// Deleted product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult deleteProduct(String id)
        {
            var product = products.SingleOrDefault(p => p.Id == Guid.Parse(id));

            products.Remove(product);

            return Ok(new
            {
                status = HttpStatusCode.OK,
                success = true,
                message = "Deleted product successfully!"
            });
        }

    }
}
