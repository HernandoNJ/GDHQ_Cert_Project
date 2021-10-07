using System;
using UnityEngine;
using static System.IO.File;

public class LocalImageSaver : MonoBehaviour
{
    [SerializeField] private string path;

    private void Awake()
    {
        ImageLoader loader = FindObjectOfType<ImageLoader>();
        StartCoroutine(loader.LoadImage(path, SaveImage));
    }

    private void SaveImage(Texture2D texture)
    {
        WriteAllBytes("C:\\Users\\Hernando\\Documents\\MyDocs\\myPicture.png", texture.EncodeToPNG());
    }

}
