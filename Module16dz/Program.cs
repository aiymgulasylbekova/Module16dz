using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Module16dz
{
    public class Program
    {
        static string logFilePath = "log.txt";

        

        static void ViewDirectoryContents()
        {
            Console.Write("Введите путь к директории: ");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("Файлы:");
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }

                Console.WriteLine("\nДиректории:");
                foreach (string directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.Write("Введите путь к директории: ");
            string path = Console.ReadLine();

            Console.Write("Введите имя файла/директории: ");
            string name = Console.ReadLine();

            try
            {
                Console.Write("1 - Файл, 2 - Директория: ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    File.Create(Path.Combine(path, name)).Close();
                    Log($"Создан файл: {Path.Combine(path, name)}");
                }
                else if (choice == 2)
                {
                    Directory.CreateDirectory(Path.Combine(path, name));
                    Log($"Создана директория: {Path.Combine(path, name)}");
                }
                else
                {
                    Console.WriteLine("Некорректный выбор.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void DeleteFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Log($"Удален файл: {path}");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Log($"Удалена директория: {path}");
                }
                else
                {
                    Console.WriteLine("Файл или директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь для копии: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Log($"Скопирован файл: {sourcePath} -> {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Log($"Скопирована директория: {sourcePath} -> {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else
                {
                    Console.WriteLine("Файл или директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь для перемещения: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Log($"Перемещен файл: {sourcePath} -> {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Log($"Перемещена директория: {sourcePath} -> {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else
                {
                    Console.WriteLine("Файл или директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ReadFromFile()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();

            try
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine("Содержимое файла:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("Файл не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Введите текст для записи в файл (Ctrl+Z для завершения ввода):");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string line;
                while ((line = Console.ReadLine()) != null)
                {
                    writer.WriteLine(line);
                }
            }

            Log($"Записан текст в файл: {filePath}");
        }

        static void CopyDirectory(string sourceDir, string destDir)
        {
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string destDirName = Path.Combine(destDir, Path.GetFileName(dir));
                CopyDirectory(dir, destDirName);
            }
        }

        static void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} - {message}");
            }
        }
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла/директории");
                Console.WriteLine("3. Удаление файла/директории");
                Console.WriteLine("4. Копирование файла/директории");
                Console.WriteLine("5. Перемещение файла/директории");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("0. Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewDirectoryContents();
                        break;
                    case "2":
                        CreateFileOrDirectory();
                        break;
                    case "3":
                        DeleteFileOrDirectory();
                        break;
                    case "4":
                        CopyFileOrDirectory();
                        break;
                    case "5":
                        MoveFileOrDirectory();
                        break;
                    case "6":
                        ReadFromFile();
                        break;
                    case "7":
                        WriteToFile();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }
            }
            Console.ReadKey();
        }
    }

}

