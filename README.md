# Final Assignment - Webservices
## Just Trade It
## Arnar Ólafsson - arnaro17@ru.is

# Dependencies
Rabbitmq  
python3  
.NET SDK 5.0

# How to run:  
I could not figure out how to use docker to run the api-gateway  
To run the webservices open three terminal tabs  
- Terminal 1:  
    - Navigate to JustTradeIt/api-gateway  
    - Issue the following commands:
        - dotnet restore  
        - dotnet build && dotnet run
- Terminal 2:  
    - Issue the following command:  
        - rabbitmq-serrver
- Terminal 3:  
    - Navigate to JustTradeIt/notification-service
    - Issue the following command:
        - pip3 install -r requirements.txt
        - python3 ./services.py

# Postman  
In Postman import Just Trade It.postman_collection.json from project root.  
Have fun sending all those requests!