using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingController : MonoBehaviour
{
    public RedLightCtro _rlC;
    public float duration;//切换红绿灯间隔
    [HideInInspector]
    public float timer = 0;
    private bool vertical = true;

    // Update is called once per frame
    private void Awake()
    {

    }
    private void InitCrossingState()
    {
        duration = 10;
        vertical = true;
    }
    private void OnEnable()
    {
        InitCrossingState();
        timer = 0;
        _rlC.SetLeftOrRight(1);
        _rlC.SetUpOrDown(0);
    }
    public void SetHLDNumber(float _time)
    {
        timer = 0;
        duration = _time;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0;
            vertical = !vertical;
            if (vertical)
            {
                StartCoroutine(ChangVCrossing());
            }
            else
            {
                StartCoroutine(ChangeUCrossing());
            }
        }
    }
    public void SetHLD()
    {
        timer = 0;
        vertical = !vertical;
        if (vertical)
        {
            StartCoroutine(ChangVCrossing());
        }
        else
        {
            StartCoroutine(ChangeUCrossing());
        }
    }
    private IEnumerator ChangVCrossing()
    {

        _rlC.SetLeftOrRight(1);
        _rlC.SetUpOrDown(0);
        yield return new WaitForSeconds(1.5f);
    

    }
    private IEnumerator ChangeUCrossing()
    {

        _rlC.SetLeftOrRight(0);
        _rlC.SetUpOrDown(1);
        yield return new WaitForSeconds(1.5f);

    }
}
