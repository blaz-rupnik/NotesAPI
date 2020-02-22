docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD

docker build -f ./Dockerfile -t notes-api:latest ./ --no-cache

docker tag notes-api:latest $DOCKER_USERNAME/notes-api:latest

docker push $DOCKER_USERNAME/notes-api:latest