using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipPrefab : MonoBehaviour
{



    GameObject body;
    Rigidbody bodyRB;
    GameObject[] thrusters = new GameObject[8];
    Rigidbody[] thrustRBs = new Rigidbody[8];
    FixedJoint[] thrustFJs = new FixedJoint[8];
    Vector3 thrusterScale = new Vector3(0.1f, 0.1f, 0.3f);
    Vector3[] thDirVec = new Vector3[8]
        {
        new Vector3(0f,1f,1f),
        new Vector3(1f,0f,1f),
        new Vector3(0f,-1f,1f),
        new Vector3(-1f,0f,1f),
        new Vector3(0f,1f,-1f),
        new Vector3(1f,0f,-1f),
        new Vector3(0f,-1f,-1f),
        new Vector3(-1f,0f,-1f)
        };
    ThrustRotation[] thRot = new ThrustRotation[8]
        {
        new ThrustRotation(45f,0f,0f),
        new ThrustRotation(0f,-45f,0f),
        new ThrustRotation(-45f,0f,0f),
        new ThrustRotation(0f,45f,0f),
        new ThrustRotation(-45f,0f,0f),
        new ThrustRotation(0f,45f,0f),
        new ThrustRotation(45f,0f,0f),
        new ThrustRotation(0f,-45f,0f)
        };

    



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






    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void OnForward4(InputAction.CallbackContext context)
    {
        Debug.Log("Forward4");
    }

    public void OnBack4(InputAction.CallbackContext context)
    {
        Debug.Log("Back4");
    }

    public void OnForwardThruster(InputAction.CallbackContext context)
    {
        Debug.Log("ForThrust");
    }

    public void OnBackThruster(InputAction.CallbackContext context)
    {
        Debug.Log("BackThrust");
    }





    // Start is called before the first frame update
    void Start()
    {
       

        Physics.gravity = new Vector3(0, 0, 0);

        body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform.position = new Vector3(0, 0, 0);     //仮
        body.transform.localScale = new Vector3(1, 1, 1);
        bodyRB = body.AddComponent<Rigidbody>();




        //本番コード
        for (int i = 0; i < thrusters.Length; i++)
        {
            thrusters[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            thrustRBs[i] = thrusters[i].AddComponent<Rigidbody>();
            thrusters[i].name = "thruster" + i.ToString();
            thrusters[i].transform.localScale = thrusterScale;
            thrustFJs[i] = ThrusterLocate(thrusters[i], thDirVec[i], thRot[i]);
        }
        //テストコード
        //thrustRBs[0].AddRelativeForce(Vector3.forward * 200);
        thrustRBs[4].AddRelativeForce(Vector3.back * 200);



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

    }
}
