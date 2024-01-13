-It was built in Net 7 so Visual Studio 2022 is needed.

-Download and install PostgreSQL with the default settings, selecting port 5432 and "pass" for the database superuser password.

-Run GeolocationApp project, it will create the database and tables, then a Swagger UI shows the next APIs:

![image](https://github.com/braiansanchez/Geolocation-API/assets/75460076/55e80eb3-2318-495a-a4d2-3b015a7af5f5)

LOCATIONS

GET: Get a list of locations 

POST: Add a new location

PUT: Update the data from the location by ID

DELETE: Delete a location by ID

----
USER

GET: Get a list of user registered

PUT: Login user

POST: Register user

----
All USER API can be used without authentication.

If you call USER GET method, it will add a new default user if the database is empty.

To use LOCATIONS API you must register an user (USER POST) and login with that user (USER PUT), the token its shown in the Response body.

Put this token in Swagger UI, in the button Authorize, example "Bearer 123asd123exampletoken".

![image](https://github.com/braiansanchez/Geolocation-API/assets/75460076/6c06c03b-6429-4bc0-9834-db20614eaa16)

If you call LOCATION GET method, it will populate the database with some locations if the database is empty.
