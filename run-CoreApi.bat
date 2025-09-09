cd Client
start dotnet run

cd ..\CoreApi
start dotnet run

cd ..
start swa start http://localhost:5000 --api-location http://localhost:7071

timeout /t 20 /nobreak
start http://localhost:4280

cls