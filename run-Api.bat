cd Client
start dotnet run

cd ..\Api
start func start

cd ..
start swa start http://localhost:5000 --api-location http://localhost:7071

timeout /t 20 /nobreak
start http://localhost:4280

cls