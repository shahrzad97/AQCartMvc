# 🛒 AQCartMvc

## 📌 Project Overview
AQCartMvc is a simple e-commerce web application developed using **ASP.NET Core MVC**.  
The project demonstrates a complete shopping cart workflow including product listing, cart management, checkout, coupon handling, invoice request, and payment integration using **Stripe (test mode)**.

---

## 📌 Project Features

- Product catalog with availability
- Shopping cart with quantity management
- Checkout form with user information
- Mandatory privacy acceptance
- Optional invoice request (without actual invoice emission)
- Coupon code support (discount logic)
- Stripe payment integration (sandbox mode)
- Order confirmation page
- Responsive UI (desktop & mobile)
- Light/Dark mode with OS detection and manual toggle
- Session-based cart management

---

## 🛠️ Technology Choices & Motivation

### Backend Framework — ASP.NET Core MVC
Chosen for its clear separation of concerns and suitability for structured backend logic.
The application follows the **MVC (Model–View–Controller)** pattern:

- **Controllers**
  - Handle HTTP requests and business logic
  - Manage checkout flow, cart operations, and Stripe session creation

- **Models**
  - Represent database entities (Product, Order, OrderItem, Coupon, etc.)
  - Mapped using Entity Framework Core

- **ViewModels**
  - Used to transfer data between Controllers and Views
  - Prevent direct exposure of domain models in the UI layer
- Clear separation of concerns (Controllers / Models / Views)
- Strong suitability for structured backend logic
- Widespread enterprise adoption

### Language — C#
- Strongly typed
- Excellent tooling and debugging support
- Ideal for transactional systems

### Database — SQL Server
- Reliable relational DBMS
- Well-suited for order-based transactional data
- Seamless integration with .NET ecosystem
  The database contains the following main tables:

  **Products**
  **Orders**
  **OrderItems**
  **Coupons**

### Notes
- No `Cart` table is present by design:  
  The cart is managed via **session**, since it is a temporary and user-specific structure.
- Orders and OrderItems are persisted only after successful checkout.
- Coupon codes are stored in the database and validated during checkout.

### ORM — Entity Framework Core
- Simplifies data access
- Code-first migrations
- Strong consistency between domain models and database schema

### Payment — Stripe (Sandbox)
- Industry-standard payment solution
- Secure and well-documented
- Sandbox mode used (no real transactions)

### Frontend
- **Razor Views**
- **Bootstrap** for responsive layout
- No heavy frontend framework, to keep focus on backend logic

---
## 🎨 UI / UX Considerations

- Fully responsive design (desktop & mobile)
- Light/Dark theme:
- Automatic OS detection
- Manual toggle with user preference persistence
- Clear validation messages for user input
- Mobile-friendly navigation and cart layout

---

## 🔐 Privacy & Invoice Handling

- Privacy acceptance is **mandatory** before payment
- Invoice request is optional
- Invoice is **not generated**, only the request is recorded (as required by the specification)
- Invoice request status is preserved across the Stripe redirect using session

---
## ⚠️ Known Limitations & Criticalities

- Payment is performed in **sandbox mode only**
- Coupon logic is basic and intended as a proof of concept
- No authentication system (out of scope for the test)
- No real invoice PDF generation (bonus feature, not required)

These limitations were consciously accepted to stay aligned with the test scope.

---
## 🔄 Version Control (Git)

Git is used for full versioning. Feature branches created for isolated development:

- feature/coupons
- feature/invoice
- Clean commit history with meaningful messages
- Pull Requests are used to manage changes safely

---
## 🧪 How to Run the Project

### ✅ Prerequisites
- .NET SDK **7.0** or newer
- SQL Server / SQL Server Express / LocalDB
- Visual Studio 2022 (recommended)

---

### ▶️ Setup Instructions

1. **Clone the repository**
```bash
git clone https://github.com/shahrzad97/AQCartMvc.git


2. Open the solution:
AQCartMvc.sln


3. Configure the database connection

- Edit appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AQCartDb;Trusted_Connection=True;"
}


4. Apply database migrations

- From Package Manager Console:
Update-Database


5. Run the application

- Press F5 or Ctrl + F5

The application will open in your browser.

---

 **💳 Stripe Test Payment**

The application uses Stripe Checkout in test mode.

Use the following test card:

Card Number: 4242 4242 4242 4242
Expiry Date: Any future date
CVC: Any 3 digits

⚠️ No real payment is processed

---
**🎟 Coupon Testing**

A test coupon is available:

Coupon Code: SAVE10
Discount: 10%
