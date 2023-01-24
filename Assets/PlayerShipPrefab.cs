using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipPrefab : MonoBehaviour
{



    GameObject body;
    Rigidbody bodyRB;
    GameObject[] thrusters = new GameObject[8];
    Rigidbody[] thrusterRBs = new Rigidbody[8];
    FixedJoint[] thrustFJs = new FixedJoint[8];
    Vector3 thrusterScale = new Vector3(0.1f, 0.1f, 0.3f);



    // Start is called before the first frame update
    void Start()
    {
       

        Physics.gravity = new Vector3(0, 0, 0);

        body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform.position = new Vector3(0, 0, 0);     //仮
        body.transform.localScale = new Vector3(1, 1, 1);
        bodyRB = body.AddComponent<Rigidbody>();

        //GameObject[] thrusters = new GameObject[8];
        //Rigidbody[] thrusterRBs = new Rigidbody[8];
        //FixedJoint[] thrustFJs = new FixedJoint[8];
        //Vector3 thrusterScale = new Vector3(0.1f, 0.1f, 0.3f);


        //本番コード
        //for (int i = 0; i < thrusters.Length; i++)
        //{
        //    thrusters[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    thrusterRBs[i] = thrusters[i].AddComponent<Rigidbody>();
        //    thrusters[i].name = "thruster" + i.ToString();
        //    thrusters[i].transform.localScale = thrusterScale;
        //}

        //テストコード↓
        
        
        thrusters[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        thrusterRBs[0] = thrusters[0].AddComponent<Rigidbody>();
        thrusters[0].name = "thruster0";
        thrusters[0].transform.localScale = thrusterScale;
        //ここまで


        //推進装置の位置、回転を書く
        //Vector3 vec3 = new Vector3(0, 1, 1);
        //vec3.Normalize();
        //vec3 = vec3 * ((1 + thrusterScale.y) / 2);
        //thrusters[0].transform.position = vec3;
        //thrusters[0].transform.rotation = Quaternion.Euler(45,0, 0);
        //thrustFJs[0] = thrusters[0].AddComponent<FixedJoint>();
        //thrustFJs[0].connectedBody = bodyRigidBody;

        //Debug.Log(vec3.ToString());

        thrustFJs[0] = ThrusterLocate(thrusters[0], new Vector3(0, 1, 1), 45, 0, 0);


    }
    private FixedJoint ThrusterLocate(GameObject go, Vector3 vec3, float a, float b, float c)
    {
        vec3.Normalize();
        vec3 = vec3 * ((1 + thrusterScale.y) / 2);
        go.transform.position = vec3;
        go.transform.rotation = Quaternion.Euler(a, b, c);
        FixedJoint rtnObj = go.AddComponent<FixedJoint>();
        rtnObj.connectedBody = bodyRB;
        return rtnObj;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
