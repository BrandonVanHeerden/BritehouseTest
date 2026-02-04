# BritehouseTest
Corporate News System Test For Britehouse

*******************Dot Net Core Web Api************************

Before using the Api, it is important to note the following:
Before Running the api, right click the solution inside Visual Studio, and click "Restore NuGet Pakages" to ensure all NuGet Packages are installed.

The Api follows Clean Code Architecture
The Api uses CQRS pattern to separate read and write logic
In conjunction with CQRS, the api uses MediatR package, which is a nuget package that uses the Mediator design pattern, to decouple the controllers from the application and business logic. MediatR sends the request to the correct handler.
The Api uses Repository pattern to decouple and seperate the database logic from business logic
The Api uses Entity Framework Core to scaffold the database.
All the sensitive connection strings and keys are stored in the Api UserSecrets. A copy of these connection strings and keys will be provided by the project administrator. 
The Api uses reflection for mapping repository abstraction (in Domain) with the concrete implementation (in Infrastructure). The naming of the abstraction and the implementation should be the same, with the exception that the abstraction should start with the "I" perfix, and both should end with the 'Repository' suffix. For Example: Abstraction --> IMyTestRepository ; Concrete Implementation --> MyTestRepository
The Api also uses reflection for mapping service (in Domain) with the concrete implementation (in Application). The naming of the abstraction and the implementation should be the same, with the exception that the abstraction should start with the "I" perfix, and both should end with the 'Service' suffix. For Example: Abstraction --> IMyTestService ; Concrete Implementation --> MyTestService

The Api uses Result Pattern to ensure all responses are uniformed and follow a specific structure.

The Api has an exception middleware for catching and logging any unpredictable system wide errors. 

The Api uses Code-First Migrations with Enitiy Framework Core. 

To Scaffold and create a new database, in Visual Studio, Open the Package Manager Console. In the "Default Project" dropdown, ensure that you select the "Infrastructure" assembly / project, ensure you have your database connection string in the api AppSettings.Json or UserSecrets File, and then run the below command in the console:
Update-Database -Context AppDBContext

To Add a new migration in Visual Studio, Open the Package Manager Console. In the "Default Project" dropdown, ensure that you select the "Infrastructure" assembly / project. Then in the console, type the below command:
add-migration YourMigrationName

To update the databse after the migration, in Visual Studio, Open the Package Manager Console. In the "Default Project" dropdown, ensure that you select the "Infrastructure" assembly / project, and then run the below command in the console:
dotnet ef database update



************************FRONT END**************************
React FrontEnd.

A React-based frontend application integrated with a .NET Core 8 Web API using JWT authentication.

Please ensure the api is running before using the Front End
## Features

- **JWT Authentication**: Secure authentication with JWT tokens
- **Protected Routes**: Routes that require authentication
- **Context API**: State management for authentication
- **Axios Interceptors**: Automatic token injection and error handling
- **Responsive Design**: Mobile-friendly UI
- **Error Handling**: Comprehensive error handling and user feedback

## Project Structure

```
src/
├── components/          # React components
│   └── ProtectedRoute.js
├── context/             # React Context providers
│   └── AuthContext.js
├── hooks/               # Custom React hooks
│   └── useAuth.js
├── pages/               # Page components
│   ├── Login.js
│   ├── Register.js
│   └── Dashboard.js
├── services/            # API services
│   ├── authService.js
│   └── axiosInstance.js
├── App.js               # Main App component
├── App.css              # App styles
├── index.js             # Entry point
└── config.js            # Configuration
```

## Installation

Delete the node_modules folder. The run the below scripts to add dependencies and run the application
```bash
# Install dependencies
npm install

# Start development server
npm start

# Build for production
npm build
```

