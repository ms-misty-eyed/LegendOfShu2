using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private HashSet<string> _collectedObjects;
    private List<ConditionalObject> _conditionalObjects;

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

        //notify all conditional objects to re-evaluate
        foreach (ConditionalObject obj in _conditionalObjects)
        {
            obj.Evaluate();
        }
    }

    public bool IsCollected(string objectName)
    {
        return _collectedObjects.Contains(objectName);
    }
}