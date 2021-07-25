# DzTechly
An e-commerce web application for a fictionnal "DZTechly" commerce that sells Tech products.

# Technologies & Dependecies
* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet) Core (.NET 5.0), used for the Server
* [Angular](https://angular.io/) version 11 used for the client
* [Stripe](https://stripe.com/) used for billing & payments
* [Sendinblue](https://www.sendinblue.com/) used for transaction mails (Email address confirmation, Reset password...)
* [Argon Design System](https://demos.creative-tim.com/argon-design-system-angular/#/home) for the UI.
* [Redis](https://redis.io/) for carts.
* [Swagger](https://swagger.io/) for a simple API documentation.

# Features
* Login & Register 
  * Reactive forms
  * Email verification and Password change with mails using Sendinblue

* Shop 
  * Most common filters (Price, name, type...) and sorting
  * Paging
  * Product details & technical sheets
  * Basic reviews
  * Waiting list with mailing in case of a refill
  
* Shopping Cart 
  * With order totals
  * Shipping prices

* Orders
  * Ordering & payment using Stripe
  * Orders history
  
* User Profile
  * Update login and password
  * Update personal informations & address
 
* Administration _(Some bugs here and there)_
  * Remove products
  * Add & Update products _**(Most bugged part)**_
  * A full orders history of the shop
  * Change other users roles or delete their accounts
  * Remove a review
  
# What is missing

* A proper upload of images, currently, only the images existing in the server can be used. This was a requirement of the project at first.
* Bug fixes for Add & Update products, specifically the technical sheets.

# How to run it
I will do my best to describe the steps to run it locally, however it is quite complicated since a lot of third parties were used.

* Create a stripe and a sendinblue account
* Install the Stripe CLI and a redis server on your local machine
* Clone this repository
* Setup the API AppSettings.json
  * Create a mailing configuration, for that you must create an Account on Sendinblue, then add this to your appsettings
    ```
        "EmailConfiguration": {
        "ApiKey": "The api key of your account",
        "FromEmail": "the email you wish to send emails from",
        "FromName": "the name of the sender, DZTechly !",
        "ConfirmEmailTemplateId": 1 this is the sendinblue templateId of the confirmation email,
        "PasswordChangeTemplateId": 2 this is the sendinblue templateId of the password change email,
        "ProductQuantityTemplateId": 5 this is the sendinblue templateId of the product refill email
        }
      
      You have to setup those by your self.
    ```
  * Create a stripe configuration, for that you will need a stripe account & a project, you will also need the Stripe CLI
    ```
      "StripeSettings": {
        "PublishibleKey": "the publishible key of your stripe project",
        "SecretKey": "the secret key of your stripe project",
        "LocalWHKey": "the localhost CLI stripe key "
      }
    ```
  * Additional settings
    * Tokens setuup
    ```
    "Token": {
        "Key": "a string key that is used to create tokens, it should be BIG for security but it can be whatever you want!",
        "Issuer": "https://localhost:5001 or the address at which you serve the API",
        "ExpiryInMinutes": 150 Minutes before expiry of the token
      }
    ```
    * Connection strings, by default the API uses an SQLite database
    ```
        "ConnectionStrings": {
        "DefaultConnection": "Data source=dztechly.db",
        "Redis": "localhost"
        }
    ```
    * Cart life span
    ```
        "CartLifeSpanInMinutes": 420,
    ```
* Run the API, you should be able to use the API.bat batch file, you can however run it with the .NET CLI if you wish, this will create an SQL database and fill it with seed data
* Go the client folder and run `npm install`, then you should be able to run the client using either client.bat or the angular CLI
* Run stripe, you can use either stripe.bat or run it with the CLI
* You will also need to setup SSL certificats, as it runs on HTTPS, this should be easy.
* Try it out, you can find all seeded accounts in the Infrastructure project under users.json, all passwords are passw0rd (With a 0).
# Screenshots
