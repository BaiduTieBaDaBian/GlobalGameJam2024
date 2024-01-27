using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{

    public void StartLevel01()
    {
        SceneManager.LoadScene("Level01");
    }
}
