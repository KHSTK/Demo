using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public string Name;
    // Start is called before the first frame update
    public void Jump()
    {
        SceneManager.LoadScene(Name);
    }
}
