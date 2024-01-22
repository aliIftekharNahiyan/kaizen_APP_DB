FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Kaizen.Web.Host/Kaizen.Web.Host.csproj", "src/Kaizen.Web.Host/"]
COPY ["src/Kaizen.Web.Core/Kaizen.Web.Core.csproj", "src/Kaizen.Web.Core/"]
COPY ["src/Kaizen.Application/Kaizen.Application.csproj", "src/Kaizen.Application/"]
COPY ["src/Kaizen.Core/Kaizen.Core.csproj", "src/Kaizen.Core/"]
COPY ["src/Kaizen.EntityFrameworkCore/Kaizen.EntityFrameworkCore.csproj", "src/Kaizen.EntityFrameworkCore/"]
WORKDIR "/src/src/Kaizen.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Kaizen.Web.Host", "src/Kaizen.Web.Host"]
COPY ["src/Kaizen.Web.Core", "src/Kaizen.Web.Core"]
COPY ["src/Kaizen.Application", "src/Kaizen.Application"]
COPY ["src/Kaizen.Core", "src/Kaizen.Core"]
COPY ["src/Kaizen.EntityFrameworkCore", "src/Kaizen.EntityFrameworkCore"]
WORKDIR "/src/src/Kaizen.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Kaizen.Web.Host.dll"]
