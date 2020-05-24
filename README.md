# powerplant-coding-challenge Submission

## Source

<https://github.com/gem-spaas/powerplant-coding-challenge>

## Getting started

### Visual studio

Requirements:

- Visual studio 2019 v16.4+

Open **powerplant-coding-challenge.sln** and run debugger.

### Docker

Requirements:

- Docker desktop

Navigate to source directory (source dir includes .slm, Dockerfile, ...)

Build image

```cmd
docker build -t application-api .
```

Run container

```cmd
docker run -p 8888:80 -d --name production-plan-api application-api
```

Navigate to <http://localhost:8888>

CleanUp

Stop **production-plan-api** container

```cmd
docker container stop production-plan-api
```

Remove **production-plan-api** container

```cmd
docker container rm production-plan-api
```

Remove **application-api** image

```cmd
docker image rm application-api
```

Check image list

```cmd
docker image ls
```

Remove other recently added images during build

```cmd
docker image rm <image to be removed>
```
