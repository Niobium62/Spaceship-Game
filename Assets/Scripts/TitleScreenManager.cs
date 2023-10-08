using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject controlsScreen;
    // Start is called before the first frame update
    public void ShowTitleScreen()
    {
        titleScreen.gameObject.SetActive(true);
        controlsScreen.gameObject.SetActive(false);
    }
    public void ShowControlsScreen()
    {
        titleScreen.gameObject.SetActive(false);
        controlsScreen.gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        StartCoroutine(PlayGameHelper());
    }

    IEnumerator PlayGameHelper()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MainGame");
    }
}
