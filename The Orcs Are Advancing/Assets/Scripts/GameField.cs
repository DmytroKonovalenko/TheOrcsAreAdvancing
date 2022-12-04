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
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                if(x>0)
                {
                    GameTile.MakeWestEastNeigbourth(tile,_tiles[i-1]);
                }
                if(y>0)
                {
                    GameTile.MaleNorthSouthNeigbourth(tile, _tiles[i - size.x]);
                }
            }
        }
    }

}
