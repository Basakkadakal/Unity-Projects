using UnityEngine;
using UnityEngine.InputSystem;
public class RaycastController : MonoBehaviour
{
    [SerializeField] private float range = 100f;  //mermi menzili
    [SerializeField] private float impactForce = 300f; //rigibody uygulanan kuvvet

    [SerializeField] private int damage = 25;     //silahýn hasarý

    [SerializeField] private Transform firePoint;//ýþýn hangi tarafa gitcek

    [SerializeField] private LayerMask hitLayers;


    private void Awake()
    {
        if(firePoint == null) //ateþ ettiðinde sen yön vermediysrn
        {
            firePoint = transform;   //karakterin baktýðý yöne ateþ
        }
    }

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;    //boþ bilgi kutusu

        Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 0.9f);

        if(Physics.Raycast(firePoint.position,firePoint.forward, out hit, range, hitLayers))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            
            if(rb != null)
            {
                Vector3 forceDirection = firePoint.forward; //rb varsa merminin gidiþ yönünü tanýmla

                rb.AddForceAtPosition(forceDirection * impactForce, hit.point, ForceMode.Impulse);  //gelen yöne doðru bir kuvvet uygula
            }

            //vurulan objede can var mý
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }


}   
