# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
# WORKDIR /app

# COPY *.csroj ./
# RUN dotnet restore

# COPY . ./
# RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/sdk:6.0
# WORKDIR /app
# EXPOSE 80
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "LeanCode-HomeProject.dll"]




# FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base
# WORKDIR /app
# EXPOSE 80

# # ENV ASPNETCORE_URLS=http://+:5000

# FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build
# WORKDIR /src
# COPY ["LeanCode-HomeProject.csproj", "./"]
# RUN dotnet restore "LeanCode-HomeProject.csproj"
# COPY . .
# WORKDIR "/src/."
# RUN dotnet build "LeanCode-HomeProject.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 80/tcp
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh