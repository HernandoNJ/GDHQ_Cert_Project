using ScriptableObjects.Model;
using UnityEngine;

namespace Model
{
public class Model : MonoBehaviour
{
    public ModelData modelData;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = modelData.model;
    }
}
}
