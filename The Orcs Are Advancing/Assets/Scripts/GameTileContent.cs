using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTileContent : MonoBehaviour
{
    [SerializeField] private GameTileContentType _type;
    public GameTileContentType Type => _type;
    
    public ContentFactory OriginFactory { get; set; }
    
    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }

    public enum GameTileContentType
    {
        Empty,
        Destination
    }
}
