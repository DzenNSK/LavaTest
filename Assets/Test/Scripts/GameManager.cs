using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Common game manager (pseudosingleton)
public class GameManager : MonoBehaviour
{
    public BulletSettings bulletImpulse; //common bullet impulse
    public float dilationTime; //Time of slo-mo
    public float dilationCoefficient; //Slo-mo coefficient

    public class VectorEvent : UnityEvent<Vector3> { }
    public UnityEvent OnReset = new UnityEvent(); //Event for reset scene
    public VectorEvent OnFire = new VectorEvent(); //Event for fire controller, argument - target point for bullet

    private static GameManager m_instance; //Instance of singleton

    public static GameManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    //
    private void Awake()
    {
        //Singleton initialization
        if(null == m_instance)
        {
            m_instance = this;
            return;
        }
        Debug.Assert(m_instance == this, "More than one GameManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Calculate target point for bullet
    public void ResolveFire(Vector2 position)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);

        if(Physics.Raycast(ray, out hit))
        {
            OnFire.Invoke(hit.point);
        }
    }

    //Listener for Reset button
    public void ResetScene()
    {
        Time.timeScale = 1f;
        OnReset.Invoke();
    }

    //Slo-mo
    public void RunSlowMotion()
    {
        Time.timeScale = dilationCoefficient;
        StartCoroutine("StopSlowMotion");
    }

    IEnumerator StopSlowMotion()
    {
        float currentTime = 0f;
        while (currentTime < dilationTime)
        {
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
    }
}
