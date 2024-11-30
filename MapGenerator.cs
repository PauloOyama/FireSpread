using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Program
{
    static string[,] GenerateIslandMap(int height, int width, List<string> vegetationTypes, int numIslands, int islandRadius)
    {
        // Inicializando o mapa vazio
        string[,] map = new string[height, width];

        // Função auxiliar para verificar os limites do mapa
        bool IsInsideBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;

        // Gerando ilhas para cada tipo de vegetação
        Random rand = new Random();
        foreach (var vegetation in vegetationTypes)
        {
            for (int i = 0; i < numIslands; i++)
            {
                // Escolhendo um ponto aleatório como centro da ilha
                int centerX = rand.Next(0, width);
                int centerY = rand.Next(0, height);

                // Expandindo a partir do centro
                for (int dx = -islandRadius; dx <= islandRadius; dx++)
                {
                    for (int dy = -islandRadius; dy <= islandRadius; dy++)
                    {
                        if (IsInsideBounds(centerX + dx, centerY + dy))
                        {
                            double distance = Math.Sqrt(dx * dx + dy * dy);
                            if (distance <= islandRadius && rand.NextDouble() < 0.8) // 80% de chance de preencher
                            {
                                map[centerY + dy, centerX + dx] = vegetation;
                            }
                        }
                    }
                }
            }
        }

        // Preenchendo áreas vazias com vegetação padrão
        string defaultVegetation = vegetationTypes[0];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (string.IsNullOrEmpty(map[y, x]))
                {
                    map[y, x] = defaultVegetation;
                }
            }
        }

        return map;
    }

    static List<List<string>> ConvertMapToList(string[,] map)
    {
        int height = map.GetLength(0);
        int width = map.GetLength(1);

        var listMap = new List<List<string>>();
        for (int y = 0; y < height; y++)
        {
            var row = new List<string>();
            for (int x = 0; x < width; x++)
            {
                row.Add(map[y, x]);
            }
            listMap.Add(row);
        }

        return listMap;
    }

    // public static void Main()
    // {
    //     // Configurações do mapa
    //     int height = 200;
    //     int width = 200;
    //     List<string> vegetationTypes = new List<string> { "RU", "T", "S", "E", "RI" };
    //     int numIslands = 60; // Número de ilhas por vegetação
    //     int islandRadius = 10; // Raio médio das ilhas

    //     // Gerando o mapa
    //     string[,] islandMap = GenerateIslandMap(height, width, vegetationTypes, numIslands, islandRadius);

    //     // Convertendo para List<List<string>>
    //     List<List<string>> listMap = ConvertMapToList(islandMap);

    //     // Serializando para JSON
    //     string json = JsonSerializer.Serialize(listMap, new JsonSerializerOptions { WriteIndented = true });

    //     // Salvando o JSON em um arquivo
    //     File.WriteAllText("map.json", json);

    //     Console.WriteLine("Mapa salvo como map.json!");
    // }


}
