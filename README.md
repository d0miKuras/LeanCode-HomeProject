# Home Project

## Task Description:
Implement a JSON API with two endpoints:
* /random - pulls a random image from a statically configured subreddit.
* /history - returns a history of all '/random' requests. 

All '/history' requests must be preserved accross application runs. No authorization needed.

## Prerequisites:
* Docker (`apt-get install docker`)
* Docker-compose (`apt-get install docker-compose`)
* [optional] Postman

## Installation:
1. Download & extract the .zip.
2. `cd` to the directory you extracted the .zip to.
3. Run `docker-compose build`.
4. Run `docker-compose up` and wait for the message informing you that the SQL Server is up and the .NET Core logger messages. This will download dotnet sdk and mssql docker images and spin them in a single container.
5. You can now send GET requests on port 8000. E.g. `http://localhost:8000/random`, `http://localhost:8000/history`.

## Configuration:
* To change the subreddit that the images get pulled from, go to `appsettings.json` and change the 'Subreddit' value after the colon, e.g. `dankmemes` or `halo`. You will need to run `docker-compose build` and `docker-compose up` again (sorry) and delete the old image.


## Explanation:
I was not sure which databases are available natively to Ubuntu (of course, I could look it up, or just save everything in a local .json or something, but I decided to learn something new for myself), so I decided to run everything in a composed docker container. The container is composed of two images: leancode-homeproject_web (which has mcr.microsoft.com/dotnet/sdk:3.1-focal, hopefully runs on 21.10) and leancode-homeproject_db (which is a mcr.microsoft.com/mssql/server).

You have to run `docker-compose build` and `docker-compose up` after changing the `appsettings.json` because for some reason I cannot install dotnet-ef from Dockerfile, so I have to do it through entrypoint.sh and when you run it for the second time, it throws an error saying that dotnet-ef is already installed.
