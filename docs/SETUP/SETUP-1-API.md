# Setup

## Instalar o .Net SDK

```plain
https://dotnet.microsoft.com/pt-br/download
```

## Verificar versão instalada

```shell
dotnet --version
```

## Criar a Solução

```
dotnet new sln -n dotnet-api-efcore
```

(O arquivo dotnet-api-efcore.sln será criado)

# Criar um Projeto Web Api

- API (Application Programming Interface);
- Web API será acessada via protocolo HTTP/HTTPS, assim aplicações cliente (browsers, apps mobile, etc.) consumam serviços de um servidor.

```
dotnet new webapi --use-controllers -o api
dotnet new webapi --framework net9.0 --use-controllers -o api
```

## Adicionar o projeto api à solução

```
dotnet sln add api
```

## Compilar toda a Solução

```
dotnet build
```

## Agora vamos executar o Projeto API com uma destas formas:

Na pasta do projeto api:

```
dotnet run
```

Sem entrar na pasta:

```
dotnet run --project api
```

(Em seguida, acesse: http://localhost:5065/WeatherForecast)

Executar com o watch (recarrega automaticamente ao salvar alterações):

```
dotnet watch run
```

```
dotnet watch run --project api
```

## Habilitar Certificado de Desenvolvimento HTTPS

Para certificado de desenvolvimento HTTPS:

```
dotnet dev-certs https --trust
```

(Uma mensagem aparecerá: "Successfully trusted the existing HTTPS certificate.")

Execute o comando a seguir para iniciar o aplicativo no perfil https:

```shell
dotnet watch run --launch-profile https
```

(Agora acesse https://localhost:7181/WeatherForecast)
