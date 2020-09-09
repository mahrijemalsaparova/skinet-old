using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
            //  //  Eager loading of navigation properties: 
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            // FindAsync hata verdiği için bunu kullanıyoruz.
            // SingleOrDefault'dan farkı: SingleOrDefault birden fazla aynı değer bulursa hata fırlatır. Fakat FirstOrDefaultAsync ilk ilgili veriyi direkt getirir hata vermez.
            .FirstOrDefaultAsync(p => p.Id==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
            //  Eager loading of navigation properties: 
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();

            // başka ekstra birşey yapmamıza gerek yok bunları include etmemiz yeterli
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
           return await _context.ProductTypes.ToListAsync();
        }
    }
}