FROM postgres:10.3
ENV DB_PASSWORD=${DB_PASSWORD}
ENV POSTGRES_USER=postgres
ENV POSTGRES_DB=postgres
COPY ./create.user.database.sh /docker-entrypoint-initdb.d/create.user.database.sh