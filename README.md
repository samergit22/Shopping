ASP.NET Core Shopping API

Overview
This project is a robust and scalable Shopping API built using ASP.NET Core. It serves as the backend for an e-commerce platform, handling key functionalities like product management, user authentication, order processing, and payment integration.

Features
Product Management: Create, read, update, and delete (CRUD) operations for products.
Category Management: Organize products into categories for easy navigation.
User Authentication: Secure login and registration with JWT-based authentication.
Order Management: Endpoints to manage shopping carts, orders, and order history.
Search and Filtering: Advanced search functionality with filtering by category, price, and keywords.
Payment Integration: Simulated payment processing for completing orders.
Pagination and Sorting: Efficient data retrieval with pagination and sorting support.


Technologies Used
Framework: ASP.NET Core
Database: Microsoft SQL Server
Authentication: JSON Web Tokens (JWT)
ORM: Entity Framework Core
API Documentation: Swagger/OpenAPI
Version Control: Git


Getting Started
Prerequisites
.NET 6 SDK or later
Microsoft SQL Server
A code editor (e.g., Visual Studio or Visual Studio Code)


API Endpoints
Authentication
POST /api/auth/register: Register a new user.
POST /api/auth/login: Authenticate and get a JWT token.
Products
GET /api/products: Get all products with pagination and filtering.
POST /api/products: Add a new product (Admin only).
PUT /api/products/{id}: Update product details (Admin only).
DELETE /api/products/{id}: Delete a product (Admin only).
Orders
POST /api/orders: Place a new order.
GET /api/orders: View order history (Authenticated users).
