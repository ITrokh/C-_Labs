/// <summary>
/// Лабораторная №1
/// № в списке-23
/// Иван Трохименко IP-64
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRoads.Helpers.Input;

namespace HouseRoads
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new InputController();
            int amount = input.GetHousesCount();
            var roads = input.GetRoads(amount);
            Entities.Map map = new Entities.Map(amount, roads);

            map.GetMinimalPosition();

            Console.ReadKey();
        }
    }
}
