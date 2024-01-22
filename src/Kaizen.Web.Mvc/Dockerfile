FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/Kaizen.Web.Mvc/Kaizen.Web.Mvc.csproj", "src/Kaizen.Web.Mvc/"]
COPY ["src/Kaizen.Web.Core/Kaizen.Web.Core.csproj", "src/Kaizen.Web.Core/"]
COPY ["src/Kaizen.Application/Kaizen.Application.csproj", "src/Kaizen.Application/"]
COPY ["src/Kaizen.Core/Kaizen.Core.csproj", "src/Kaizen.Core/"]
COPY ["src/Kaizen.EntityFrameworkCore/Kaizen.EntityFrameworkCore.csproj", "src/Kaizen.EntityFrameworkCore/"]
WORKDIR "/src/src/Kaizen.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Kaizen.Web.Mvc", "src/Kaizen.Web.Mvc"]
COPY ["src/Kaizen.Web.Core", "src/Kaizen.Web.Core"]
COPY ["src/Kaizen.Application", "src/Kaizen.Application"]
COPY ["src/Kaizen.Core", "src/Kaizen.Core"]
COPY ["src/Kaizen.EntityFrameworkCore", "src/Kaizen.EntityFrameworkCore"]
WORKDIR "/src/src/Kaizen.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Kaizen.Web.Mvc.dll"]
