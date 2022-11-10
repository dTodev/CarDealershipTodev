using CarDealership.Models.Models;

namespace CarDealership.DL.Interfaces
{
    public interface IBrandRepository
    {
        public Task<Brand> CreateBrand(Brand brand);
        public Task<Brand> UpdateBrand(Brand brand);
        public Task<Brand> DeleteBrand(int brandId);
        public Task<IEnumerable<Brand>> GetAllBrands();
        public Task<Brand> GetBrandById(int brandid);
        public Task<Brand> GetBrandByName(string brandName);
    }
}
