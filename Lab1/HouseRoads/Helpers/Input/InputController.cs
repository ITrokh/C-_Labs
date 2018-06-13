using HouseRoads.Entities;
using System;
using System.Collections.Generic;

namespace HouseRoads.Helpers.Input
{
    /// <summary>
    /// Класс предоставляет функциональность 
    /// для взаимодействия с пользователем и получения ввода
    /// </summary>
    class InputController : Input
    {
        int Input.GetHousesCount()
        {
            int amount = 0;
            do
            {
                Console.WriteLine("Введите количество домов");
                string input = Console.ReadLine();
                //  Проверка, содержит ли вход только цифры
                if (!IsDigitsOnly(input))
                    continue;
                try
                {
                    amount = Convert.ToInt32(input);
                }
                catch
                {
                    continue;
                }
            }
            while (amount <= 0);

            return amount;
        }

        List<Road> Input.GetRoads(int houseCount)
        {
            if (houseCount <= 0) throw new ArgumentException("количество домов должно быть больше 0");

            List<Road> roads = new List<Road>();

            Road tmp = new Road();

            int count = 1;

            string input = "";

            int house1 = 0;
            int house2 = 0;

            do
            {
                try
                {
                    Console.WriteLine("Дорога № {0}", count);

                    Console.WriteLine("Введите номер 1 дома\nОт 1 To {0}", houseCount);
                    input = Console.ReadLine();

                    if (!IsDigitsOnly(input))
                        continue;

                    house1 = Convert.ToInt32(input);

                    if (house1 <= 0)
                        continue;

                    
                    Console.WriteLine("Введите номер 2 дома\nОт 1 To {0}", houseCount);
                    input = Console.ReadLine();

                    
                    if (!IsDigitsOnly(input))
                        continue;

                    house2 = Convert.ToInt32(input);

                    if (house2 <= 0)
                        continue;

                    
                    Console.WriteLine("Введите длину дороги(должна быть 0)");
                    input = Console.ReadLine();
                    if (!IsDigitsOnly(input))
                        continue;


                    tmp.House1 = house1;
                    tmp.House2 = house2;
                    tmp.Length = Convert.ToInt32(input);

                    //  Добавить в список и увеличить номер дороги
                    roads.Add(tmp);
                    count++;
                }
                catch
                {
                    continue;
                }

            }
            while (input.ToLower() != "exit");

            return roads;

        }

        //Проверяет, введины ли только цифры
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
