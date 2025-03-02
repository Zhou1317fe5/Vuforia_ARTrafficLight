using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightCtro : MonoBehaviour
{
   
    public RedOrGreen[] _rGs;
    
    public void SetUpOrDown(int state)   // 0 red ; 1 green
    {
        _rGs[1].SetRedLight(state);
        _rGs[3].SetRedLight(state);
    }
    public void SetLeftOrRight(int state)
    {
        _rGs[0].SetRedLight(state);
        _rGs[2].SetRedLight(state);
    }
}
