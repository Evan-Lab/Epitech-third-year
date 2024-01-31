sudo -u root systemctl restart docker
docker build . -t whanos-jenkins
docker run -it -p 8080:8080 -p 50000:50000 --restart=on-failure -v /var/run/docker.sock:/var/run/docker.sock --name whanos-jenkins whanos-jenkins