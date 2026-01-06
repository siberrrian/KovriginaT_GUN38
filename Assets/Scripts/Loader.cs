using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private int _index;

    public void LoadLevel()
    {
        switch (_index)
        {
            case 0:
                SceneManager.LoadScene(_index); 
                break;
            case 1:
                SceneManager.LoadScene(_index, LoadSceneMode.Additive);
                break;
        }
    }
}
