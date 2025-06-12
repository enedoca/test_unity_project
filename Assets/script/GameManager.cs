using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject restartTextObject;

    void Start()
    {
        if (restartTextObject != null)
            restartTextObject.SetActive(false);
    }
    public void OnPlayerDestroyed()
    {
        if (restartTextObject != null)
            restartTextObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
