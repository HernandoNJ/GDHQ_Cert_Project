using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Training.WebRequest {
public class DownloadImage : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private RawImage imageFromWeb;
    [SerializeField] private Renderer image3; // Plane

    private void Start()
    {
        image3.material.color = Color.cyan;
        image3.transform.localEulerAngles = new Vector3(90,0,180);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DownloadImageRoutine());
            image3.material.color = Color.red;
        }
    }

    IEnumerator DownloadImageRoutine()
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        // using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        // {
            //yield return request.SendWebRequest();
            yield return webRequest.SendWebRequest();

            // if(request.isHttpError || request.isNetworkError) Debug.LogError(request.error);
            if(webRequest.isHttpError || webRequest.isNetworkError) Debug.LogError(webRequest.error);
            else
            {
                var webTexture = DownloadHandlerTexture.GetContent(webRequest);
                imageFromWeb.texture = webTexture;
                image3.material.mainTexture = webTexture;
                // var textureFromWeb = DownloadHandlerTexture.GetContent(request);
                // imageFromWeb.texture = textureFromWeb;
                // image3.material.mainTexture = textureFromWeb;
                // Debug.Log("Image downloaded");
            }

            // http://gyanendushekhar.com/2017/07/08/load-image-runtime-unity/
        //}
    }
}
}