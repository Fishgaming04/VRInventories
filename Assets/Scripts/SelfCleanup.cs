using UnityEngine;

public class SelfCleanup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExerciseSingleton.Instance.CleanupEvent += SelfDestruct;
    }

    private void OnDestroy()
    {
        ExerciseSingleton.Instance.CleanupEvent -= SelfDestruct;
    }

    private void SelfDestruct()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);

        }
    }
}
