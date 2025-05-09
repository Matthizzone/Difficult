using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehavior : MonoBehaviour
{
    public bool selected;

    public Vector3 targetPos;

    const float SPIN_STRENGTH = 0.3f;
    const float DISSIPATION = 1f;

    Vector3 mPrevPos;
    Vector3 mPosDelta;

    void Update()
    {
        MouseOverSpin();
    }

    void MouseOverSpin()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool mousing_over = Physics.Raycast(ray, out RaycastHit hit, 100);
        mousing_over &= hit.transform == transform;

        if (mousing_over)
        {
            mPosDelta = Input.mousePosition - mPrevPos;

            if (Input.GetMouseButtonDown(0)) Select();
        }
        else
        {
            Vector3 default_spin = selected ? new Vector3(4, 0, 0) : Vector3.zero;
            mPosDelta = MattMath.FRIndepLerp(mPosDelta, default_spin, DISSIPATION);
        }

        Vector3 rot_vec = new Vector3(mPosDelta.y, 0, -mPosDelta.x);

        transform.Rotate(rot_vec * SPIN_STRENGTH, Space.World);

        mPrevPos = Input.mousePosition;
    }

    void Select()
    {
        selected = !selected;
    }
}
