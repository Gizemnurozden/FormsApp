namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository()
        {
            _categories.Add(new Category {CategoryId=1,Name="Telefon"});
            _categories.Add(new Category {CategoryId=2,Name="Bilgisayar"});

            _products.Add(new Product{ProductId=1,Name="İphone13",Price = 35000,IsActive=true,Image="1.jpg",CategoryId=1});
            _products.Add(new Product{ProductId=2,Name="İphone14",Price = 45000,IsActive=true,Image="2.jpg",CategoryId=1});
            _products.Add(new Product{ProductId=3,Name="İphone15",Price = 60000,IsActive=true,Image="3.jpg",CategoryId=1});
            _products.Add(new Product{ProductId=4,Name="İphone16",Price = 70000,IsActive=true,Image="4.jpg",CategoryId=1});

            _products.Add(new Product{ProductId=5,Name="Macbook Air",Price = 80000,IsActive=true,Image="5.jpg",CategoryId=2});
            _products.Add(new Product{ProductId=6,Name="Macbook Pro",Price = 90000,IsActive=true,Image="6.jpg",CategoryId=2});
        }

        public static List<Product> Product
        {
            get{
                return _products;
            }

        }

         public static List<Category> Categories 
        {
            get{
                return _categories;
            }

        }
        public static void CreateProduct(Product entity) //yeni ürün ekletirim.
        {
            _products.Add(entity);
        }

        public static void EditProduct(Product updateProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
            if (entity != null)
            {
                entity.Name = updateProduct.Name;
                entity.Price = updateProduct.Price;
                entity.Image = updateProduct.Image;
                entity.CategoryId = updateProduct.CategoryId;
                entity.IsActive= updateProduct.IsActive;

            }
        }

        public static void DeleteProduct(Product deletedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == deletedProduct.ProductId);
            if (entity != null)
            {
                _products.Remove(entity);
            }
        }

    }
}