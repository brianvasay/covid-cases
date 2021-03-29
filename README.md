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