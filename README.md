# Speech-to-Text Translation com Azure Speech Services

Este projeto é uma aplicação C# que utiliza o **Azure Speech Services** para reconhecimento de fala e tradução em tempo real. O código converte áudio falado em inglês (`en-US`) para texto e o traduz para português (`pt-BR`).

## Pré-requisitos

1. **.NET SDK** instalado ([Baixar aqui](https://dotnet.microsoft.com/en-us/download))
2. **Conta no Azure** com um recurso do **Speech Services** criado
3. **Chave de Assinatura e Região** do Azure Speech Services
4. **Microfone configurado** no sistema para capturar o áudio

## Configuração

1. Clone este repositório ou crie um diretório e adicione o arquivo `Program.cs` com o código fornecido.
2. Substitua as credenciais no código:
   ```csharp
   string subscriptionKey = "SUA_CHAVE_AQUI";  
   string region = "SUA_REGIAO_AQUI";
   ```
3. Instale o pacote do Azure Cognitive Services Speech SDK:
   ```sh
   dotnet add package Microsoft.CognitiveServices.Speech
   ```

## Como Executar

1. Abra o terminal na pasta do projeto.
2. Compile e execute:
   ```sh
   dotnet run
   ```
3. O programa iniciará o reconhecimento de fala e exibirá as traduções em tempo real.
4. Pressione **Enter** para encerrar a aplicação.

## Logs e Depuração

- O programa gera um arquivo de log `speech_log.txt` com detalhes do reconhecimento de fala.
- Caso ocorra um erro, ele será exibido no console com detalhes do código e mensagem do erro.

## Licença

Este projeto está sob a licença MIT. Sinta-se à vontade para modificar e utilizar conforme necessário.

