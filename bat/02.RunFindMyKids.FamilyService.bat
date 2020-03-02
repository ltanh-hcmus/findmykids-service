cd FindMyKids.FamilyService
docker build -t familyservice .
docker run -it --rm -p 5001:80 --name familyservicecontainer familyservice
PAUSE