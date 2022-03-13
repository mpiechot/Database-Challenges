cat pagila-master/pagila-schema.sql | docker exec -i postgres psql -U gast -d pagila
cat pagila-master/pagila-data.sql | docker exec -i postgres psql -U gast -d pagila