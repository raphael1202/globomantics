# Globomantics

Globomantics is a web application designed to facilitate the management and bidding of houses. Users can easily add, edit, delete houses, and place bids, making it a robust platform for real estate transactions.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [API Endpoints](#api-endpoints)

## Installation

### API

To get started with the API:

1. Clone the repository:

    ```bash
    git clone https://github.com/raphael1202/globomantics.git
    cd globomantics/Api
    ```

2. Install the necessary dependencies:

    ```bash
    dotnet restore
    ```

3. Set up the database:

    ```bash
    dotnet ef database update
    ```

4. Run the API:

    ```bash
    dotnet run
    ```

### React Web

To get started with the React web application:

1. Navigate to the ReactWeb directory:

    ```bash
    cd ../ReactWeb
    ```

2. Install the necessary dependencies:

    ```bash
    npm install
    ```

3. Run the application:

    ```bash
    npm run dev
    ```

## Usage

After completing the installation steps, you can access the React web application locally and interact with the API for managing and bidding on houses.

## Features

- Add new houses to the listing
- Edit details of existing houses
- Delete houses from the listing
- Place bids on houses

## Technologies Used

- **Frontend:**
  - React
  - HTML
  - CSS
  - Bootstrap
  - TypeScript
  - Axios
  - React Query
  - React Router
  - Form Validation

- **Backend:**
  - C#
  - .NET Core
  - Entity Framework Core
  - SQLite

- **Authentication:**
  - User authentication and authorization

## API Endpoints

### House Endpoints

- **Get all houses:**
  - `GET /houses`
  - Returns a list of all houses.
  
- **Get a house by ID:**
  - `GET /house/{houseId:int}`
  - Returns details of a house by ID. Returns 404 if not found.
  
- **Add a new house:**
  - `POST /houses`
  - Adds a new house. Validates input. Returns 201 on success.
  
- **Update a house:**
  - `PUT /houses`
  - Updates an existing house. Validates input. Returns 404 if not found.
  
- **Delete a house:**
  - `DELETE /houses/{houseId:int}`
  - Deletes a house by ID. Returns 404 if not found.

### Bid Endpoints

- **Get all bids for a house:**
  - `GET /house/{houseId:int}/bids`
  - Returns a list of bids for a house. Returns 404 if house not found.
  
- **Add a bid for a house:**
  - `POST /house/{houseId:int}/bids`
  - Adds a new bid for a house. Validates input. Returns 404 if house not found, 400 if HouseId mismatch.

