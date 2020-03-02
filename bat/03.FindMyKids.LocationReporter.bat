:: %ipaddress%: set ipaddress=192.168.1.109
cd FindMyKids.LocationReporter
docker build -t locationreporter .
docker run -it --rm -p 5002:81 --name locationreportercontainer locationreporter
PAUSE