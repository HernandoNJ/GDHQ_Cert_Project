using UnityEngine;

namespace Training.Others
{
    public class DamageOnClick : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, hitInfo: out hitInfo))
                {
                    var iDamage = hitInfo.collider.GetComponent<ITakeDamageT>();
                    if (iDamage != null) iDamage.TakeDamage(1);
                }
            }
        }
    }
}
