using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    private GameTile _north, _east, _south, _west,_nextOnPath;
    private int _distance;
    public bool HasPath=>_distance!=int.MaxValue;
    public static void MakeWestEastNeigbourth(GameTile east,GameTile west)
    {
        west._east = east;
        east._west = west;

    }
    public static void MaleNorthSouthNeigbourth(GameTile north,GameTile south)
    {
        north._south = south;
        south._north = north;
    }

    public void ClearPath()
    {
        _distance = int.MaxValue;
        _nextOnPath = null;
    }
    public void BecomeDestination()
    {
        _distance = 0;
        _nextOnPath = null;
    }  
    
    public GameTile GrowPathTo(GameTile neighbor)
    {
        if(!HasPath||neighbor==null||neighbor.HasPath)
        {
            return null;
        }
        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;
        return neighbor;
    }

    public GameTile GrowPaythNorh() => GrowPathTo(_north);
    public GameTile GrowPaythWest() => GrowPathTo(_west);
    public GameTile GrowPaythSouth() => GrowPathTo(_south);
    public GameTile GrowPaythEast() => GrowPathTo(_east);
}
