using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float weaponRange = 100f;
    [SerializeField] private GameObject bloodSplatter;

    void Update()
    {
        ShootWeapon();
    }

    private void ShootWeapon()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        if (Physics.Raycast(ray, out var hit, weaponRange))
        {
            //calculate a better damage value?
            if (hit.collider.gameObject != null)
            {
                var splatter = Instantiate(bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                splatter.transform.parent = hit.transform;
                var health = hit.collider.gameObject.GetComponent<UniversalHealth>();
                if (health != null)
                {
                    health.Damage(10);
                }
            }
        }
    }
}
