using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI drawer for impulse label
public class ImpulseDrawer : MonoBehaviour
{
    [SerializeField]
    private Text textField;
    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (null != textField) textField.text = "Impulse: " + GameManager.Instance.bulletImpulse.ImpulseMagnitude.ToString();
    }
}
