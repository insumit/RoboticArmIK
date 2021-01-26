/// Arduino Tinkerkit Braccio robotic arm simulator with IK (Inverse Kinematics) for Unity
/// Shan-Yuan Teng <tanyuan@cmlab.csie.ntu.edu.tw>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SolveIK : MonoBehaviour {

	public bool useIK = true; // IK mode or manual adjustment
    public bool useFK = false;
	public bool autoEnd = true; // horizontal end in IK mode

    public GameObject targetSpawnPosition;//, theta1, theta2,theta3,theta4, theta5;
    Vector3 targetPosition;
    Vector3 currentPosition;
    bool mode;
    public Slider thetaBase, thetaShoulder, thetaElbow, t4,t5;
    //public Toggle IKmode, FKmode;

    public GameObject[] arms = new GameObject[5];

    public SkinnedMeshRenderer left, right;

    /* Arm dimensions( m ) */
    float BASE_HGT = 0.088f;
	float HUMERUS = 0.24f;
	float ULNA = 0.24f;
	float GRIPPER = 0.258f;

	/* pre-calculations */
	float hum_sq;
	float uln_sq;
    GameObject wristJoint, target;
	void Start () {
        /* pre-calculations */
        target = GameObject.FindGameObjectWithTag("target");
        target.SetActive(false);
        hum_sq = HUMERUS*HUMERUS;
		uln_sq = ULNA*ULNA;
        t4.value = 90;
        t5.value = 90;
    }

	void Update ()
    {
        if (useIK)
        {
            target.SetActive(true);
            //target.transform.position = wristJoint.transform.position;
            targetPosition = target.transform.position;
            SetArm(targetPosition.x, targetPosition.y, targetPosition.z, autoEnd);

            arms[0].transform.localRotation = Quaternion.Euler(new Vector3(0f, thetaBase.value, 0f));
            arms[1].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, thetaShoulder.value - 90f));
            arms[2].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, thetaElbow.value - 90f));
            arms[3].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, t4.value - 90f));
            arms[4].transform.localRotation = Quaternion.Euler(new Vector3(0f, t5.value, 0f));
        }
        else if(useFK)
        {
            arms[0].transform.localRotation = Quaternion.Euler(new Vector3(0f, thetaBase.value, 0f));
            arms[1].transform.localRotation = Quaternion.Euler(new Vector3(thetaShoulder.value - 90f, 0f, 0f));
            arms[2].transform.localRotation = Quaternion.Euler(new Vector3(thetaElbow.value - 90f, 0f, 0f));
            arms[3].transform.localRotation = Quaternion.Euler(new Vector3(0f, t4.value, 0f));
            //arms[4].transform.localRotation = Quaternion.Euler(new Vector3(0f, t5.value, 0f));
            left.SetBlendShapeWeight(0, t5.value);
            right.SetBlendShapeWeight(1, t5.value);
        }
	}
    public void ChooseIKmode()
    {
        useIK = true;
        useFK = false;
    }

    public void ChooseFKmode()
    {
        useFK = true;
        useIK = false;
    }


    void SetArm(float x, float y, float z, bool endHorizontal)
    {
        // Base angle
        float bas_angle_r = Mathf.Atan2(x, z);
        //float bas_angle_d = bas_angle_r * Mathf.Rad2Deg + 90f;
        thetaBase.value = bas_angle_r * Mathf.Rad2Deg + 90f;
        if (thetaBase.value < 0)
            thetaBase.value = 0;
        if (thetaBase.value > 270)
            thetaBase.value = 270;

        float wrt_y = y - BASE_HGT; // Wrist relative height to shoulder
        float s_w = x * x + z * z + wrt_y * wrt_y; // Shoulder to wrist distance square
        float s_w_sqrt = Mathf.Sqrt(s_w);

        // Elbow angle: knowing 3 edges of the triangle, get the angle
        float elb_angle_r = Mathf.Acos((hum_sq + uln_sq - s_w) / (2f * HUMERUS * ULNA));
        //float elb_angle_d = 270f - elb_angle_r * Mathf.Rad2Deg;
        thetaElbow.value = 270f - elb_angle_r * Mathf.Rad2Deg;
        if (thetaElbow.value < 15)
           thetaElbow.value = 15;
        if (thetaElbow.value > 165)
           thetaElbow.value = 165;
        // Shoulder angle = a1 + a2
        float a1 = Mathf.Atan2(wrt_y, Mathf.Sqrt(x * x + z * z));
        float a2 = Mathf.Acos((hum_sq + s_w - uln_sq) / (2f * HUMERUS * s_w_sqrt));
        float shl_angle_r = a1 + a2;
        //float shl_angle_d = 180f - shl_angle_r * Mathf.Rad2Deg;
        thetaShoulder.value = 180f - shl_angle_r * Mathf.Rad2Deg;
        if (thetaShoulder.value < 0)
            thetaShoulder.value = 0;
        if (thetaShoulder.value > 180)
            thetaShoulder.value = 180;
    }
}
