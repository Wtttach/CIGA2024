using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raining : MonoBehaviour
{
    public RainPool RainPool;
    public float RainingInterval;
    public float FadeTime=0.5f;
    private float _timer;
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= RainingInterval)
        {
            _timer = 0;
            var rain = RainPool.Get();
            StartCoroutine(RainFadeOut(rain));
        }
    }
    
    private IEnumerator RainFadeOut(GameObject rain)
    {
        yield return new WaitForSeconds(FadeTime);
        RainPool.Release(rain);
    }
}
