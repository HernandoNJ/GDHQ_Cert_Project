using UnityEngine;

namespace Training.Abstract_classes{
public abstract class Employee : MonoBehaviour
{
    public static string companyName = "GameDev Company";

    // this field is shared in all children classes
    public string employeeName;

    public abstract void CalculateSalary();
}
}