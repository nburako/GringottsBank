# Simple Banking API
This is a demo banking api with  features such as opening an account, withdrawing
money and so on.

## Getting Started
### Prerequisites
 - Visual Studio 2019/2022
 - Docker Desktop

### Running
Clone repository:
```
https://github.com/nburako/GringottsBank.git
```
Set **Container Orchestration** as startup project. Docker-compose.yml includes API and PostgreSQL. 
Because project is dockerized, you don't need any other setup. 
Run project and SwaggerUI will welcome you. Endpoints can be seen and triggered there.

## Design
This is a .NET 5 Web API application. It has code first approach and uses Entity Framework with PostgreSQL.
I used **Domain Driven Design** because banking and payment systems are tightly bound to business rules. 
I applied **Command-Query Separation *(CQRS)*** Pattern to be more suitable for expansion with vertical slice design. 
**Swagger** was used to further document the endpoints. **Fluent Validation** was implemented for an error-proof system. 
To keep code clean, **Mediator Pattern** is used in controllers.

### Transaction Consistency
There may be serious concurrency problems with transactions such as deposits and withdraws. 
To solve this, I avoided possible transaction conflicts by using the **Optimistic Concurrency** method in the command.
