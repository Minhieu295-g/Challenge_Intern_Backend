using MyAppDemo.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace MyAppDemo.Helpers.Document
{
    public class ProductExample : IExamplesProvider<Product>
    {
        public Product GetExamples()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smart TV",
                Price = 29999
            };
        }
    }

    public class ProductResponseExample : IExamplesProvider<ApiResponse<Product>>
    {
        public ApiResponse<Product> GetExamples()
        {
            return new ApiResponse<Product>
            {
                Status = HttpStatusCode.OK,
                Success = true,
                Message = "Get product successfully",
                Data = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart TV",
                    Price = 29999
                }
            };
        }
    }
    public class BadrequestProductResponseExample : IExamplesProvider<ApiResponse<Product>>
    {
        public ApiResponse<Product> GetExamples()
        {
            return new ApiResponse<Product>
            {
                Status = HttpStatusCode.BadRequest,
                Success = false,
                Message = "Invalid product ID format",
                Data = null
            };
        }
    }
    public class NotFoundProductResponseExample : IExamplesProvider<ApiResponse<Product>>
    {
        public ApiResponse<Product> GetExamples()
        {
            return new ApiResponse<Product>
            {
                Status = HttpStatusCode.NotFound,
                Success = false,
                Message = "Product not found",
                Data = null
            };
        }
    }
    public class ListProductResponseExample : IExamplesProvider<ApiResponse<List<Product>>>
    {
        public ApiResponse<List<Product>> GetExamples()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "TV 360",
                Price = 35000
            });
            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Xe May",
                Price = 125000
            });
            return new ApiResponse<List<Product>>
            {
                Status = HttpStatusCode.OK,
                Success = true,
                Message = "Get list products successfully",
                Data = products
            };
        }
    }
    public class ProductVMExample : IExamplesProvider<ProductVM>
    {
        public ProductVM GetExamples()
        {
            return new ProductVM
            {
                Name = "Smartphone",
                Price = 15000
            };
        }
    }

    public class AddProductResponseExample : IExamplesProvider<ApiResponse<Product>>
    {
        public ApiResponse<Product> GetExamples()
        {
            return new ApiResponse<Product>
            {
                Status = HttpStatusCode.Created,
                Success = true,
                Message = "Created product successfully",
                Data = new Product
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "Smartphone",
                    Price = 15000
                }
            };
        }
    }
    public class UpdateProductResponseExample : IExamplesProvider<ApiResponse<Product>>
    {
        public ApiResponse<Product> GetExamples()
        {
            return new ApiResponse<Product>
            {
                Status = HttpStatusCode.Created,
                Success = true,
                Message = "Update product successfully",
                Data = new Product
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "Smartphone",
                    Price = 15000
                }
            };
        }
    }
}
