public class Program
{
    public static void Main(string[] args)
    {
        bool continueRunning = true;
        while (continueRunning)
        {
            try
            {
                Console.WriteLine("Укажите путь к файлу:");
                string filePath = Console.ReadLine();

                if (string.IsNullOrEmpty(filePath))
                {
                    Console.WriteLine("Неправильный путь к файлу");
                    return;
                }

                Dictionary<char, int> letterFrequency = new Dictionary<char, int>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        char[] charLineArray = line.ToLower().ToCharArray();
                        foreach (char c in charLineArray)
                        {
                            if (char.IsWhiteSpace(c) || !char.IsLetter(c)) continue;

                            if (letterFrequency.ContainsKey(c))
                                letterFrequency[c] += 1;
                            else
                                letterFrequency.Add(c, 1);
                        }
                    }
                }
                if (letterFrequency.Count == 0)
                {
                    Console.WriteLine("Файл пуст или не содержит букв");
                    return;
                }

                int maxFrequency = letterFrequency.Values.Max();
                int maxBarLength = 100;

                Console.WriteLine("Гистограмма частоты символов:");
                foreach (var keyValuePair in letterFrequency.OrderByDescending(kvp => kvp.Value))
                {
                    int barLength = (int)((double)keyValuePair.Value / maxFrequency * maxBarLength);
                    Console.WriteLine($"{keyValuePair.Key}: {new string('*', barLength)} ({keyValuePair.Value})");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите 'Y' для анализа нового файла или любую другую клавишу для выхода");
            if (Console.ReadKey().Key != ConsoleKey.Y)
            {
                continueRunning = false;
            }
            Console.Clear(); 
        }
    }
}
