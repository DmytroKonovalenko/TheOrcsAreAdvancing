using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameTileContent;

[CreateAssetMenu]

public class ContentFactory : ScriptableObject
{
    [SerializeField] private GameTileContent _destinationPrefabs;
    [SerializeField] private GameTileContent _enemyPrefabs;
  public void Reclaim(GameTileContent content)
    {
        Destroy(content.gameObject);
    }



    public GameTileContent Get (GameTileContentType type)
    {
        switch(type)
        {
            case GameTileContentType.Destination:
                return Get(_destinationPrefabs);
            case GameTileContentType.Empty:
                return Get(_enemyPrefabs);
        }
        return null;
    }
    private GameTileContent Get(GameTileContent prefab)
    {
        GameTileContent instance = Instantiate(prefab);
        instance.OriginFactory = this;
        MoveFactoryScene(instance.gameObject);
        return instance;
    }
    private Scene _contentScene;

    private void MoveFactoryScene(GameObject o)
    {
        if (!_contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                _contentScene = SceneManager.GetSceneByName(name);
                if (!_contentScene.isLoaded)
                {
                    _contentScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                _contentScene = SceneManager.CreateScene(name);
            }
        }
        SceneManager.MoveGameObjectToScene(o, _contentScene);
    }
}
