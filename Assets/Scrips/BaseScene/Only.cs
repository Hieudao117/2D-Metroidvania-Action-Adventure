using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bootstrapper : MonoBehaviour
{
    public string firstMapName = "Forest";
    public string firstSpawnPointName = "SpawnPointPlayerBaseTo1";

    // Đổi Start() thành một hàm public thường
    public void NewGame()
    {
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.Transition(firstMapName, firstSpawnPointName);
        }
    }
}
