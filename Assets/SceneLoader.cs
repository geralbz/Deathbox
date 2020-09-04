using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int scene = 1;


    public void GoTo() => SceneManager.LoadScene(scene);
    public void Quit() => Application.Quit();
}
