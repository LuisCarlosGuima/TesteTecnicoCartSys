## 🚀 Tecnologias Utilizadas

* **[.NET 8](https://dotnet.microsoft.com/)**: Plataforma principal de desenvolvimento.
* **[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)**: ORM para acesso e manipulação do banco de dados (Code-First).
* **[SQL Server](https://www.microsoft.com/sql-server)**: Banco de dados relacional rodando via Docker.
* **[xUnit, Moq & FluentAssertions](https://xunit.net/)**: Stack completa para testes unitários isolados.
* **[JWT (JSON Web Tokens)](https://jwt.io/)**: Autenticação e autorização seguras, incluindo sistema de Refresh Token.
* **[Docker](https://www.docker.com/)**: Containerização do ambiente de banco de dados.

## ⚠️ Pré-requisitos Importantes

* **Visual Studio 2026 (Obrigatório):** Este projeto utiliza o novo padrão de arquivo de solução **`.slnx`**. Para abrir, compilar e executar a solução corretamente, é necessário utilizar o Visual Studio 2026. Versões anteriores com o antigo formato `.sln` não são suportadas.
* **Docker Desktop:** Para rodar o banco de dados localmente sem necessidade de instalações complexas.
* **.NET 8 SDK:** Caso prefira compilar ou rodar os testes via CLI.

## 🛠️ Como Executar o Projeto

Siga o passo a passo abaixo para configurar o ambiente de desenvolvimento:

### 1. Clonar o Repositório

```bash
git clone [URL_DO_SEU_REPOSITORIO]
cd cartsys-backend

```

### 2. Subir o Banco de Dados (Docker)

Para evitar que o repositório ficasse excessivamente pesado, **não foi incluído um arquivo de backup do banco de dados (.tar) ou o volume do container exportado**. O banco deve ser recriado localmente através do Docker e populado pelas Migrations.

A imagem utilizada no desenvolvimento é a `mcr.microsoft.com/mssql/server:2022-latest`.

**Passo A: Baixar a imagem do SQL Server**

```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest

```

**Passo B: Criar e rodar o container**

Execute o comando abaixo para instanciar o banco de dados com os parâmetros de infraestrutura e credenciais idênticos aos configurados no projeto:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Admin@#123" -p 1433:1433 --name teste-tecnico --hostname teste-tecnico --memory="6g" --cpus="4" -v sql_data:/var/opt/mssql --restart unless-stopped -d mcr.microsoft.com/mssql/server:2022-latest

```

*(Nota: O parâmetro `SA_PASSWORD` está definido como `Admin@#123` para manter a compatibilidade exata com a string de conexão configurada na aplicação).*

### 3. Configurar as Tabelas (Migrations)

Com o banco rodando na porta `1433`, precisamos aplicar o mapeamento do Entity Framework para criar as tabelas estruturais.

Abra o **Package Manager Console** no Visual Studio 2026 e execute:

```powershell
Update-Database

```

*(Alternativa via terminal CLI: `dotnet ef database update`)*

### 4. Executar a API

Basta dar o "Play" (F5) no Visual Studio 2026. A documentação do Swagger abrirá automaticamente no seu navegador, pronta para testes.

---

## 🔐 Credenciais de Acesso Inicial (Seed)

Para facilitar os testes, a API possui um *Seed* que injeta um usuário administrador no banco de dados assim que a aplicação sobe pela primeira vez. Utilize estas credenciais para gerar o seu Token JWT no endpoint de `/login`:

* **E-mail:** `admin@admin.com`
* **Senha:** `123456`

---

## 🏗️ Arquitetura e Padrões

O projeto foi refatorado para seguir estritamente os princípios de **Clean Architecture** e **SOLID**:

* **Controllers:** Responsáveis apenas por receber a requisição HTTP e retornar a resposta padronizada (`CartSysResponse`). Não possuem regras de negócio.
* **Services (Regra de Negócio):** Onde a lógica da aplicação reside. Fazem a ponte entre os Controllers e os Repositórios. Ex: `DesenvolvedorService`, `AuthService`.
* **Repositories (Persistência):** Isolam o Entity Framework Core. Os serviços não sabem como os dados foram salvos, apenas interagem com as interfaces (`ICidadeRepository`, `IUsuarioRepository`, etc).
* **Notification Pattern:** Utilização de uma interface `INotifications` injetada via Injeção de Dependência para capturar e centralizar erros de domínio, eliminando o lançamento excessivo de `Exceptions` para controle de fluxo.