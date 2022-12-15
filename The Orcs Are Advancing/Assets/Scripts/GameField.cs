using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField]
    private Transform _ground;
    [SerializeField]
    private GameTile _tilePrefabs;
    private GameTile[] _tiles; 
    private Vector2Int _size;
    private Queue<GameTile> _searchFrontier = new Queue<GameTile>();

    public void Initialize(Vector2Int size)
    {
        _size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);
        _tiles = new GameTile[size.x * size.y];
        Vector2 offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);

        for(int i=0, y=0;y<size.y;y++)
        {
            for(int x=0;x<size.x;x++,i++)
            {
                GameTile tile =_tiles[i]= Instantiate(_tilePrefabs);
                tile.transform.SetParent(transform,false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0.01f, y - offset.y);

                if(x>0)
                {
                    GameTile.MakeWestEastNeigbourth(tile,_tiles[i-1]);
                }
                if(y>0)
                {
                    GameTile.MaleNorthSouthNeigbourth(tile, _tiles[i - size.x]);
                }
                tile.IsAlternative = (x & 1) == 0;
                if((y&1)==0)
                {
                    tile.IsAlternative = !tile.IsAlternative;
                }

            }
        }
        FindPath();
    }

    public void FindPath()
    {
        foreach(var tile in _tiles)
        {
            tile.ClearPath();
        }
        int destinationIndex = _tiles.Length / 2;
        _tiles[destinationIndex].BecomeDestination();
        _searchFrontier.Enqueue(_tiles[destinationIndex]);

        while(_searchFrontier.Count>0)
        {
            GameTile tile = _searchFrontier.Dequeue();
            if(tile!=null)
            {
                if(tile.IsAlternative)
                {
                    _searchFrontier.Enqueue(tile.GrowPaythNorh());
                    _searchFrontier.Enqueue(tile.GrowPaythSouth());
                    _searchFrontier.Enqueue(tile.GrowPaythEast());
                    _searchFrontier.Enqueue(tile.GrowPaythWest());
                }
                else
                {
                    _searchFrontier.Enqueue(tile.GrowPaythWest());
                    _searchFrontier.Enqueue(tile.GrowPaythEast());
                    _searchFrontier.Enqueue(tile.GrowPaythSouth());
                    _searchFrontier.Enqueue(tile.GrowPaythNorh());
                }
                

            }
        }
        foreach(var t in _tiles)
        {
            t.ShowPath();
        }
    }
public  GameTile GetTile(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray ,out hit))
        {
            int x = (int)(hit.point.x * _size.x * 0.5f);
            int y = (int)(hit.point.z * _size.y * 0.5f);
            if(x>=0&&x<_size.x&&y>=0&&y<_size.y)
            {
                return _tiles[x + y * _size.x];

            }
            
        }
        return null;
    }
}
