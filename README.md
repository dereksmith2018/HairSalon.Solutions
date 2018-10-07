# Hair Salon

##### C# exercise By Derek Smith., Epicodus - September, 2018

## *Description*
_Hair Salon_ is a web app The user will be able to add a stylist, specialist and a client to the database. You will be able to assign a client to a employee.



## *Specifications*
* As a salon stylist, I need to be able to see a list of all our stylists.
* As an stylist, I need to add new stylists to our system when they are hired.
* As an stylist, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.
* And here are the user stories that the salon owner would like you to add:
* As an stylist, I need to be able to delete stylists (all and single).
* As an stylist, I need to be able to delete clients (all and single).
* As an stylist, I need to be able to view clients (all and single).
* As an stylist, I need to be able to edit JUST the name of a stylist. (You can choose to allow employees to edit additional properties but it is not required.)
* As an stylist, I need to be able to edit ALL of the information for a client.
* As an stylist, I need to be able to add a specialty and view all specialties that have been added.
* As an stylist, I need to be able to add a specialty to a stylist.
* As an stylist, I need to be able to click on a specialty and see all of the stylists that have that specialty.
* As an stylist, I need to see the stylist's specialties on the stylist's details page.
* As an stylist, I need to be able to add a stylist to a specialty.

## *Known Bugs & Issues*
User interface is not stylized as much as it could be.

## *Setup/Installation Requirements*

1. Clone this repository by using Terminal command:
```
    $ git clone https://github.com/dereksmith2018/HairSalon.Solutions
```
```
    via the terminal, enter the program (HairSalon) directory and _dotnet restore_ & _dotnet build_
```
```
    Lastly, run _dotnet run_ and terminal should provide the folloing url: http://localhost:5000. Open this in a browser.
```
2. Create the database using MySQL and follow these SQL commands:
```
1. > CREATE DATABASE derek_smith;
```
```
2. > USE derek_smith
```
```
$ CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
```
```
$ CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255));
```
```
$ CREATE TABLE specialists (id serial PRIMARY KEY, name VARCHAR(255));
```
```
$ CREATE TABLE stlists_clients (id serial PRIMARY KEY, stylist_id INT, client_id INT);
```
```
$ CREATE TABLE stylists_specialists (id serial PRIMARY KEY, stylist_id INT, specialty_id INT);
```

```
3. Build a test database - we'll do this in phpMyAdmin:
```
### Specs: Hair Salon
| Spec | Input | Output |
| :-------------     | :------------- | :------------- |
| **The program will allow the user to add a stylist** | Input: "Derek" | Output: "Derek" |
| **The program will allow the user to add clients** | Input: "Kate" | Output: "Kate"|
| **The program will allow the user to view the clients that a stylist has** | Input: "Stylist: Derek, Client: Kate" | Output: "Susan" |

## *Support and contact details*
Contact: Derek Smith @dereksmith2018 github

## *Technologies Used*
* C#, ASP.NET Core 1.1
* phpMyAdmin
* MSTest
* MySql
* HTML

#### *Copyright* (c) 2018 Derek Smith, Epicodus
