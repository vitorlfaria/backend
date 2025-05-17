FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

#Copiando os arquivos para dentro do container
COPY . .

ARG BUILD_CONFIGURATION=Release
RUN dotnet restore "./src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"
RUN dotnet publish "./src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

#Copiando os arquivos publicados da etapa de construção
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080/

#Expondo a porta do aplicativo
EXPOSE 8080
ENTRYPOINT ["dotnet", "Ambev.DeveloperEvaluation.WebApi.dll"]