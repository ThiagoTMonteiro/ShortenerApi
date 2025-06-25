# ShortenerApi

Uma API RESTful para encurtamento de URLs desenvolvida com ASP.NET Core. A qual retorna uma URL encurtada e redireciona para o link original.

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core 9.0  
- Entity Framework Core  
- Docker  
- PostgreSQL  
- Traefik (como proxy reverso)  

## 🚀 Como Executar

1. Clone este repositório:

   ```bash
   git clone https://github.com/ThiagoTMonteiro/ShortenerApi.git
   cd ShortenerApi

2. Construa e inicie os containers (incluindo Traefik):

   ```bash
   docker-compose up --build

3. Acessar a API via Traefik no endereço http://short.local.
   
   > **Nota:** Certifique-se de que o domínio `short.local` esteja configurado no seu arquivo `hosts` apontando para `127.0.0.1` ou que seu ambiente resolva esse domínio corretamente.


## Como funciona?

Ao executar este programa, o usuário deve primeiro cadastrar uma Url depois de obter essa url encurtada poderar fazer um acesso direto usando a mesma.

## 📚 EndPoints

A API oferece os seguintes endpoints:

- **POST** `/api/Links/shorten`: Cria um link encurtado a partir de uma URL fornecida.

    Request:

    ```json
    {
      "url": "https://www.linkedin.com/in/"
    }
    ```

    Response:

    ```json
    {
      "shortUrl": "http://short.local/0382b5",
      "originalUrl": "https://www.linkedin.com/in/"
    }
    ```

- **GET** `/api/Links`: Obter todos os links
   
   Response:

   ```json
    [
     {
       "shortUrl": "http://short.local/0382b5",
       "originalUrl": "https://www.linkedin.com/in/,
       "clicks": 1
     }
   ]
   ```

- **DELETE** `/api/Links/{shortCode}`: Excluir um link pelo seu código curto

  Request:

   `/api/Links/{shortCode}`


  Response:

  ```json
    {
      "message": "Link deleted successfully"
    }
  ```

## 📖 Documentação OpenAPI (Scalar)

A API gera automaticamente sua documentação OpenAPI utilizando o Scalar.

> No projeto, o método `MapScalarApiReference()` é utilizado para configurar a documentação, garantindo que os tipos escalares personalizados sejam devidamente mapeados no schema OpenAPI.

### Como acessar

- [http://short.local/scalar/v1](http://short.local/scalar/v1)

Essa interface permite visualizar todos os endpoints, modelos de dados, parâmetros e realizar testes interativos diretamente no navegador.

> **Nota:** O método `MapScalarApiReference()` é uma extensão personalizada utilizada na configuração do Scalar para melhorar a geração dos schemas da API.

## ⚙️ Configurações

As configurações de aplicação estão localizadas em `appsettings.json` e `appsettings.Development.json`.



