echo OFF
setlocal

set dacpac_path=..\bizca.database\bin\Release\bizca.database.dacpac
set dacpac_project=..\bizca.database\bizca.database.sqlproj
set container_name=sql-server-db-fts
set image_name=sql-server-db-fts
set database_name=bizca
set password=Password_0
set port=1459

set container_id=
for /f %%i in ('docker ps -q -f name^=%container_name%') do set container_id=%%i

if "%container_id%"=="" (
    echo "container does not exist"
)else (
	echo "container %container_id% found"
	docker rm --force %container_name%
)

docker build -t %image_name% .
docker run --rm --name %container_name% -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=%password%" -e "MSSQL_PID=Express" -p %port%:1433 -d %image_name%

timeout 15

msbuild  %dacpac_project% /P:Configuration=Release

sqlpackage.exe /a:publish /tcs:"Server=localhost,%port%;Database=%database_name%;User=sa;Password=%password%" /sf:%dacpac_path% /Variables:Environnement=Dev 
pause


