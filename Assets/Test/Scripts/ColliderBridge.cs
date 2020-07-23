using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component for binding collider events to character ragdoll manager
public class ColliderBridge : MonoBehaviour
{
    RagdollManager _rm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterManager(RagdollManager rm)
    {
        _rm = rm;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rm.ProcessHit(collision, gameObject);
    }
}
