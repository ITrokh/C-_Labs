using System.Collections.Generic;
using System;


namespace HouseRoads.Entities
{
    /// <summary>
    /// Содержит дома и дороги, соединенные между собой
    /// Содержит логику для поиска наименьших маршрутов
    /// </summary>
    class Map
    {
        private const int Infinity = 999999;

        private int _housesAmount = 0;

        private List<Road> _roads = null;

        private MeetPoint _meetPoint;

        private int[,] _routes = null;

        public Map(int housesAmount, List<Road> roads)
        {
            if (housesAmount <= 0 || roads == null || roads.Capacity < 0)
                throw new ArgumentException("Неправильные параметры конструктора карты!!");

            this._housesAmount = housesAmount;
            this._roads = roads;
        }

        //  нахождение минимального положение
        public void GetMinimalPosition()
        {
            BuildDistanceMatrix();

            int minimalSumm = Infinity;
            int tmpSumm = 0;
            int minimalHouse = 0;

            //  найти все суммы каждого дома к другому
            for (int i = 0; i < _housesAmount; i++)
            {
                for(int j = 0; j < _housesAmount; j++)
                {
                    tmpSumm += _routes[i, j];
                }
                if(minimalSumm > tmpSumm)
                {
                    minimalSumm = tmpSumm;
                    minimalHouse = i;
                }
                tmpSumm = 0;
            }

            //  найти лучшее место для встречи
            FindMeetPoint(minimalHouse);

            if (_meetPoint.FromHouse1ToPoint == 0)
            {
                Console.WriteLine("Номер дома {0}", _meetPoint.House1 + 1);
            }
            else
            {
                Console.WriteLine("Точка встречи");
                Console.WriteLine("Дом 1 : {0}", _meetPoint.House1 + 1);
                Console.WriteLine("Дом 2 : {0}", _meetPoint.House2 + 1);
                Console.WriteLine("От дома 1 к точке : {0}", _meetPoint.FromHouse1ToPoint);
            }

        }


        //  найти матрицу расстояний
        private void BuildDistanceMatrix()
        {
            //  создать матрицу длины
            _routes = new int[_housesAmount, _housesAmount];

            BuildAdjacencyMatrix(ref _routes, _roads.ToArray());

            //  построить матрицу длины
            FloidUorshall(ref _routes);

            //  заполнить основную диагональ
            for (int i = 0; i < _housesAmount; i++)
            {
                _routes[i, i] = 0;
            }


        }

