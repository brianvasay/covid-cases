**Running the Application**
----
* **Install the latest version of the .NET Core SDK found in this link:** 
  * https://dotnet.microsoft.com/download/dotnet/3.1

* **Install a code editor. You may use Visual Studio Code:** 
  * https://code.visualstudio.com/download

* **If you decided to install Visual Studio Code, I recommend the following extensions:** 
  * C#
  * C# Extensions

* **Once you have pulled the latest copy, please remove the `mock.` prefix from the following files:** 
  * mock.appsettings.json
  * mock.appsettings.Development.json
  
* **Ensure that the above files have values in the following section:** 
  * `"Server=; Port=; Username=; Password=; Database=; "`

* **Once done with the previous steps, pull the latest copy of the repository and open a terminal to type the following commands:** 
  * This will trust the tls certificate in your local machine 
  `dotnet dev-certs https -t`

  * Navigate to the repository 
  `cd <directory>\CovidCases\Application`

  * Run the application 
  `dotnet watch run`

* **Call the API endpoints in port 5001** 
  * https://localhost:5001/api/cases
  * https://localhost:5001/api/cases/top/confirmed?observationDate=2020-04-14&maxResults=2

**List All Cases**
----
  This will return the cases of all countries ordered by observation date, in descending order.

* **URL**

  `/api/cases`

* **Method:**

  `GET`
  
*  **URL Params**

   None

*  **Required:**
 
   None

* **Data Params**

  None

* **Success Response:**

  * **Code:** 200
    **Content:** 
    `[
        {  "observation_date": "2020-04-14",
        "country": "Haiti",
        "confirmed": 40,
        "deaths": 0,
        "recovered": 0 }
     ]`
 
* **Error Response:**

  * **Code:** 400 BAD REQUEST
    **Content:** `{
    "errors": [
        "<error message>"
    ],
    "statusCode": 404,
    "message": "A bad request has been made."
}`

  OR

  * **Code:** 404 NOT FOUND
    **Content:** `{
    "errors": [
        "<error message>"
    ],
    "statusCode": 404,
    "message": ""A requested resource cannot be found."
}`
----

**List All Cases by Observation Date**
----
  This will return the cases of all countries filtered by observation date.

* **URL**

  `/api/cases/top/confirmed`

* **Method:**

  `GET`
  
* **URL Params**

  `observationDate=[yyyy-MM-dd]`
  `maxResults=[number]`

* **Required:**
 
  `observationDate=[yyyy-MM-dd]`

* **Data Params**

  None

* **Success Response:**

  * **Code:** 200
    **Content:** 
    `[
        {  "observation_date": "2020-04-14",
        "country": "Haiti",
        "confirmed": 40,
        "deaths": 0,
        "recovered": 0 }
     ]`
 
* **Error Response:**

  * **Code:** 400 BAD REQUEST
    **Content:** `{
    "errors": [
        "<error message>"
    ],
    "statusCode": 404,
    "message": "A bad request has been made."
}`

  OR

  * **Code:** 404 NOT FOUND
    **Content:** `{
    "errors": [
        "<error message>"
    ],
    "statusCode": 404,
    "message": ""A requested resource cannot be found."
}`
----