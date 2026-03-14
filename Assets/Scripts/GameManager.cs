using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private HashSet<string> _collectedObjects = new HashSet<string>();
    private List<ConditionalObject> _conditionalObjects = new List<ConditionalObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterConditional(ConditionalObject obj)
    {
        _conditionalObjects.Add(obj);
    }

    public void RegisterCollected(string objectName)
    {
        _collectedObjects.Add(objectName);

        foreach (ConditionalObject obj in _conditionalObjects)
        {
            if (obj == null) continue; //use continue not return, so the loop keeps going
            obj.Evaluate();
        }
    }

    public bool IsCollected(string objectName)
    {
        return _collectedObjects.Contains(objectName);
    }
}