# C# Project Backend - API

## Installation

- Will be added later

## API Endpoints

- Base URL: `http://localhost:<PORT>/`
- All requests needs to be submitted in JSON format
  - Headers: `Content-Type: application/json`

### Login

Logs in a user with the provided credentials.

- **HTTP Request**

  - Method: `POST`
  - URL: `/api/login`

- **Request Body: JSON**

  ```json
  {
    "username": "newuser123",
    "password": "securepassword"
  }
  ```

  - `username` (string) - The username of the user.
  - `password` (string) - The password of the user.

- **Response: JSON**

  - 200 OK: The request was successful, and the user is authenticated.

    - success (boolean) - Indicates if the login was successful.
    - message (string) - A message indicating the result of the login.
    - data (object) - Contains user information and authentication token.
    - token (string) - The authentication token for the user.
    - user (object) - User data including ID, username, etc.

  - 400 Bad Request: Authentication failed due to invalid credentials.

    - success (boolean) - false
    - message (string) - "Invalid credentials"

### Sign Up

Registers a new user with the provided details.

- **HTTP Request**

  - Method: POST
  - URL: /api/signup

- **Request Body: JSON**

  ```json
  {
    "username": "newuser123",
    "password": "securepassword",
    "email": "newuser@example.com",
    "firstName": "John",
    "lastName": "Doe"
  }
  ```

  - `username` (string, required) - The desired username for the new user.
  - `password` (string, required) - The password for the new user.
  - `email` (string, required) - The email address of the new user.
  - `firstName` (string, optional) - The first name of the user.
  - `lastName` (string, optional) - The last name of the user.

- **Response: JSON**

  - 200 OK: The request was successful, and the user has been registered.

    - success (boolean) - Indicates if the register was successful.
    - message (string) - A message indicating the result of the registration.
    - data (object) - Contains user information and authentication token.
    - token (string) - The authentication token for the user.
    - user (object) - User data including ID, username, etc.

  - 400 Bad Request: The request was invalid.

    - success (boolean) - false
    - message (string) - "Invalid input data"
