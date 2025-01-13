using HirePlatform.Contracts.User;
using Mapster;
using Shopping.Contracts.Cart;
using Shopping.Contracts.CartItem;
using Shopping.Contracts.Category;
using Shopping.Contracts.Order;
using Shopping.Contracts.Products;
using Shopping.Models;

namespace Shopping.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Add configuration that's global to app
            TypeAdapterConfig<User, UserResponse>.NewConfig()
              .Map(dest => dest.Id, src => src.Id)
              .Map(dest => dest.FirstName, src => src.FirstName)
              .Map(dest => dest.LastName, src => src.LastName)
              .Map(dest => dest.NationalID, src => src.NationalID)
              .Map(dest => dest.Email, src => src.Email)
              .Map(dest => dest.Password, src => src.PasswordHash)
              .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
              .Map(dest => dest.CreatedDate, src => src.CreatedDate);

            TypeAdapterConfig<ProductRequest, Product>.NewConfig()
            .Map(dest => dest.ImageUrl, src => src.ImageUrl);

            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
          .Map(dest => dest.Name, src => src.Name)
          .Map(dest => dest.Description, src => src.Description)
          .Map(dest => dest.Price, src => src.Price)
          .Map(dest => dest.Stock, src => src.Stock)
          .Map(dest => dest.ImageUrl, src => src.ImageUrl)
          .Map(dest => dest.CategoryId, src => src.CategoryId)
          .Map(dest => dest.CategoryName, src => src.Category.Name);

            // Mapping for CategoryRequest to Category (when creating or updating category)
            TypeAdapterConfig<CategoryRequest, Category>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description); // If needed

            // Mapping for Category to CategoryResponse (for returning the category)
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description);

             // Mapping for CartRequest to Cart
            TypeAdapterConfig<CartRequest, Cart>.NewConfig()
                .Map(dest => dest.UserId, src => src.UserId);

            // Mapping for CartItemRequest to CartItem
            TypeAdapterConfig<CartItemRequest, CartItem>.NewConfig()
                .Map(dest => dest.ProductId, src => src.ProductId)
                .Map(dest => dest.Quantity, src => src.Quantity);

            // Mapping for Cart to CartResponse
            TypeAdapterConfig<Cart, CartResponse>.NewConfig()
                 .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.UserId, src => src.UserId.ToString())
                .Map(dest => dest.CartItems, src => src.Items.Adapt<List<CartItemResponse>>());

            // Mapping for CartItem to CartItemResponse
            TypeAdapterConfig<CartItem, CartItemResponse>.NewConfig()
                .Map(dest => dest.ProductName, src => src.Product.Name)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.Quantity, src => src.Quantity)
                .Map(dest => dest.TotalPrice, src => src.Price * src.Quantity);

            // Mapping from OrderRequest to Order entity
            TypeAdapterConfig<OrderRequest, Order>.NewConfig()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.Status, src => "Pending");

            // Mapping from OrderItemRequest to OrderItem entity
            TypeAdapterConfig<OrderItemRequest, OrderItem>.NewConfig()
                .Map(dest => dest.ProductId, src => src.ProductId)
                .Map(dest => dest.Quantity, src => src.Quantity);

            // Mapping from Order entity to OrderResponse
            TypeAdapterConfig<Order, OrderResponse>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.TotalAmount, src => src.Items.Sum(item => item.Price * item.Quantity)) // Calculating total amount
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.Items, src => src.Items.Adapt<List<OrderItemResponse>>());  // Mapping OrderItems to OrderItemResponse

            // Mapping from OrderItem entity to OrderItemResponse
            TypeAdapterConfig<OrderItem, OrderItemResponse>.NewConfig()
                .Map(dest => dest.ProductName, src => src.Product.Name)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.Quantity, src => src.Quantity)
                .Map(dest => dest.TotalPrice, src => src.Price * src.Quantity);
        }
    }
}
