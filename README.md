# Home Project

## Task Description:
Implement a JSON API with two endpoints:
* /random - pulls a random image from a statically configured subreddit.
* /history - returns a history of all '/random' requests. 

All '/history' requests must be preserved accross application runs. No authorization needed. Must run on Ubuntu 21.10.

## Prerequisites:
* Docker
* [optional] Postman

## Installation:
1. Download & extract the .zip.
2. `cd` to the directory you extracted the .zip to.
3. Run `docker-compose build` and wait for it to finish building the image.
4. Run `docker-compose up` and wait for the message informing you that the SQL Server is up and the .NET Core logger.
5. You can now send GET requests on port 8000. E.g. `http://localhost:8000/random`, `http://localhost:8000/history`.

## Configuration:
* To change the subreddit that the images get pulled from, go to `appsettings.json` and change the 'Subreddit' value after the colon, e.g. `dankmemes` or `halo`. You will need to run `docker-compose build` and `docker-compose up` again (sorry).


