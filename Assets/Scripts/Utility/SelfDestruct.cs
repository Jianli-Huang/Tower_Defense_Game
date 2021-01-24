using UnityEngine;

/// <summary>
/// Self destructs the attached GameObject after the set amount of time
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    public float timeInSecondsBeforeDestruction = 3f;

    void Start()
    {
        Destroy(gameObject, timeInSecondsBeforeDestruction);
    }
}
