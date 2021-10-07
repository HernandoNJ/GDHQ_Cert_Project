using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    public IEnumerator LoadImage(string path, Action<Texture2D> onComplete)
    {
        var www = UnityWebRequestTexture.GetTexture(path);
        var texture = new Texture2D(1, 1);
        yield return www.SendWebRequest();
        var myTexture = DownloadHandlerTexture.GetContent(www);
        // onComplete can be replaced by any function and receives myTexture
        onComplete(myTexture);
        
        /*
        *** Code without using Action ***
        var lis = FindObjectOfType<LocalImageSaver>(); var ifw = FindObjectOfType<ImageFromWeb>();
        lis.SaveImage(myTexture);
        ifw.ReplaceImage(myTexture);
        Both cases the functions will be called in this coroutine
        If so, I must call it only, for example, in start()
        */

    }
}
