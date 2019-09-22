using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreloadInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		SceneManager.LoadScene(SceneController.mainMenuIndex);
    }
}
