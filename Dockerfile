FROM mcr.microsoft.com/dotnet/core/sdk:latest AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:5000

FROM base AS builder
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY ./FarmerzonAddress/*.csproj FarmerzonAddress/
COPY ./FarmerzonAddressDataAccess/*.csproj FarmerzonAddressDataAccess/
COPY ./FarmerzonAddressDataAccessModel/*.csproj FarmerzonAddressDataAccessModel/
COPY ./FarmerzonAddressDataTransferModel/*.csproj FarmerzonAddressDataTransferModel/
COPY ./FarmerzonAddressErrorHandling/*.csproj  FarmerzonAddressErrorHandling/
COPY ./FarmerzonAddressManager/*.csproj FarmerzonAddressManager/
RUN dotnet restore --verbosity detailed
COPY . .
WORKDIR /src/FarmerzonAddress
RUN dotnet build -c $Configuration -o /app

FROM builder AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FarmerzonAddress.dll"]