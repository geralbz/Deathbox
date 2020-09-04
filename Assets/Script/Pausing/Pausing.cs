using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour
{
    public static bool pause = false;
    [SerializeField]GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !pause)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) & pause)
        {
            UnPause();
        }
    }

    public void Pause()
    {
        pause = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
       
    }

    public void Quit()
    {
        Application.Quit();   
    }
}
