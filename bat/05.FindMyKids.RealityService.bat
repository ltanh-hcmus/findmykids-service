set ipaddress=192.168.1.109

cd FindMyKids.RealityService
docker build -t realityservice .
docker run -it --rm -p %ipaddress%:5002:83 --name realityservicecontainer realityservice
PAUSE