# ShortenerApi

Uma API RESTful para encurtamento de URLs desenvolvida com ASP.NET Core. A qual retorna uma URL encurtada e redireciona para o link original.

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core 9.0  
- Entity Framework Core  
- Docker  
- SQL Server  
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

## 📚 EndPoints

A API oferece os seguintes endpoints:

- **POST** `/api/Links/shorten`: Cria um link encurtado a partir de uma URL fornecida.

  Request:
  
  ```json
    {
      "url": "https://www.linkedin.com/in/"
    }

- **GET** `/api/Links`: Obter todos os links
- **DELETE** `/api/Links/{shortCode}

