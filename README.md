# RestaurantManager

## ER-Diagram

![image](https://github.com/user-attachments/assets/cf97868f-a305-4268-95f5-3e5aa0eb2900)

# Rest-API requests

## Booking

### POST - /Booking/create

Creates a new booking.
```
{
  "restaurantId": 0,                             Add Id of restaurant you want to book
  "requestedTime": "2024-09-04T21:39:36.743Z",   Date and time when the booking should start (bookings last 2h)
  "userId": 0,                                   Add Id of user that's making the booking
  "nrOfPeople": 0,                               Declare how many people the booking is intended for (not currently taken into account)
  "requests": "string"                           Specify any dietary requests or other.
}
```
### GET - /Booking/all_bookings

Lists all bookings in system

### GET - /Booking/{restaurantId}/all_bookings

Lists all bookings at certain restaurant.
(not currently in order of time)

### GET - /Booking/{bookingId}

shows booking using bookingId.

### PUT - /Booking/{bookingId}

Updates booking info using bookings Id.

### DELETE - /Booking/{bookingId}

Deletes booking using bookings id



## Menu

### POST - /restaurant:{restaurantId}/Menu/create

Creates a new Menu using restaurant Id.
```
{
  "name": "string",       <- Name of menu
  "restaurantId": 0       <- Id of restaurant where menu is served
}
```
### GET  - /restaurant:{restaurantId}/Menu/all_menus

Lists all Menus at restaurant

### GET - /restaurant:{restaurantId}/Menu/{menuId}

Lists all menuitems of a menu at a restaurant using restaurants id and menu id.

### PUT - /restaurant:{restaurantId}/Menu/{menuId}

Updates a menu at a restaurant using restaurantId and menuId.
```
{
  "id": 0,           <- Id of menu
  "name": "string"   <- New name of menu
}
```
### DELETE - /restaurant:{restaurantId}/Menu/{menuId}

Deletes menu at restaurant using restaurant id and menu id.

### GET - /restaurant:{restaurantId}/Menu/{menuId}/{menuItemId}

Prints menuitem using restaurant id, menu id and menuitem id.

### POST - /restaurant:{restaurantId}/Menu/{menuId}/create

Creates a new menuitem using restaurants id and menu id.
```
{
  "name": "string",           <- Mame of dish/drink/item on menu
  "category": "string",       <- Subcategory if any (for example soup, drink..)
  "description": "string",    <- Description of content
  "amountAvaliable": 0        <- Amount of dishes made (not currently in use)
}
```
## Restaurant

### POST - /Restaurant/create

Creates new restaurant
```
{
  "name": "string",           <- Name of restaurant
  "phoneNumber": "string",    <- Phonenumber
  "email": "string",          <- Email
  "address": "string",        <- Full Address
  "description": "string"     <- Description of restaurant
}
```
### GET - /Restaurant/view/all_restaurants

Lists all avaliable restaurants info

### GET - /Restaurant/view/{restaurantId}

Prints info of restaurant using restaurant id.

### PUT - /Restaurant/update/{restaurantId}

Updates restaurantinfo using restaurants id. All fields will be replaced.
```
{
  "id": 0,                     <- Id of restaurant to update
  "name": "string",            <- Name of restaurant
  "phoneNumber": "string",     <- Phonenumber 
  "email": "string",           <- Email 
  "address": "string",         <- Full Address
  "description": "string"      <- Description of restaurant
}
```
### DELETE - /Restaurant/delete/{restaurantId}

Deletes restaurant using restaurant id.


## Table

### POST - /restaurant/{restaurantId}/Table/create

Creates new table at restaurant using restaurant id.
```
{
  "restaurantId": 0,      <- Enter id of restaurant
  "nrOfSeats": 0          <- Max amount of seats at table (Not currently in use)
}
```
### GET - /restaurant/{restaurantId}/Table/all_tables

List all tables at restaurant using restaurant id.

### GET - /restaurant/{restaurantId}/Table/{tableId}

Print info about specific table at restaurant using restaurant id and table id.

### PUT - /restaurant/{restaurantId}/Table/{tableId}

Update info about table at restaurant using restaurant id and table id
```
{
  "tableId": 0,          <- Id of table to update
  "nrOfSeats": 0         <- Max amount of number of seats at table (Not currently in use)
}
```
### DELETE - /restaurant/{restaurantId}/Table/{tableId}

Delete table at restaurant using restaurant id and table id.


## User

### POST - /User/create

Creates a new user 
```
{
  "name": "string",         <- Name of user
  "email": "string",        <- Email of user
  "phoneNumber": "string"   <- Phonenumber of user
}
```
### GET - /User/all_users

Lists all users in system

### GET - /User/{userId}

Prints info about user using user id.

### PUT - /User/{id}

Update userinfo using user id. 
```
{
  "id": 0,                  <- Id of user
  "name": "string",         <- Name of user
  "email": "string",        <- Email of user
  "phoneNumber": "string"   <- Phonenumber of user
}
```
### DELETE - /User/{id}

Deletes user using user id.