        //  найти точку встречи
        private void FindMeetPoint(int bestHouse)
        {
            _meetPoint = new MeetPoint();

            _meetPoint.FromHouse1ToPoint = 0;
            _meetPoint.House1 = bestHouse;
            _meetPoint.House2 = bestHouse;

            //  разница между самым большим и наименьшим путем
            int delta = 0;

            //  сумма всех путей
            int summ = 0;

            //  самые большие и наименьшие числа на маршруте
            int biggest = 0;
            int smallest = Infinity;

            for(int i = 0; i < _housesAmount; i++)
            {
                summ += _routes[bestHouse, i];

                if (_routes[bestHouse, i] > biggest)
                    biggest = _routes[bestHouse, i];

                if (smallest > _routes[bestHouse, i])
                    smallest = _routes[bestHouse, i];

            }

            //  вычислить дельту для этого маршрута
            delta = biggest - smallest;


            // Итерирует каждую из дорог и ставит точку на дорогу
            // найти наименьшие маршруты для всех домов
            // находим summ и delta
            // если summ <= newSumm и delta <newDelta
            // Этот маршрут лучшй для встречи
            // заполняем _meetPoint и продолжаем взаимодействие

            //  массив дорог
            Road[] roads = _roads.ToArray();

            // новый пересчитанный маршрут
            // от нашей точки до всех домов
            int[] route = new int[_housesAmount];

            //  текущая точка
            MeetPoint point = new MeetPoint();

            for(int i = 0; i < roads.Length; i++)
            {
                Road tmp = new Road();
                tmp = roads[i];

                
                for(int j = 1; j < tmp.Length; j++)
                {
                    point.House1 = tmp.House1 - 1;
                    point.House2 = tmp.House2 - 1;
                    point.Length = tmp.Length;
                    point.FromHouse1ToPoint = point.Length - j;

                    // расстояния от текущей точки ко всем домам
                    int[] distances = new int[_housesAmount];

                    //  устанавливаем все на 0
                    for (int x = 0; x < _housesAmount; x++)
                    {
                        distances[x] = 0;
                    }

                    distances[point.House1] = point.FromHouse1ToPoint;
                    distances[point.House2] = point.Length - point.FromHouse1ToPoint;

                    //  инициализируем расстояния
                    for (int k = 0; k < distances.Length; k++)
                    {
                        if(k != point.House1 && k != point.House2)
                        {
                            //  расстояние от домов до k
                            int viaHouse1 = _routes[point.House1, k];
                            int viaHouse2 = _routes[point.House2, k];

                            //  находим лучший маршрут
                            if(viaHouse1 >= viaHouse2)
                            {
                                distances[k] = point.Length - point.FromHouse1ToPoint + viaHouse2;
                            }
                            else
                            {
                                distances[k] = point.FromHouse1ToPoint + viaHouse1;
                            }
                        }
                    }

                    
                    int newSumm = 0;
                    int newDelta = 0;

                    int newBiggest = 0;
                    int newSmallest = Infinity;

                    for (int x = 0; x < _housesAmount; x++)
                    {
                        newSumm += _routes[bestHouse, x];

                        if (distances[x] > newBiggest)
                            newBiggest = distances[x];

                        if (newSmallest > distances[x])
                            newSmallest = distances[x];

                    }

                    //  вычислить дельту для этого маршрута
                    newDelta = newBiggest - newSmallest;

                    // если найденный маршрут лучше, то заменяем
                    if (summ >= newSumm && delta > newDelta)
                    {
                        summ = newSumm;
                        delta = newDelta;

                        _meetPoint = point;
                    }


                }
            }


        }


        //  строим матрицу смежности
        private void BuildAdjacencyMatrix(ref int[,] routes, Road[] roads)
        {

            //  установить все длины в бесконечность Infinity
            for (int i = 0; i < _housesAmount; i++)
            {
                for (int j = 0; j < _housesAmount; j++)
                {
                    _routes[i, j] = Infinity;
                }
            }

            //  инициализировать матрицу длины в качестве матрицы смежности
            foreach (var road in _roads)
            {
                var i = road.House1 - 1;
                var j = road.House2 - 1;
                var length = road.Length;

                if (_routes[i, j] > length)
                    _routes[i, j] = length;

                if (_routes[j, i] > length)
                    _routes[j, i] = length;
            }
        }

        private void FloidUorshall(ref int[,] routes)
        {
            int lenght = _housesAmount;
            for (int k = 0; k < lenght; k++)
            {
                for (int i = 0; i < lenght; i++)
                {
                    for (int j = 0; j < lenght; j++)
                    {
                        routes[i, j] = Math.Min(routes[i, j], routes[i, k] + routes[k, j]);
                    }
                }
            }
        }

  

        private struct MeetPoint
        {
            public int House1;
            public int House2;
            public int Length;
            public int FromHouse1ToPoint;
        }



        private class Route
        {
            //  Все номера домов, которые включены в этот маршрут
            private List<int> _vertexes = null;

            //  Длина маршрута
            public int Length { get; set; }

            //  Получаем все вершины
            public int[] Vertexes
            {
                get
                {
                    int[] vertexes = _vertexes.ToArray();
                    return vertexes;
                }
            }

            //  Добавляет новую вершину
            public void AddVertex(int house)
            {
                _vertexes.Add(house);
            }

            //  Стандартный конструктор
            public Route()
            {
                this._vertexes = new List<int>();
            }
        }
    }
}
