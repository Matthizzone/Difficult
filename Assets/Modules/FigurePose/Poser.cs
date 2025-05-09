using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Poser : MonoBehaviour
{
    public float rot_speed;

    Vector3 local_rot;

    Transform selected_ball;

    Vector3 mPrevPos;
    Vector3 mPosDelta;

    void Update()
    {
        TurnFigure();
        DragBone();
    }

    void TurnFigure()
    {
        Vector2 input_axis = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) input_axis.y += 1;
        if (Input.GetKey(KeyCode.A)) input_axis.x -= 1;
        if (Input.GetKey(KeyCode.S)) input_axis.y -= 1;
        if (Input.GetKey(KeyCode.D)) input_axis.x += 1;

        local_rot = new Vector3(
            local_rot.x + input_axis.y * rot_speed * Time.deltaTime,
            0,
            local_rot.z - input_axis.x * rot_speed * Time.deltaTime);

        local_rot.x = Mathf.Clamp(local_rot.x, -90, 90);

        transform.localEulerAngles = local_rot;
    }

    void DragBone()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                selected_ball = hit.transform.parent;
                mPrevPos = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButton(0) && selected_ball)
        {
            mPosDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mPrevPos;
            print(mPosDelta);
            mPosDelta.y = 0;

            Vector3 R = Camera.main.ScreenToWorldPoint(Input.mousePosition) - selected_ball.position;
            R.y = 0;
            R.Normalize();

            Vector3 T = Vector3.Cross(R, Vector3.up).normalized;
            Vector3 U = Vector3.up;

            float r_comp = Vector3.Dot(mPosDelta, R);
            float t_comp = Vector3.Dot(mPosDelta, T);

            print(r_comp + "   " + t_comp);

            Vector3 rot_vec = T * r_comp;// + U * t_comp;

            selected_ball.Rotate(rot_vec * 0.2f, Space.World);

            mPrevPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        else if (Input.GetMouseButton(1) && selected_ball)
        {
            // ?
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected_ball = null;
        }
    }
}
