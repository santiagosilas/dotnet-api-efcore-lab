# Comandos Úteis

Uso dos comandos da ferramenta .NET CLI (Command Line Interface)

## Executar a Api

dotnet watch run --launch-profile https

## Acesso ao Swagger

https://localhost:7181/swagger/index.html

## Criar o Script de Migração

dotnet ef migrations add NomeDaMigracao

## Aplicar Migração (para gerar o banco e as tabelas)

(Gera-se o BD e tabelas com base no script de migração)

dotnet ef database update

## Remove o script de migração criado

dotnet ef migrations remove NomeDaMigracao
