:: Chạy lệnh kích hoạt card mạng 
cd docker-compose
docker network create rabbitmq-cluster
docker network create redis-cluster
docker network create elastic

:: Xóa images cũ
docker container rm docker-compose_rabbit1_1 docker-compose_redis_1 docker-compose_PostgreSQL_1 pgadmin_container -f
docker image rm docker.elastic.co/kibana/kibana docker.elastic.co/elasticsearch/elasticsearch rabbitmq redis dpage/pgadmin4 sameersbn/postgresql -f

:: Chạy lệnh cấu hình
docker-compose up -d

PAUSE