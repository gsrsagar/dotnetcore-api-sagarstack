using api_web_new.DTO.Response;

namespace api_web_new.Services
{
    public interface IProductService
    {
        public Task<ProductResponseDTO> GetAll();
    }
}
