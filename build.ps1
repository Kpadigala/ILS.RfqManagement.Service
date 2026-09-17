if (-not (Test-Path .\ConnectionStrings))
{
	copy-item c:\DevOps\ConnectionStrings . -recurse
}
if (-not (Test-Path .\BuildAndDeployment))
{
	copy-item c:\DevOps\BuildAndDeployment . -recurse
}

docker build --rm --progress=plain --build-arg SONARQUBE_PROJECT_KEY=ILS_test.sonarqube.ilsmart.service --build-arg SONARQUBE_TOKEN=3cbcd6dd874743932fb43f8e5af605b0be8cc0bc -t int-dtr.ilsmart.com/ils/test.sonarqube.ilsmart.com .

