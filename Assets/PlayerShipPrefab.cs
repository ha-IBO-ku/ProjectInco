using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        (0f,1f,1f),
        (1f,0f,1f),
        (0f,-1f,1f),
        (-1f,0f,1f),
        (0f,1f,-1f),
        (1f,0f,-1f),
        (0f,-1f,-1f),
        (-1f,0f,-1f)
        };
    ThrustRotation[] thRot = new ThrustRotation[8]
        {
        (45f,0f,0f),
        (0f,45f,0f),
        (-45f,0f,0f),
        (0f,-45f,0f),
        (-45f,0f,0f),
        (0f,-45f,0f),
        (45f,0f,0f),
        (0f,45f,0f)
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

        //テストコード↓
        
        
        //thrusters[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //thrustRBs[0] = thrusters[0].AddComponent<Rigidbody>();
        //thrusters[0].name = "thruster0";
        //thrusters[0].transform.localScale = thrusterScale;
        //ここまで


        //推進装置の位置、回転を書く
        //Vector3 vec3 = new Vector3(0, 1, 1);
        //vec3.Normalize();
        //vec3 = vec3 * ((1 + thrusterScale.y) / 2);
        //thrusters[0].transform.position = vec3;
        //thrusters[0].transform.rotation = Quaternion.Euler(45,0, 0);
        //thrustFJs[0] = thrusters[0].AddComponent<FixedJoint>();
        //thrustFJs[0].connectedBody = bodyRB;

        //Debug.Log(vec3.ToString());

        //thrustFJs[0] = ThrusterLocate(thrusters[0], new Vector3(0, 1, 1), 45, 0, 0);
        //thrustFJs[1] = ThrusterLocate(thrusters[1], new Vector3(1, 0, 1), 0, 45, 0);
        //for (int i = 0; i < thrustFJs.Length; i++)
        //{
        //    thrustFJs[i] = ThrusterLocate(thrusters[i], thDirVec[i], thRot[i]);
        //}

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
    void Update()
    {

    }
}
