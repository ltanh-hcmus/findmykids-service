# 18HCB - Thesis
### Xây dựng hệ thống đảm bảo an toàn cho trẻ em.

# Author
### Lê Tuấn Anh - 18424003
### Huỳnh Văn Hậu - 18424024

# Find my kids - Service
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
### Development
In folder of project
```sh
$ dotnet run --server.urls=http://0.0.0.0:5009
```
### Todos

 - CQRS and event sourcing basic follow
 - Display postion real time

License
----
##### Lê Tuấn Anh - Huỳnh Văn Hậu
