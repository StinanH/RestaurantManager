# RestaurantManager

## ER-Diagram

![image](https://github.com/user-attachments/assets/cf97868f-a305-4268-95f5-3e5aa0eb2900)

# Rest-API requests

##Booking

###POST - /Booking/create

###GET - /Booking/view/all_bookings

###GET - /Booking/view/bookingAvaliability

###GET - /Booking/view/restaurant_bookings/{restaurantId}

###GET - /Booking/view/{userId}

###PUT - /Booking/update/{id}

###DELETE - /Booking/delete/{id}


##Menu

###POST - /restaurant:{restaurantId}/Menu/create - To create new menu

###GET  - /restaurant:{restaurantId}/Menu/all_menus

###GET - /restaurant:{restaurantId}/Menu/{menuId}

###PUT - /restaurant:{restaurantId}/Menu/update/{menuId}

###DELETE - /restaurant:{restaurantId}/Menu/delete/{menuId}

###GET - /restaurant:{restaurantId}/Menu/:{menuId}/menuitem:{menuItemId}

###POST - /restaurant:{restaurantId}/Menu/:{menuId}/create


##Restaurant

###POST - /Restaurant/create

###GET - /Restaurant/view/all_restaurants

###GET - /Restaurant/view/{restaurantId}

###PUT - /Restaurant/update/{restaurantId}

###DELETE - /Restaurant/delete/{restaurantId}


##Table

###POST - /restaurant:{restaurantId}/Table/create

###GET - /restaurant:{restaurantId}/Table/view/all_tables

###GET - /restaurant:{restaurantId}/Table/view/{tableId}

###PUT - /restaurant:{restaurantId}/Table/update/{tableId}

###DELETE - /restaurant:{restaurantId}/Table/delete/{tableId}


##User

###POST - /User/create

###GET - /User/view/all_users

###GET - /User/view/{userId}

###PUT - /User/update/{id}

###DELETE - /User/delete/{id}

