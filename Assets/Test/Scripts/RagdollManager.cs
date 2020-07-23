using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main ragdoll manager
public class RagdollManager : MonoBehaviour
{
    [SerializeField]
    private List<Collider> colliders = new List<Collider>(); //List of all bodypart colliders
    private Animator animator; //Character animator

    //Original position and rotation for reset
    private Vector3 spawn;
    private Quaternion spawnRot;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        spawnRot = transform.rotation;

        animator = GetComponent<Animator>();

        //collect all colliders to list
        CollectColliders<CapsuleCollider>();
        CollectColliders<BoxCollider>();
        CollectColliders<SphereCollider>();

        GameManager.Instance.OnReset.AddListener(Restore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Generic collider collector. Collect all colliders of T subtype to list and create ColliderBridge's
    private void CollectColliders<T>() where T:Collider
    {
        T[] res;
        Collider collider;
        ColliderBridge cb;

        res = GetComponentsInChildren<T>();
        for(int i = 0; i < res.Length; i++)
        {
            collider = res[i];
            collider.attachedRigidbody.isKinematic = true;
            cb = collider.gameObject.AddComponent<ColliderBridge>();
            cb.RegisterManager(this);
            colliders.Add(collider);
        }
    }

    //Callback for ColliderBridge, process collisions from any bodypart
    public void ProcessHit(Collision collision, GameObject bodypart)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            if(bodypart.transform.IsChildOf(gameObject.transform))ResolveHit(bodypart, collision.GetContact(0).point, -collision.impulse);
        }
    }

    //Processor for collisions
    public void ResolveHit(GameObject bodypart, Vector3 position, Vector3 direction)
    {
        SetKinematic(false);
        animator.enabled = false;
        Rigidbody rb = bodypart.GetComponent<Rigidbody>();
        rb.AddForceAtPosition(direction.normalized * GameManager.Instance.bulletImpulse.ImpulseMagnitude, position, ForceMode.Impulse);
        GameManager.Instance.RunSlowMotion();
    }

    //Set all colliders kinematic mode on/off (for animation/ragdoll mode)
    private void SetKinematic(bool state)
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].attachedRigidbody.isKinematic = state;
        }
    }
  
    //Reset character to starting mode
    public void Restore()
    {
        transform.position = spawn;
        transform.rotation = spawnRot;
        animator.enabled = true;
        SetKinematic(true);
    }

}
