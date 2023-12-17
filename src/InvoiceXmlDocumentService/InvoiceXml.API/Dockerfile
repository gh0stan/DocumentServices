#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InvoiceXmlDocumentService/InvoiceXml.API/InvoiceXml.API.csproj", "InvoiceXmlDocumentService/InvoiceXml.API/"]
COPY ["InvoiceXmlDocumentService/InvoiceXml.Application/InvoiceXml.Application.csproj", "InvoiceXmlDocumentService/InvoiceXml.Application/"]
COPY ["InvoiceXmlDocumentService/InvoiceXml.Domain/InvoiceXml.Domain.csproj", "InvoiceXmlDocumentService/InvoiceXml.Domain/"]
COPY ["InvoiceXmlDocumentService/InvoiceXml.Infrastructure/InvoiceXml.Infrastructure.csproj", "InvoiceXmlDocumentService/InvoiceXml.Infrastructure/"]
RUN dotnet restore "InvoiceXmlDocumentService/InvoiceXml.API/InvoiceXml.API.csproj"
COPY . .
WORKDIR "/src/InvoiceXmlDocumentService/InvoiceXml.API"
RUN dotnet build "InvoiceXml.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvoiceXml.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvoiceXml.API.dll"]