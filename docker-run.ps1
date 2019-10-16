Set-Location $PSScriptRoot

$env:DOCKER_HOST = "tcp://10.0.1.202:2375"

docker-compose -f docker-compose.yml up -d --build
