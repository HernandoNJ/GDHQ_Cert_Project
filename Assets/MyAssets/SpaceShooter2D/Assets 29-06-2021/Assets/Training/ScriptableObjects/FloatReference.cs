using System;

namespace Training.ScriptableObjects
{
[Serializable]
public class FloatReference 
{
    public bool useConstant = true;
    public float constantValue;
    public FloatVariable variable;

    public float Value 
        => useConstant? constantValue:variable.numValue;
}
}
