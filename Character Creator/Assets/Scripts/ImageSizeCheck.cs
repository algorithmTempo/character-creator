using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSizeCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Image xptoExample = GetComponent<Image>();

        var realWidth = xptoExample.sprite.rect.width;
        var realHeight = xptoExample.sprite.rect.height;
        Debug.Log("Width: " + realWidth + " Height: " + realHeight);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
