FROM mcr.microsoft.com/mssql/server:2017-latest

ENV CHECK_POLICY=OFF
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=@dmin1@dmin1
ENV MSSQL_PID=Developer

COPY ./script.sql /setup_scripts/script.sql

RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P ${SA_PASSWORD} -i /setup_scripts/script.sql \
    && pkill sqlservr 

CMD ["/opt/mssql/bin/sqlservr"]