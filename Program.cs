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
            Console.WriteLine("Pressione e segure a tecla 'M' para falar...");

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

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.M)
                {
                    Console.WriteLine("Ouvindo...");
                    await recognizer.StartContinuousRecognitionAsync();

                    while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.M)
                    {
                        await Task.Delay(100);
                    }

                    Console.WriteLine("Parando...");
                    await recognizer.StopContinuousRecognitionAsync();
                }
                await Task.Delay(100);
            }
        }
    }
}