# WebStore

Сreating an online store for registered, unregistered users and admin.
The project is written using asp.net core mvs

Registered Users:
1. User authorization.
2. User roles: administrator and regular user.
User rights:
  	- see all comrades by category,
- buy goods
- watch the basket
- remove unnecessary goods from the cart,
- make a purchase
 - мiew detailed information of your basket
- view detailed product information

Admin rights:
 - create a category
 - create a product
 - delete product and category
 - view all categories and products
 - role management


Start the project by changing the connection string in appsettings.json
And in the header of the project profile click on "Users" -> click "Add role" and add the role "Admin".

2. Click "Register" and register under email - admin@gmail.com and password Q1234_qaz
3. Then click "Cabinet" then click "Add category" and add a category.
4. Next, go to the category you added and add the product,
   and through the "house" in the profile header you can make purchases, and through the cabinet under the role of "Admin" you can add roles
  (you can add the role "User" and register under another email and get a registered user), product categories and product, delete product, edit
