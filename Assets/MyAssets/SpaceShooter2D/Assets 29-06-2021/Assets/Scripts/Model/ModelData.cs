using UnityEngine;

namespace ScriptableObjects.Model
{
[CreateAssetMenu(fileName = "ModelData", menuName = "ScriptableObject/Model/ModelData")]
public class ModelData : ScriptableObject
{
    public string modelName;
    public string description;
    public Sprite model;
}
}
