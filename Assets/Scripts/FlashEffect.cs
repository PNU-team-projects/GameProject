using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    public Material flashMat;
    public float flashTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator Perform(Action whenDone)
    {
        spriteRenderer.material = flashMat;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material = defaultMat;

        whenDone?.Invoke();
    }
}
