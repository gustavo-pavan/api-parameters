#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Parameters.Presentation.Api/Parameters.Presentation.Api.csproj", "Parameters.Presentation.Api/"]
COPY ["Parameters.Application.Request.Command/Parameters.Application.Request.Command.csproj", "Parameters.Application.Request.Command/"]
COPY ["Parameters.Domain.Entity/Parameters.Domain.Entity.csproj", "Parameters.Domain.Entity/"]
COPY ["Parameters.IoC.DependencyInjection/Parameters.IoC.DependencyInjection.csproj", "Parameters.IoC.DependencyInjection/"]
COPY ["Parameters.Application.Request.Handler/Parameters.Application.Request.Handler.csproj", "Parameters.Application.Request.Handler/"]
COPY ["Parameters.Domain.Repository/Parameters.Domain.Repository.csproj", "Parameters.Domain.Repository/"]
COPY ["Parameters.Application.Request.Validation/Parameters.Application.Request.Validation.csproj", "Parameters.Application.Request.Validation/"]
COPY ["Parameters.Helper.Behavior/Parameters.Helper.Behavior.csproj", "Parameters.Helper.Behavior/"]
COPY ["Parameters.Helper.Extensions/Parameters.Helper.Extensions.csproj", "Parameters.Helper.Extensions/"]
COPY ["Parameters.Infra.Context/Parameters.Infra.Context.csproj", "Parameters.Infra.Context/"]
COPY ["Parameters.Infra.Repository/Parameters.Infra.Repository.csproj", "Parameters.Infra.Repository/"]
RUN dotnet restore "Parameters.Presentation.Api/Parameters.Presentation.Api.csproj"
COPY . .
WORKDIR "/src/Parameters.Presentation.Api"
RUN dotnet build "Parameters.Presentation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Parameters.Presentation.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parameters.Presentation.Api.dll"]