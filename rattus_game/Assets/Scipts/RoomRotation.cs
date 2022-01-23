using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRotation : MonoBehaviour
{

    public GameObject btnLeft, btnRight;
    public float rotationSpeed = 0.1f;
    public Transform[] target;
    public Transform currentTarget;
    Quaternion rotGoal;
    Vector3 direction;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = target[0];
        btnLeft = this.gameObject;
        btnRight = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime); //smooth transition

        direction = (currentTarget.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, rotationSpeed);
    }
    public void buttonRightClicked()
    {
        i = i - 1;
        if (i == -1) i = 3;

        Debug.Log("button Left pressed");
        currentTarget = target[i];
    }
    public void buttonLeftClicked()
    {
        
        i = i + 1;
        if (i == 4) i = 0;

        Debug.Log("button Right pressed");
        currentTarget = target[i];
    }

}
