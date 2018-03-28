FROM microsoft/dotnet

RUN mkdir /app
WORKDIR /app
ADD ./site/work /app  
