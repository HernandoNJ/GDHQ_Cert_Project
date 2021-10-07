using UnityEngine;

namespace Interfaces
{
    public class Main : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(rayOrigin, out hitInfo))
                {
                    IDamage iDam = hitInfo.collider.GetComponent<IDamage>();
                    if (iDam != null)
                    {
                        iDam.Damage(100);
                    }
                }
            }
        }
    }
}
