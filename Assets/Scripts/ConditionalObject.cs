using UnityEngine;

public class ConditionalObject : MonoBehaviour
{
    public GameObject requiredCollectable;

    private void Start()
    {
        gameObject.SetActive(false); 
        GameManager.Instance.RegisterConditional(this); 
    }

    public void Evaluate()
    {
        if (requiredCollectable == null) return;
        gameObject.SetActive(GameManager.Instance.IsCollected(requiredCollectable.name));
    }
}