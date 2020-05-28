using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadScene : MonoBehaviour
{
    public GameObject dialog;
    private VideoPlayer vp;
    private bool reverse = false;
    private bool fadeout = false;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        vp = GameObject.Find("Spin").GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeout)
        {
            if (vp.targetCameraAlpha<1&&!reverse)
            {
                vp.targetCameraAlpha += 0.011f;
                if (vp.targetCameraAlpha>=1)
                {
                    reverse = true;
                    GameObject.Find("BG").SetActive(false);
                    GameObject.Find("Player").SetActive(false);
                    dialog.SetActive(false);
                    GameObject.Find("Canvas").SetActive(false);
                }
            }
            if (reverse)
            {
                vp.targetCameraAlpha -= 0.011f;
                if (vp.targetCameraAlpha <= 0)
                {
                    fadeout = false;
                }
            }
            
        }
    }

    public void play()
    {
        if (!isPlaying)
        {
            GameObject.Find("Player").GetComponent<Animator>().SetBool("Fall", true);
            GameObject.Find("z").SetActive(false);
            isPlaying = true;
            StartCoroutine(delay1());
        }
    }

    private IEnumerator delay1()
    {
        yield return new WaitForSeconds(3.4f);
        dialog.SetActive(true);
        StartCoroutine(delay2());
    }

    private IEnumerator delay2()
    {
        yield return new WaitForSeconds(2.9f);
        fadeout = true;
        StartCoroutine(delay3());
    }

    private IEnumerator delay3()
    {
        yield return new WaitForSeconds(3.4f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
