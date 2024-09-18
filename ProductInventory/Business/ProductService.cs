using DataAccess;
using System.Data.SqlClient;

namespace Business
{
    public class ProductService
    { 
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = await _productRepository.GetAllProducts();
            return products;
        }


        public async Task<Product> CreateProduct(Product newProduct)
        {
            Product product = await _productRepository.Create(newProduct);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            Product updatedProduct = await _productRepository.Update(product);
            return updatedProduct;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            
            return await _productRepository.DeleteProduct(productId);
        }

        public async Task<Product> GetProductById(int productId)
        {

            return await _productRepository.GetProductById(productId);
        }









        //private readonly ConnectionStrings _databaseAccess;

        //public ProductService(string connectionString)
        //{
        //    _databaseAccess = new DatabaseConnection(connectionString);
        //}

        //public List<Product> GetAllProducts()
        //{
        //    var parameters = new SqlParameter[] { };
        //    return _databaseAccess.GetProducts("GetAllProducts", parameters);
        //}

        //public Product GetProductById(int id)
        //{
        //    var parameters = new[]
        //    {
        //        new SqlParameter("@Id", id)
        //    };
        //    var products = _databaseAccess.GetProducts("GetProductById", parameters);
        //    return products.Count > 0 ? products[0] : null;
        //}

        //public void CreateProduct(Product product)
        //{
        //    var parameters = new[]
        //    {
        //        new SqlParameter("@Name", product.Name),
        //        new SqlParameter("@Price", product.Price),
        //        new SqlParameter("@Quantity", product.Quantity),
        //        new SqlParameter("@ProductStatus", product.ProductStatus),
        //        new SqlParameter("@CreationDate", product.CreationDate),
        //        new SqlParameter("@UpdateDate", product.UpdateDate)
        //    };
        //    _databaseAccess.ExecuteNonQuery("CreateProduct", parameters);
        //}

        //public void UpdateProduct(Product product)
        //{
        //    var parameters = new[]
        //    {
        //        new SqlParameter("@Id", product.Id),
        //        new SqlParameter("@Name", product.Name),
        //        new SqlParameter("@Price", product.Price),
        //        new SqlParameter("@Quantity", product.Quantity),
        //        new SqlParameter("@ProductStatus", product.ProductStatus),
        //        new SqlParameter("@UpdateDate", product.UpdateDate)
        //    };
        //    _databaseAccess.ExecuteNonQuery("UpdateProduct", parameters);
        //}

        //public void DeleteProduct(int id)
        //{
        //    var parameters = new[]
        //    {
        //        new SqlParameter("@Id", id)
        //    };
        //    _databaseAccess.ExecuteNonQuery("DeleteProduct", parameters);
        //}
    }

}
