using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RestartGame : MonoBehaviour
{
    private List<GameObjectState> initialStates = new List<GameObjectState>();

    private void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            initialStates.Add(new GameObjectState(obj, obj.activeSelf));
        }
    }

    public void ResetScene()
    {
        foreach (GameObjectState state in initialStates)
        {
            state.GameObject.SetActive(state.InitialActiveState);
            Collectable.totalCollectables = 0;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private class GameObjectState
    {
        public GameObject GameObject { get; private set; }
        public bool InitialActiveState { get; private set; }

        public GameObjectState(GameObject gameObject, bool initialActiveState)
        {
            GameObject = gameObject;
            InitialActiveState = initialActiveState;
        }
    }
}
