using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(NextScene), 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextScene()
    {
        SceneManager.LoadScene(2);
    }
}
