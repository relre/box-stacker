using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void SceneChange(string scene_Name)
    {
        SceneManager.LoadScene(scene_Name);
    }
}
