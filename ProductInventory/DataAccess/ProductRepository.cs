using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ProductRepository
    {
        private readonly ConnectionStrings connections;

        public ProductRepository(IOptions<ConnectionStrings> options)
        {
            connections = options.Value;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (var connection = new SqlConnection(connections.stringSQL) )
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("GetAllProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using(var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Quantity = reader.GetInt32(3),
                            ProductStatus = reader.GetString(4),
                            CreationDate = reader.GetDateTime(5),
                            UpdateDate = reader.GetDateTime(6)

                        });
                    }
                }
            }

            return products;
        }


        public async Task<Product> GetProductById(int id)
        {
            Product product = null;
            using (var connection = new SqlConnection(connections.stringSQL))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("GetProductById", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Quantity = reader.GetInt32(3),
                            ProductStatus = reader.GetString(4),
                            CreationDate = reader.GetDateTime(5),
                            UpdateDate = reader.GetDateTime(6)
                        };
                    }
                    else
                    {
                        throw new Exception($"No se encontró ningún producto con el ID: {id}");
                    }
                }
            }
            return product;
        }


        public async Task<Product> Create(Product product)
        {
            Product createdProduct = null;

            using (var connection = new SqlConnection(connections.stringSQL))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("AddProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@ProductStatus", product.ProductStatus);


                try
                {
                    var result = await cmd.ExecuteScalarAsync();
                    int newProductId = Convert.ToInt32(result);

                    createdProduct = new Product
                    {
                        Id = newProductId,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ProductStatus = product.ProductStatus,
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear el producto", ex);
                }
            }

            return createdProduct;
        }



        public async Task<Product> Update(Product product)
        {
            Product updatedProduct = null;

            using (var connection = new SqlConnection(connections.stringSQL))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("UpdateProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@ProductStatus", product.ProductStatus);

                try
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            updatedProduct = new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                ProductStatus = reader.GetString(reader.GetOrdinal("ProductStatus")),
                                CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                                UpdateDate = reader.GetDateTime(reader.GetOrdinal("UpdateDate")),
                            };
                        }
                    }
                }
                catch
                {
                    throw new Exception("Error al actualizar el producto");
                }
            }

            return updatedProduct;
        }


        public async Task<bool> DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(connections.stringSQL))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DeleteProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        var affectedRows = await cmd.ExecuteNonQueryAsync();
                        return affectedRows > 0;
                    }
                    catch 
                    {
                        throw new Exception("Error al eliminar el producto");
                    }
                }
            }
        }





    }
}
