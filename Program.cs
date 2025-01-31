using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using System.Threading;

class Program
{
    static async Task Main()
    {
        string subscriptionKey = "7deuIiXy1AgNTaa4wZsPeCNH56Z4Noew04c0XefQ5vPiWgQlqS7rJQQJ99BAACZoyfiXJ3w3AAAYACOGNr2c";
        string region = "brazilsouth";

        var config = SpeechTranslationConfig.FromSubscription(subscriptionKey, region);
        config.SpeechRecognitionLanguage = "pt-BR";
        config.AddTargetLanguage("en-US");

        using (var recognizer = new TranslationRecognizer(config))
        {
            Console.WriteLine("Pressione e segure a tecla 'M' para falar...");

            recognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.TranslatedSpeech)
                {
                    Console.WriteLine($"Texto: {e.Result.Text}");
                    foreach (var translation in e.Result.Translations)
                    {
                        Console.WriteLine($"Tradução ({translation.Key}): {translation.Value}");
                    }
                }
            };

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.M)
                    {
                        Console.WriteLine("Ouvindo...");
                        await recognizer.StartContinuousRecognitionAsync();

                        while (Console.KeyAvailable == false || Console.ReadKey(true).Key == ConsoleKey.M)
                        {
                            Thread.Sleep(100);
                        }

                        Console.WriteLine("Parando...");
                        await recognizer.StopContinuousRecognitionAsync();
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
