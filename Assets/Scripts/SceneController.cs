using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMainScene() 
    {
        throw new NotImplementedException();
        SceneManager.LoadScene(0);
    }

    public void OpenGameScene() 
    {
        SceneManager.LoadScene(0);
    }
}
