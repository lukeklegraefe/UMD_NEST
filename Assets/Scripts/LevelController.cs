using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Animator transition;
    void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex) {
            case 2:
                FindObjectOfType<AudioController>().Play("HubMusic");
                break;
            case 3:
                FindObjectOfType<AudioController>().Play("TropicalMusic");
                break;
            case 4:
                FindObjectOfType<AudioController>().Play("JungleMusic");
                break;
            case 5:
                FindObjectOfType<AudioController>().Play("CityMusic");
                break;
            case 6:
                FindObjectOfType<AudioController>().Play("SnowMusic");
                break;
            default:
                FindObjectOfType<AudioController>().Play("HubMusic");
                break;
        }
    }

    public void LoadLevel(int index) {
        if(SceneManager.GetActiveScene().buildIndex != 0) {
            FindObjectOfType<Player>().SaveData();
        }
        StopAllCoroutines();
        StartCoroutine(PlayTransition(index));
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    IEnumerator PlayTransition(int index) {
        transition.SetTrigger("fade");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(index);
    }
}
