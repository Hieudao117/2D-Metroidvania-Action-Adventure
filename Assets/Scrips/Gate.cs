using UnityEngine;

public class Gate : MonoBehaviour
{
    public string sceneToLoad;    
    public string spawnPointName;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            SceneTransitionManager.Instance.Transition(sceneToLoad, spawnPointName);
    }
}
