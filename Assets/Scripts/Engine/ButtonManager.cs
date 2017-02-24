using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Loads a scene
     * @param level The name of the scene to load
     */
    public void LoadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    /**
     * Closes the game
     */
    public void ExitGame() {
        Application.Quit();
    }
}