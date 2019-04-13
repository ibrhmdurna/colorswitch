using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMechanics : MonoBehaviour
{
    [SerializeField] float RotateSpeed = 15f;
    /*[SerializeField]*/ bool TurnLeft;
    [SerializeField] GameObject Body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TurnRotate();
    }

    void TurnRotate()
    {
        if (TurnLeft)
            Body.transform.Rotate(0f, 0f, -RotateSpeed * Time.deltaTime);
        else
            Body.transform.Rotate(0f, 0f, RotateSpeed * Time.deltaTime);
    }
}
