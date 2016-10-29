using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> GetTop(int topCount)
        {
            return this.All().OrderByDescending(p => p.ProductId).Take(topCount);
        }

        public override void Delete(Product product)
        {
            product.IsDelete = true;
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}