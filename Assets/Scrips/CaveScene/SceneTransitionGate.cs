using UnityEngine;

public class SceneTransitionGate : MonoBehaviour
{
    public string sceneToLoad;    // Gõ: Map_2
    public string spawnPointName; // Gõ: Spawn_From_Map1
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            SceneTransitionManager.Instance.Transition(sceneToLoad, spawnPointName);
    }
}