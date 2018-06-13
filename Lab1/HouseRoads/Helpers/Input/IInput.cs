namespace HouseRoads.Helpers.Input
{
   
    interface Input
    {
        int GetHousesCount();
        System.Collections.Generic.List<HouseRoads.Entities.Road> GetRoads(int houseCount);
    }
}
