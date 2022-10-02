using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex:2);
    }

    public void GoToInstructionScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
