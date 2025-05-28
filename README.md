# 💸 Pix Charge System

Sistema completo de **emissão de cobranças via PIX**, com suporte a cobranças avulsas e recorrentes, feito em `.NET 8`, `SQL Server` e `Next.js`.

---

## 🚀 Tecnologias

- **Back-end**: ASP.NET Core 8 (Web API)
- **Front-end**: Next.js 14 com TypeScript
- **Banco de Dados**: SQL Server 2022 (Docker)
- **Infraestrutura**: Docker + Docker Compose
- **Gerenciamento**: Docker Desktop
- **Documentação**: Swagger UI

---

## 📁 Estrutura do Projeto

```bash
pix-charge-system/
├── backend/
│   └── PixCharge.API/             # API .NET 8
├── frontend/
│   └── pix-charge-app/            # Aplicação Next.js
├── docker-compose.yml             # Orquestração dos containers
└── README.md                      # Documentação do projeto
```

---

## 🐳 Como subir o ambiente

### Pré-requisitos:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (caso desenvolva local)
- Node.js 20+ (caso rode o front localmente fora do container)

### Subir tudo com Docker:

```bash
docker-compose up --build -d
```

---

## 🔍 Endpoints disponíveis

| Serviço        | URL                        |
|----------------|----------------------------|
| API (.NET 8)   | http://localhost:5000/swagger |
| Front-end (Next.js) | http://localhost:3000 |
| Banco de Dados | `localhost:1433` (SQL Server) |
| Usuário:       | `sa`                        |
| Senha:         | `Your_password123`         |

---

## 🛠️ Desenvolvimento local (sem Docker)

### Rodar API:

```bash
cd backend/PixCharge.API
dotnet run
```

### Rodar Front-end:

```bash
cd frontend/pix-charge-app
npm install
npm run dev
```

---

## 📌 Em breve

- [ ] Criação de clientes
- [ ] Emissão de cobranças avulsas PIX
- [ ] Cobranças recorrentes
- [ ] Notificações por e-mail ou WhatsApp
- [ ] Dashboard financeiro
- [ ] Integração com Mercado Pago

---

## 🧑‍💻 Autor

Desenvolvido por **Pedro Vinícios**  
📧 [pedrooviniciossantos@gmail.com]  
🔗 [linkedin.com/in/pedro-vinicios](https://linkedin.com/in/pedro-vinicios)

---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).