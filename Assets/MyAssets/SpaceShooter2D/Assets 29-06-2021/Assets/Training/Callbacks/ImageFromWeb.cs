using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageFromWeb : MonoBehaviour
{
    [SerializeField] private string imagePath;

    private void OnEnable()
    {
        var imageLoader = FindObjectOfType<ImageLoader>();
        StartCoroutine(imageLoader.LoadImage(imagePath, ReplaceImage));
    }

    private void ReplaceImage(Texture2D texture)
    {
        Rect rect = new Rect(0f, 0f, texture.width, texture.height);
        GetComponent<Image>().sprite = Sprite.Create(texture,rect,Vector2.zero);
    }
    
    
}
