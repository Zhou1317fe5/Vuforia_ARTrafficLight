using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private AsyncOperation op = null;
    private float pregram;
    private Slider slider;
    public Slider slider_word;
    private void Awake()
    {
        slider = transform.Find("LoadingSlide").GetComponent<Slider>();
    }
    void Start()
    {
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        if (string.IsNullOrEmpty(QuanJu.ScreenName))
            yield break;
        op = SceneManager.LoadSceneAsync(QuanJu.ScreenName);
        op.allowSceneActivation = false;
        while (true)
        {
            if (slider.value == pregram)
            {
                pregram = op.progress;
            }
              
            if (op.progress==0.9f)
            {
                pregram = 1;
            }
            slider.value = Mathf.Lerp(slider.value, pregram, Time.deltaTime * 1);
            slider_word.value = slider.value;
            if (slider.value > 0.99f)
            {
                slider.value = 1;
                slider_word.value = 1;
                break;
            }
            yield return Time.deltaTime;
        }
        op.allowSceneActivation = true;
    }
  
}
