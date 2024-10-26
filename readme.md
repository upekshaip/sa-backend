## API Documentation

### Overview

This API provides endpoints for managing auctions, bids, payments, users, notifications, and dashboard data. The API follows the OpenAPI 3.0.1 specification.

### Menu

- [Overview](#overview)
- [Endpoints](#endpoints)
  - [Auctions](#auctions)
    - [GET /api/auctions](#get-api-auctions)
    - [POST /api/auctions/my](#post-api-auctionsmy)
    - [POST /api/auctions/mybids](#post-api-auctionsmybids)
    - [POST /api/auctions/statusUpdate](#post-api-auctionsstatusupdate)
    - [GET /api/auctions/{id}](#get-api-auctionsid)
    - [POST /api/auctions/create](#post-api-auctionscreate)
    - [POST /api/auctions/additem](#post-api-auctionsadditem)
    - [POST /api/auctions/upload-image](#post-api-auctionsupload-image)
  - [Bids](#bids)
    - [GET /api/bids](#get-api-bids)
    - [GET /api/bids/{id}](#get-api-bidsid)
    - [POST /api/bids/create](#post-api-bidscreate)
  - [Dashboard](#dashboard)
    - [POST /api/dashboard/all](#post-api-dashboardall)
  - [Notifications](#notifications)
    - [GET /api/notifications/{id}](#get-api-notificationsid)
    - [POST /api/notifications/check](#post-api-notificationscheck)
    - [POST /api/notifications/read](#post-api-notificationsread)
  - [Payments](#payments)
    - [GET /api/payments/{id}](#get-api-paymentsid)
    - [POST /api/payments/create](#post-api-paymentscreate)
    - [POST /api/payments/info](#post-api-paymentsinfo)
    - [POST /api/payments/check](#post-api-paymentscheck)
    - [POST /api/payments/my](#post-api-paymentsmy)
  - [Users](#users)
    - [GET /api/users](#get-api-users)
    - [GET /api/users/{id}](#get-api-usersid)
    - [POST /api/users/signup](#post-api-userssignup)
    - [PUT /api/users/edit](#put-api-usersedit)
    - [POST /api/users/resetPassword](#post-api-usersresetpassword)
    - [POST /api/users/login](#post-api-userslogin)
- [Schemas](#schemas)
  - [CheckNotificationsDto](#checknotificationsdto)
  - [CreateAuctionDto](#createauctiondto)
  - [CreateAuctionItemDto](#createauctionitemdto)
  - [CreateBidDto](#createbiddto)
  - [CreatePaymentDto](#createpaymentdto)
  - [CreateUserDto](#createuserdto)
  - [GetDashboardDto](#getdashboarddto)
  - [GetInfoDto](#getinfodto)
  - [GetMyAuctionsDto](#getmyauctionsdto)
  - [GetMyPaymentsDto](#getmypaymentsdto)
  - [ReadNotifications](#readnotifications)
  - [ResetUserPasswordDto](#resetuserpassworddto)
  - [StatusUpdateDto](#statusupdatedto)
  - [UpdateUserDto](#updateuserdto)
  - [UserLoginDto](#userlogindto)

### Endpoints

#### Auctions

- **GET /api/auctions**

  - **Tags**: Auctions
  - **Responses**:
    - `200`: Success

- **POST /api/auctions/my**

  - **Tags**: Auctions
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `GetMyAuctionsDto`
    - **Example**:
      ```json
      {
        "id": 1
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/auctions/mybids**

  - **Tags**: Auctions
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `GetMyAuctionsDto`
    - **Example**:
      ```json
      {
        "id": 1
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/auctions/statusUpdate**

  - **Tags**: Auctions
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `StatusUpdateDto`
    - **Example**:
      ```json
      {
        "id": 1,
        "auctionId": 1,
        "isLive": "true"
      }
      ```
  - **Responses**:
    - `200`: Success

- **GET /api/auctions/{id}**

  - **Tags**: Auctions
  - **Parameters**:
    - `id` (path, required, integer, int32)
  - **Responses**:
    - `200`: Success

- **POST /api/auctions/create**

  - **Tags**: Auctions
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `CreateAuctionDto`
    - **Example**:
      ```json
      {
        "title": "Auction Title",
        "description": "Auction Description",
        "auctionImage": "image_url",
        "auctionCategory": "Category",
        "sellerId": 1,
        "startTime": "2023-10-01T00:00:00Z",
        "endTime": "2023-10-10T00:00:00Z",
        "startingBid": 100.0
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/auctions/additem**

  - **Tags**: Auctions
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `CreateAuctionItemDto`
    - **Example**:
      ```json
      {
        "auctionId": 1,
        "itemName": "Item Name",
        "itemDescription": "Item Description",
        "itemImage": "image_url",
        "itemCategory": "Category"
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/auctions/upload-image**
  - **Tags**: Auctions
  - **Request Body**:
    - **Content Types**: `multipart/form-data`
    - **Schema**: Object with property `file` (string, binary)
    - **Example**:
      ```json
      {
        "file": "binary_data"
      }
      ```
  - **Responses**:
    - `200`: Success

#### Bids

- **GET /api/bids**

  - **Tags**: Bids
  - **Responses**:
    - `200`: Success

- **GET /api/bids/{id}**

  - **Tags**: Bids
  - **Parameters**:
    - `id` (path, required, integer, int32)
  - **Responses**:
    - `200`: Success

- **POST /api/bids/create**
  - **Tags**: Bids
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `CreateBidDto`
    - **Example**:
      ```json
      {
        "auctionId": 1,
        "bidderId": 1,
        "status": "active",
        "bidAmount": 150.0
      }
      ```
  - **Responses**:
    - `200`: Success

#### Dashboard

- **POST /api/dashboard/all**
  - **Tags**: Dashboard
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `GetDashboardDto`
    - **Example**:
      ```json
      {
        "userId": 1
      }
      ```
  - **Responses**:
    - `200`: Success

#### Notifications

- **GET /api/notifications/{id}**

  - **Tags**: Notifications
  - **Parameters**:
    - `id` (path, required, integer, int32)
  - **Responses**:
    - `200`: Success

- **POST /api/notifications/check**

  - **Tags**: Notifications
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `CheckNotificationsDto`
    - **Example**:
      ```json
      {
        "userId": 1
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/notifications/read**
  - **Tags**: Notifications
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `ReadNotifications`
    - **Example**:
      ```json
      {
        "id": 1,
        "userId": 1
      }
      ```
  - **Responses**:
    - `200`: Success

#### Payments

- **GET /api/payments/{id}**

  - **Tags**: Payment
  - **Parameters**:
    - `id` (path, required, integer, int32)
  - **Responses**:
    - `200`: Success

- **POST /api/payments/create**

  - **Tags**: Payment
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `CreatePaymentDto`
    - **Example**:
      ```json
      {
        "userId": 1,
        "auctionId": 1,
        "amount": 200.0,
        "type": "credit"
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/payments/info**

  - **Tags**: Payment
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `GetInfoDto`
    - **Example**:
      ```json
      {
        "userId": 1,
        "auctionId": 1,
        "type": "credit"
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/payments/check**

  - **Tags**: Payment
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `CreatePaymentDto`
    - **Example**:
      ```json
      {
        "userId": 1,
        "auctionId": 1,
        "amount": 200.0,
        "type": "credit"
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/payments/my**
  - **Tags**: Payment
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `GetMyPaymentsDto`
    - **Example**:
      ```json
      {
        "userId": 1
      }
      ```
  - **Responses**:
    - `200`: Success

#### Users

- **GET /api/users**

  - **Tags**: Users
  - **Responses**:
    - `200`: Success

- **GET /api/users/{id}**

  - **Tags**: Users
  - **Parameters**:
    - `id` (path, required, integer, int32)
  - **Responses**:
    - `200`: Success

- **POST /api/users/signup**

  - **Tags**: Users
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `CreateUserDto`
    - **Example**:
      ```json
      {
        "firstName": "John",
        "lastName": "Doe",
        "email": "john.doe@example.com",
        "username": "johndoe",
        "gender": "male",
        "mobile": 1234567890,
        "password": "password123",
        "address": "123 Main St"
      }
      ```
  - **Responses**:
    - `200`: Success

- **PUT /api/users/edit**

  - **Tags**: Users
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `UpdateUserDto`
    - **Example**:
      ```json
      {
        "id": 1,
        "firstName": "John",
        "lastName": "Doe",
        "email": "john.doe@example.com",
        "username": "johndoe",
        "gender": "male",
        "mobile": 1234567890,
        "address": "123 Main St"
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/users/resetPassword**

  - **Tags**: Users
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `ResetUserPasswordDto`
    - **Example**:
      ```json
      {
        "id": 1,
        "newPassword": "newpassword123",
        "oldPassword": "oldpassword123"
      }
      ```
  - **Responses**:
    - `200`: Success

- **POST /api/users/login**
  - **Tags**: Users
  - **Request Body**:
    - **Content Types**: `application/json`, `text/json`, `application/*+json`
    - **Schema**: `UserLoginDto`
    - **Example**:
      ```json
      {
        "username": "johndoe",
        "password": "password123"
      }
      ```
  - **Responses**:
    - `200`: Success

### Schemas

#### CheckNotificationsDto

- **Type**: object
- **Properties**:
  - `userId` (integer, int32)
- **Additional Properties**: false

#### CreateAuctionDto

- **Type**: object
- **Properties**:
  - `title` (string, nullable)
  - `description` (string, nullable)
  - `auctionImage` (string, nullable)
  - `auctionCategory` (string, nullable)
  - `sellerId` (integer, int32)
  - `startTime` (string, date-time)
  - `endTime` (string, date-time)
  - `startingBid` (number, double)
- **Additional Properties**: false

#### CreateAuctionItemDto

- **Type**: object
- **Properties**:
  - `auctionId` (integer, int32)
  - `itemName` (string, nullable)
  - `itemDescription` (string, nullable)
  - `itemImage` (string, nullable)
  - `itemCategory` (string, nullable)
- **Additional Properties**: false

#### CreateBidDto

- **Type**: object
- **Properties**:
  - `auctionId` (integer, int32)
  - `bidderId` (integer, int32)
  - `status` (string, nullable)
  - `bidAmount` (number, double)
- **Additional Properties**: false

#### CreatePaymentDto

- **Type**: object
- **Properties**:
  - `userId` (integer, int32)
  - `auctionId` (integer, int32)
  - `amount` (number, double)
  - `type` (string, nullable)
- **Additional Properties**: false

#### CreateUserDto

- **Type**: object
- **Properties**:
  - `firstName` (string, nullable)
  - `lastName` (string, nullable)
  - `email` (string, nullable)
  - `username` (string, nullable)
  - `gender` (string, nullable)
  - `mobile` (integer, int32)
  - `password` (string, nullable)
  - `address` (string, nullable)
- **Additional Properties**: false

#### GetDashboardDto

- **Type**: object
- **Properties**:
  - `userId` (integer, int32)
- **Additional Properties**: false

#### GetInfoDto

- **Type**: object
- **Properties**:
  - `userId` (integer, int32)
  - `auctionId` (integer, int32)
  - `type` (string, nullable)
- **Additional Properties**: false

#### GetMyAuctionsDto

- **Type**: object
- **Properties**:
  - `id` (integer, int32)
- **Additional Properties**: false

#### GetMyPaymentsDto

- **Type**: object
- **Properties**:
  - `userId` (integer, int32)
- **Additional Properties**: false

#### ReadNotifications

- **Type**: object
- **Properties**:
  - `id` (integer, int32)
  - `userId` (integer, int32)
- **Additional Properties**: false

#### ResetUserPasswordDto

- **Type**: object
- **Properties**:
  - `id` (integer, int32)
  - `newPassword` (string, nullable)
  - `oldPassword` (string, nullable)
- **Additional Properties**: false

#### StatusUpdateDto

- **Type**: object
- **Properties**:
  - `id` (integer, int32)
  - `auctionId` (integer, int32)
  - `isLive` (string, nullable)
- **Additional Properties**: false

#### UpdateUserDto

- **Type**: object
- **Properties**:
  - `id` (integer, int32)
  - `firstName` (string, nullable)
  - `lastName` (string, nullable)
  - `email` (string, nullable)
  - `username` (string, nullable)
  - `gender` (string, nullable)
  - `mobile` (integer, int32)
  - `address` (string, nullable)
- **Additional Properties**: false

#### UserLoginDto

- **Type**: object
- **Properties**:
  - `username` (string, nullable)
  - `password` (string, nullable)
- **Additional Properties**: false
