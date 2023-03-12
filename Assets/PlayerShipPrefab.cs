using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipPrefab : MonoBehaviour
{



    GameObject body;
    Rigidbody bodyRB;
    GameObject[] thrusters = new GameObject[10];
    Rigidbody[] thrustRBs = new Rigidbody[10];
    FixedJoint[] thrustFJs = new FixedJoint[10];
    Vector3 thrusterScale = new Vector3(0.1f, 0.1f, 0.3f);
    Vector3[] thDirVec = new Vector3[10]
        {
        new Vector3(0f,1f,1f),
        new Vector3(1f,0f,1f),
        new Vector3(0f,-1f,1f),
        new Vector3(-1f,0f,1f),
        new Vector3(0f,1f,-1f),
        new Vector3(1f,0f,-1f),
        new Vector3(0f,-1f,-1f),
        new Vector3(-1f,0f,-1f),
        new Vector3(1f,0f,0f),
        new Vector3(-1f,0f,0f)
        };
    ThrustRotation[] thRot = new ThrustRotation[10]
        {
        new ThrustRotation(45f,0f,0f),
        new ThrustRotation(0f,-45f,0f),
        new ThrustRotation(-45f,0f,0f),
        new ThrustRotation(0f,45f,0f),
        new ThrustRotation(-45f,0f,0f),
        new ThrustRotation(0f,45f,0f),
        new ThrustRotation(45f,0f,0f),
        new ThrustRotation(0f,-45f,0f),
        new ThrustRotation(90f,0f,0f),
        new ThrustRotation(90f,0f,0f)
        };
    float fwd4 = 0.0f;
    float back4 = 0.0f;
    //float hTcCwize;
    //float hTcwize;
    Vector2 fwdTh = new Vector2(0, 0);
    Vector2 bkTh = new Vector2(0, 0);
    int thSpeed = 100;
    GameObject cam;
    int flame = 0;
    float hTcCwize = 0.0f;
    float hTcwize = 0.0f;


    struct ThrustRotation
    {
        public float X;
        public float Y;
        public float Z;

        public ThrustRotation(float x,float y,float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }




    public void OnCounterclockwize(InputAction.CallbackContext context)
    {
        //if (context.phase == InputActionPhase.Performed)
        //{
            //hTcCwize = 1;
        //}
        //else
        //{
            //hTcCwize = 0;
        //}
        hTcCwize = context.ReadValue<float>();
        Debug.Log("hTcCwize=" + hTcCwize.ToString() + "::" + flame.ToString());
        //if (hTcCwize != 0.0f)
        //{
            //thrustRBs[8].AddRelativeForce(Vector3.forward * (hTcCwize * thSpeed / 2.0f));
            //thrustRBs[9].AddRelativeForce(Vector3.back * (hTcCwize * thSpeed / 2.0f));
        //}
    }
    public void OnClockwize(InputAction.CallbackContext context)
    {
        //if (context.phase == InputActionPhase.Performed)
        //{
        //hTcwize = 1;
        //}
        this.hTcwize = context.ReadValue<float>();
        Debug.Log("hTcwize=" + hTcwize.ToString());
        /*if (hTcwize != 0)
        {
            thrustRBs[8].AddRelativeForce(Vector3.back * (hTcwize * thSpeed / 2.0f));
            thrustRBs[9].AddRelativeForce(Vector3.forward * (hTcwize * thSpeed / 2.0f));
        }*/
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void OnForward4(InputAction.CallbackContext context)
    {
        //if (context.phase==InputActionPhase.Performed)
        //{
        fwd4 = context.ReadValue<float>();
        //Debug.Log(fwd4.ToString());     //0.0~1.0
        //for (int i = 0; i < 4; i++)
        //{
        //    thrustRBs[i].AddRelativeForce(Vector3.forward * fwd4);
        //}
        //Debug.Log("Forward4");

        //}
    }

    public void OnBack4(InputAction.CallbackContext context)
    {
        back4 = context.ReadValue<float>();
        
        //Debug.Log("Back4");
    }

    public void OnForwardThruster(InputAction.CallbackContext context)
    {
        fwdTh = context.ReadValue<Vector2>();
        //Debug.Log("FWD=" + fwdTh.ToString());      //0.0~1.0
        //Debug.Log("ForThrust");
    }

    public void OnBackThruster(InputAction.CallbackContext context)
    {
        bkTh = context.ReadValue<Vector2>();
        //Debug.Log("BACK=" + bkTh.ToString());
        //Debug.Log("BackThrust");
    }





    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Start.");
        Physics.gravity = new Vector3(0, 0, 0);

        body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform.position = new Vector3(0, 0, 0);     //仮
        body.transform.localScale = new Vector3(1, 1, 1);
        bodyRB = body.AddComponent<Rigidbody>();

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.transform.parent = body.transform;


        //hTcCwize = 0.0f;
        //hTcwize = 0.0f;


        
        for (int i = 0; i < thrusters.Length; i++)
        {
            thrusters[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            thrustRBs[i] = thrusters[i].AddComponent<Rigidbody>();
            thrusters[i].name = "thruster" + i.ToString();
            thrusters[i].transform.localScale = thrusterScale;
            thrustFJs[i] = ThrusterLocate(thrusters[i], thDirVec[i], thRot[i]);
        }
        //テストコード
        //thrustRBs[8].AddRelativeForce(Vector3.forward * 100);
        //thrustRBs[4].AddRelativeForce(Vector3.back * 200);



    }
    
    private FixedJoint ThrusterLocate(GameObject go, Vector3 vec3, ThrustRotation tR)
    {
        vec3.Normalize();
        vec3 = vec3 * ((1 + thrusterScale.y) / 2);
        go.transform.position = vec3;
        go.transform.rotation = Quaternion.Euler(tR.X, tR.Y, tR.Z);
        FixedJoint rtnObj = go.AddComponent<FixedJoint>();
        rtnObj.connectedBody = bodyRB;
        return rtnObj;
    }
    // Update is called once per frame
    //void Update()
    //{
    //
    //}
    void FixedUpdate()
    {
        //if (hTcCwize != 0.0f)
        //{
            //Debug.Log("before_cCwize=" + hTcCwize.ToString());
            //thrustRBs[8].AddRelativeForce(Vector3.forward * (hTcCwize * thSpeed / 2.0f));
            //thrustRBs[9].AddRelativeForce(Vector3.back * (hTcCwize * thSpeed / 2.0f));
            //Debug.Log("after_cCwize=" + hTcCwize.ToString());
            //Debug.Log((hTcCwize * thSpeed / 2.0f).ToString());
            //Vector3 VecTest8 = new Vector3();
            //VecTest8 = Vector3.forward * (hTcCwize * thSpeed / 2.0f);
            //Debug.Log("thruster8=" + VecTest8.ToString());
        //}
        if (fwd4 != 0.0f)
        {
            for (int i = 0; i < 4; i++)
            {
                thrustRBs[i].AddRelativeForce(Vector3.forward * fwd4 * thSpeed);
            }
        }
        if (back4 != 0.0f)
        {
            for (int i=4;i<8;i++)
            {
                thrustRBs[i].AddRelativeForce(Vector3.back * back4 * thSpeed);
            }
        }


        if (fwdTh != Vector2.zero)
        {
            if (fwdTh.x < 0.0f)
            {
                thrustRBs[3].AddRelativeForce(Vector3.forward * -(fwdTh.x) * thSpeed);
            }
            else
            {
                thrustRBs[1].AddRelativeForce(Vector3.forward * fwdTh.x * thSpeed);
            }
            if (fwdTh.y < 0.0f)
            {
                thrustRBs[2].AddRelativeForce(Vector3.forward * -(fwdTh.y) * thSpeed);
            }
            else
            {
                thrustRBs[0].AddRelativeForce(Vector3.forward * fwdTh.y * thSpeed);
            }
        }

        if (bkTh != Vector2.zero)
        {
            if (bkTh.x < 0.0f)
            {
                thrustRBs[7].AddRelativeForce(Vector3.back * -(bkTh.x) * thSpeed);
            }
            else
            {
                thrustRBs[5].AddRelativeForce(Vector3.back * bkTh.x * thSpeed);
            }
            if (bkTh.y < 0.0f)
            {
                thrustRBs[6].AddRelativeForce(Vector3.back * -(bkTh.y) * thSpeed);
            }
            else
            {
                thrustRBs[4].AddRelativeForce(Vector3.back * bkTh.y * thSpeed);
            }
        }
        //Debug.Log("hTcCwize in FixedUpdate = " + hTcCwize.ToString() + "::" + flame.ToString());
        //if (hTcCwize != 0.0f)
        //{
            //Debug.Log("before_cCwize=" + hTcCwize.ToString());
            //thrustRBs[8].AddRelativeForce(Vector3.forward * (hTcCwize * thSpeed / 2.0f));
            //thrustRBs[9].AddRelativeForce(Vector3.back * (hTcCwize * thSpeed / 2.0f));
            //Debug.Log("after_cCwize="+ hTcCwize.ToString());
            //Debug.Log((hTcCwize * thSpeed / 2.0f).ToString());
            //Vector3 VecTest8 = new Vector3();
            //VecTest8 = Vector3.forward * (hTcCwize * thSpeed / 2.0f);
            //Debug.Log("thruster8=" + VecTest8.ToString());
        //}
        if (this.hTcwize != 0.0f)
        
        {
            Debug.Log("add force!");
            thrustRBs[8].AddRelativeForce(Vector3.back * (hTcwize * thSpeed / 2.0f));
            thrustRBs[9].AddRelativeForce(Vector3.forward * (hTcwize * thSpeed / 2.0f));
        }

        flame++;
    }
}
