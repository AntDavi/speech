using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;

class Program
{
    static async Task Main()
    {
        string subscriptionKey = "INSIRA_AQUI_SUA_CHAVE";  
        string region = "INSIRA_AQUI_SUA_REGIAO";

        var config = SpeechTranslationConfig.FromSubscription(subscriptionKey, region);
        // Idioma do áudio de entrada
        config.SpeechRecognitionLanguage = "pt-BR";
        // Idioma de saída traduzido
        config.AddTargetLanguage("en-US"); 
        config.SetProperty(PropertyId.Speech_LogFilename, "speech_log.txt"); // Log de depuração

        using (var recognizer = new TranslationRecognizer(config))
        {
            Console.WriteLine("Iniciando reconhecimento de fala...");
            recognizer.Recognizing += (s, e) =>
            {
                Console.WriteLine($"DEBUG: Ouvido: {e.Result.Text}");
            };

            recognizer.Recognized += (s, e) =>
            {
                Console.WriteLine($"DEBUG: Status: {e.Result.Reason}");
                if (e.Result.Reason == ResultReason.TranslatedSpeech)
                {
                    Console.WriteLine($"Texto reconhecido: {e.Result.Text}");
                    foreach (var translation in e.Result.Translations)
                    {
                        Console.WriteLine($"Tradução [{translation.Key}]: {translation.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("DEBUG: Não foi possível reconhecer o áudio.");
                }
            };

            recognizer.Canceled += (s, e) =>
            {
                Console.WriteLine($"DEBUG: Erro! {e.Reason}, Código: {e.ErrorCode}, Mensagem: {e.ErrorDetails}");
            };

            await recognizer.StartContinuousRecognitionAsync();
            Console.WriteLine("Pressione Enter para encerrar...");
            Console.ReadLine();
            await recognizer.StopContinuousRecognitionAsync();
        }
    }
}
