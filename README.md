# WBAPI-Reservation

Comandos para ejecutar la migración

dotnet ef migrations add addInitialDB --project Infrastructure --startup-project WBAPI-Reservation
dotnet ef database update --project Infrastructure --startup-project WBAPI-Reservation

dotnet ef migrations add addAuthSchema --project Infrastructure --startup-project AuthApi
dotnet ef database update --project Infrastructure --startup-project AuthApi

Usuarios:
pedroperez: Administrador
grabrielgomez: Asesor Medellin
rodolforuiz: Asesor Bogotá

Contraseña para todos los usuarios:
Prueba123*

Comandos para la ejecución de pruebas
Back:
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
