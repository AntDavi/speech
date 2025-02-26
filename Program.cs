using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;

class Program
{
    static async Task Main()
    {
        string subscriptionKey = "7deuIiXy1AgNTaa4wZsPeCNH56Z4Noew04c0XefQ5vPiWgQlqS7rJQQJ99BAACZoyfiXJ3w3AAAYACOGNr2c";
        string region = "brazilsouth";

        var config = SpeechTranslationConfig.FromSubscription(subscriptionKey, region);
        config.SpeechRecognitionLanguage = "pt-BR";
        config.AddTargetLanguage("en-US"); // Traduzir para inglês

        using (var recognizer = new TranslationRecognizer(config))
        using (var synthesizer = new SpeechSynthesizer(SpeechConfig.FromSubscription(subscriptionKey, region)))
        {
            Console.WriteLine("Ouvindo...");

            recognizer.Recognized += async (s, e) =>
            {
                if (e.Result.Reason == ResultReason.TranslatedSpeech)
                {
                    Console.WriteLine($"Texto: {e.Result.Text}");

                    foreach (var translation in e.Result.Translations)
                    {
                        Console.WriteLine($"Tradução ({translation.Key}): {translation.Value}");
                        await synthesizer.SpeakTextAsync(translation.Value);
                    }
                }
            };

            // Inicia o reconhecimento contínuo
            try
            {
                await recognizer.StartContinuousRecognitionAsync();
                Console.WriteLine("Reconhecimento de fala iniciado. Pressione qualquer tecla para parar...");
                Console.ReadKey();
                await recognizer.StopContinuousRecognitionAsync();
                Console.WriteLine("Reconhecimento de fala parado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
            // Mantém o programa em execução
            Console.WriteLine("Pressione qualquer tecla para parar...");
            Console.ReadKey();

            // // Para o reconhecimento contínuo
            // await recognizer.StopContinuousRecognitionAsync();
        }
    }
}