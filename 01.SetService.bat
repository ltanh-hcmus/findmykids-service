:: Xóa images cũ
docker container rm familyservicecontainer locationreportercontainer eventprocessorcontainer realityservicecontainer -f
docker image rm familyservice locationreporter eventprocessor realityservice -f