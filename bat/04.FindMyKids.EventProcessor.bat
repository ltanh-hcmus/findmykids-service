cd FindMyKids.EventProcessor
docker build -t eventprocessor .
docker run --network=redis-cluster -it --rm -p 5003:82 --name eventprocessorcontainer eventprocessor
PAUSE