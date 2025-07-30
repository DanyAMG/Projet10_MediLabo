Projet10_MediLabo - Medical Management Application

This project is the 10th project of the OpenClassroom formation "Développeur d'application Backend"
It's a web application destinated to medical infrastructures to manage the patients and help to
evaluate diabetes risk.
The application allows authenticated users to :
- View and Manage patients (add, edit, delete).
- View and manage medical history of the patients.
- Help evaluate diabetes risk based on an internal analysis.

This application is built around a microservices architecture. With multiple multiservices
communicating through a gateway.

The microservices are the following :
- Back_Patient - manage patient data (SQL Server)
- Back_Notes - manage notes (MongoDB)
- Back_RapportRisque - evaluate diabete risk
- Gateway_Ocelot - API Gateway
- Auth_Service - Authentication and user management (SQL Server)
- Frontend - Front-end service in Blazor WebAssembly

The different technologies used :
- .NET 9 (and .NET 7 for the Frontend)
- Entity Framework Core
- Identity Framework
- MongoDB.Driver
- Blazor WebAssembly
- Ocelot(API Gateway)
- Docker

Prerequisites
-.NET 7 SDK & .NET 9 SDK
- Docker

To run the project : 
Clone the repository, then run the containers :

'''bash
docker-compose up --build

This will launch  :
- Back_Patient
- Back_Notes
- Back_RapportRisque
- Gateway_Ocelot
and the containers for the MongoDB and SQL database.

Then, once the services are up you need to run the frontend service that is note dockerised.
You need to launch it  manually from the Visual Studio or the .NET CLI.
With Visual Studio :
	1.Open the Front_User project.
	2. Set it as the startup project.
	3. Run with F5 or Ctrl+F5

With .NET CLI :
'''bash
cd Front_User
dotnet run

The frontend will be acessible then at : http://localhost:5189

Make sure the backend services are running in docker before launching the frontend,
otherwise API calls will fail.

AUTHENTICATION
'''md
- Users must be logged in with a username and a password.
- A JWT Token is stored in localStorage and added to all authorized HTTP requests
using a HttpMessageHandler.
- Access to protected resources is restricted with authentication.


NOTES
- Patient and Users data are stored in SQL Server (Dockerized).
- Medical notes are strored in MongoDB server (Dockerized).
- Services communicate via the Ocelot Gateway.
-ALl microservices, execpt the frontend, are containerized and orchestrated using Docker Compose.

GREENCODE SUGGESTION
- Optimize database queries : In this project it is not a problem for now because we don't have much data in the database.
But in the future we should ensure to apply filtering at the database instead of fetching all datas.
This will reduces memory usage and network loads.
- Stop unusued Docker containers : To minimize the background resource consumption, make sure to stop the Docker containers
when they are not actively used during development. Running containers consume CPU and memory.

Auteurs
Dany MOTA as part of a personal learning project.

License
This work is licensed under the Creative Commons Attribution-NonCommercial 4.0 International License.  
You may use and adapt this project for non-commercial purposes with proper attribution.

To view a copy of this license, visit [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).