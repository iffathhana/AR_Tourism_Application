using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenceManagescripts : MonoBehaviour
{
    public void LoadScence(string scenceName)
    {
        SceneManager.LoadScene(scenceName);
    }
}
