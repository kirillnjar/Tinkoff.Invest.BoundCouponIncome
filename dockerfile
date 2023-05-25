FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src\Tinkoff.Invest.BoundCouponIncome\Tinkoff.Invest.BoundCouponIncome.csproj", "src\Tinkoff.Invest.BoundCouponIncome/"]
RUN dotnet restore "src\Tinkoff.Invest.BoundCouponIncome\Tinkoff.Invest.BoundCouponIncome.csproj"

COPY . .

WORKDIR "/src/src/Tinkoff.Invest.BoundCouponIncome.csproj"

RUN dotnet build Tinkoff.Invest.BoundCouponIncome.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tinkoff.Invest.BoundCouponIncome.csproj" -c Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh