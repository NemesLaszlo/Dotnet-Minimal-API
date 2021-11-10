# Dotnet-Minimal-API

This repository contains a Minimal API (CRUD) with .NET 6 as a practise project.

### Tech Stack - Backend

- .NET 6
- Swagger
- FluentAssertions
- NSubstitute
- XUnit

### Endpoints of the Backend

| Entity   | Type   | URL             | Description             | Success     | Authorize |
| -------- | ------ | --------------- | ----------------------- | ----------- | --------- |
| Customer | GET    | /customers      | Get all customers.      | 200 OK      | No        |
|          | GET    | /customers/{id} | Get customer by Id.     | 200 OK      | No        |
|          | POST   | /customers      | Create a customer.      | 201 CREATED | No        |
|          | PUT    | /customers/{id} | Update a customer by Id | 200 OK      | Yes       |
|          | DELETE | /customers/{id} | Delete a customer by Id | 200 OK      | Yes       |
