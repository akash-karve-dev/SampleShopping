echo "START SERVER MANUALLY"
/opt/mssql/bin/sqlservr &
echo "SLEEP FOR 30 SECONDS"
sleep 30
echo "DATABASE INITIALIZATION SCRIPT STARTED"
for item in /scripts/*.sql
do
echo "Executing file" $item
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password1234! -d master -i $item
done
#/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password1234! -d master -i /scripts/user-db-init.sql
echo "DATABASE INITIALIZATION SCRIPT COMPLETED"
sleep infinity