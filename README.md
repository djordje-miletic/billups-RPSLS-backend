# billups-RPSLS-backend

Application name: Rock Paper Scissors Lizard Spock game
Application type: Web API
Application tech stack: .NET
Application requirements: .NET 8 SDK https://dotnet.microsoft.com/en-us/download/dotnet/8.0
Application descritpion: This is .NET 8 Web API project. This application porpose is to provide backend part for simulation of game from TV show Big bang theory.
App uses in memory database so it can store games and determine who is winner. App consists from web api which provides data to frontend part and play endpoint simulates game agains computer.
Second part of application is gamehub that enables playes to play one against another. 

How to run it:

If you're using visual studio just load up project and run it. Pay attention to port number because you maybe need to change it on front end part of application.

Docker run:

In root folder of application there is docker file provided. To run in you need docker installed on you machine.
To start app execute following commands:

docker build -t billups.rpsls.api .

docker run -p 8080:8080 -p 8081:8081 billups.rpsls.api
