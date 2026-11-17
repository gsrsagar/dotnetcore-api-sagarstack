using api_web_new.DTO.Response;

namespace api_web_new.Services
{
    public class ProductService : IProductService
    {
        public Task<ProductResponseDTO> GetAll()
        {
            return Task.FromResult(new ProductResponseDTO
            {
                Id = 1,
                Name = "iPhone 16 Pro"
            });
        }
    }
}
