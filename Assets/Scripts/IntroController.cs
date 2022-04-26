using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroController : MonoBehaviour
{
    public PlayableDirector director;
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void Update()
    {

    }

    public void MoveEgg() {
        director.Play();
        StartCoroutine(ExitIntro());
    }

    IEnumerator ExitIntro() {
        yield return new WaitForSeconds(3.5f);
        FindObjectOfType<LevelController>().LoadLevel(2);
    }
}
