using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _fieldSize;
    [SerializeField]
    private GameField _field;
    void Start()
    {
        _field.Initialize(_fieldSize);
    }

   
}
