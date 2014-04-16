using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class SubsphereSeeker :
    MonoBehaviour
{
    [SerializeField]
    private GameObjectSeekerDepthPair[] _gameObjectSeekerDepthPairs;

    public IEnumerable<Subsphere> GetSubspheres()
    {
        return
            from subsphereCollection in
                from gameObjectSeekerDepthPair in _gameObjectSeekerDepthPairs
                select GetSubspheresFromGameObject(gameObjectSeekerDepthPair.GameObject, gameObjectSeekerDepthPair.SeekerDepth)
            from subsphere in subsphereCollection
            select subsphere;
    }

    private static IEnumerable<Subsphere> GetSubspheresFromGameObject(GameObject objectToSeek, int depth)
    {
        IEnumerable<Subsphere> result = new Subsphere[0];
        IEnumerable<Transform> currentGameObjectsToSeek = new Transform[] { objectToSeek.transform };

        do
        {
            var newGameObjectsToSeek = new HashSet<Transform>();

            foreach (var currentGameObject in currentGameObjectsToSeek)
            {
                result = result.Concat(currentGameObject.GetComponents<Subsphere>());
                foreach (var child in currentGameObject.transform)
                {
                    newGameObjectsToSeek.Add((Transform)child);
                }
            }

            currentGameObjectsToSeek = newGameObjectsToSeek;
            
            --depth;
        }
        while (depth >= 0);

        return result;
    }
}

[Serializable]
public sealed class GameObjectSeekerDepthPair :
    IEquatable<GameObjectSeekerDepthPair>
{
    [SerializeField]
    private GameObject _gameObject;

    [SerializeField]
    private int _seekerDepth = 0;

    public GameObject GameObject { get { return _gameObject; } }
    public int SeekerDepth { get { return _seekerDepth; } }

    public GameObjectSeekerDepthPair(GameObject gameObject)
    {
        _gameObject = gameObject;
    }
    public override bool Equals(object obj)
    {
        return Equals(obj as GameObjectSeekerDepthPair);
    }

    public bool Equals(GameObjectSeekerDepthPair other)
    {
        return
            other != null &&
            _gameObject.Equals(other._gameObject) &&
            _seekerDepth.Equals(other._seekerDepth);
    }

    public override int GetHashCode()
    {
        return
            _gameObject.GetHashCode() ^
            _seekerDepth.GetHashCode();
    }

    public GameObjectSeekerDepthPair(GameObject gameObject, int seekerDepth)
    {
        _gameObject = gameObject;
        _seekerDepth = seekerDepth;
    }
}
