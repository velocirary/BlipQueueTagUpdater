# Blip Queue Tag Updater

Este projeto é uma aplicação em .NET que automatiza a atualização de **tags** em **filas de atendimento** na plataforma **Blip**.
É possível realizar a atualização apenas para **filas específicas**, listadas em um arquivo chamado **queues.json**.

---

## 📌 Visão Geral

A aplicação se conecta à API HTTP do Blip, obtém todas as filas de atendimento e atualiza as **tags** de todas as filas.

---

## ⚙️ Tecnologias Utilizadas

- [.NET 6](https://dotnet.microsoft.com/)
- [Blip HTTP API](https://docs.blip.ai)
- `HttpClient` com autenticação via `BotKey`
- `IHostedService` via `Microsoft.Extensions.Hosting`
- `System.Text.Json` para serialização

---

## 🚀 Como Executar

### 1. Requisitos

- .NET 6 SDK ou superior
- Chave de autenticação (`BotKey`) da Blip
- Permissão para consultar e atualizar filas via Blip HTTP API

### 2. Clonando o projeto

```bash
git clone https://github.com/velocirary/BlipQueueTagUpdater.git
cd BlipQueueTagUpdater
```

### 3. Configuração

Edite o arquivo `appsettings.json` com os dados do seu bot:

```json
{
  "Blip": {
    "BaseUrl": "https://seu-subdominio.http.msging.net/commands",
    "To": "postmaster@desk.msging.net",
    "BotKey": "SUA_CHAVE_DO_BOT",
    "Tags": [ "Tag1", "Tag2", "Tag3" ]
  }
}
```

### 4. Atualização de Filas Específicas

Caso queira atualizar apenas filas específicas, crie um arquivo queues.json na raiz do projeto com o seguinte formato:

```json
{
  "AllowedQueues": [
    "fila-exemplo1",
    "fila-exemplo2",
    "fila-exemplo3"
  ]
}
```
A aplicação verificará se esse arquivo existe e, se encontrado, fará a atualização somente das filas listadas.

## 🧠 Lógica do Projeto

A classe `QueueService` executa o processo principal da aplicação, oferecendo duas formas de execução:

### 🔁 `ExecuteAllAsync()`

- Obtém **todas** as filas de atendimento através do método `BlipClient.GetAttendanceQueuesAsync()`.
- Aplica as tags configuradas no `appsettings.json` para todas as filas obtidas.

### 🎯 `ExecuteAllowedOnlyAsync()`

- Verifica se existe o arquivo `queues.json` na raiz do projeto.
- Lê a lista de filas permitidas dentro da propriedade `AllowedQueues` do arquivo `queues.json`.
- Filtra as filas obtidas da Blip, mantendo apenas as que estão listadas no `queues.json`.
- Atualiza as tags **somente** nessas filas filtradas.

## 🗂 Estrutura

```
BlipQueueTagUpdater/
├── Infrastructure/
│   ├── BlipClient.cs         # Implementação da chamada HTTP para o Blip
│   ├── BlipOptions.cs        # Classe de configuração mapeada via IOptions
│   └── IBlipClient.cs        # Interface da camada HTTP
├── Models/
│   ├── AttendanceQueue.cs    # Modelo de uma fila de atendimento
│   ├── QueueConfig.cs        # Modelo da Condiguração das Filas
│   └── BlipResponse.cs       # Modelo da resposta da API
├── Services/
│   ├── QueueService.cs       # Orquestra a lógica de atualização
│   └── IQueueService.cs      # Interface da camada de serviço
├── Program.cs                # Ponto de entrada da aplicação
├── appsettings.json          # Configurações do ambiente
└── BlipQueueTagUpdater.csproj
```

---

## ✅ Funcionalidades

- 🔎 Consulta automatizada de filas de atendimento Blip
- 🏷️ Atualização em massa de tags em filas específicas
- 📂 Atualização seletiva baseada em queues.json
- ⚙️ Parametrização simples via `appsettings.json`
