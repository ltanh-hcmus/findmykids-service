# 18HCB - Thesis
### Xây dựng hệ thống đảm bảo an toàn cho trẻ em.

# Author
### Lê Tuấn Anh - 18424003
### Huỳnh Văn Hậu - 18424024

# Service
![2020-02-22_22-35-39](https://user-images.githubusercontent.com/61270653/75095430-3092c780-55c7-11ea-85a5-0c0f4238e1c0.png)

### Tech
* .Net core microservice
* RabbitMq
* Redis
* docker

### Installation
Create docker network
```sh
$ docker network create rabbitmq-cluster
$ docker network create redis-cluster
```
In docker-compose folder
```sh
$ docker-compose up -d
```
### Start service
Start Family Service
```sh
$ dotnet run --server.urls=http://localhost:5001
```
Start LocationReporter Service
```sh
$ dotnet run --server.urls=http://localhost:5002
```
Start EventProcessor Service
```sh
dotnet run --server.urls=http://localhost:5003
```
Start RealityService
```sh
dotnet run --server.urls=http://localhost:5004
```
### Todos

 - CQRS and event sourcing basic follow
 - Display postion real time

License
----
##### Lê Tuấn Anh - Huỳnh Văn Hậu
