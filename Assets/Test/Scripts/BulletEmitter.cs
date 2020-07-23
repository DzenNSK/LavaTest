using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab; //Prefab of bullet
    public float bulletVelocity; //Standart bullet velocity
    public float maxDistance; //Distance for disabling bullet (if no hit)

    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullet.transform.position = transform.position;

        GameManager.Instance.OnFire.AddListener(Fire); //Listener for fire control
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - bullet.transform.position).magnitude > maxDistance) bullet.SetActive(false); //disable bullet if msxDistance reached
    }

    //Bullet launch
    public void Fire(Vector3 target)
    {
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        Vector3 vel = (target - bullet.transform.position).normalized * bulletVelocity;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (null != rb) rb.velocity = vel;
    }
}
