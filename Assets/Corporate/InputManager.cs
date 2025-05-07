using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    /*
    public static InputManager instance;

    bool keyboard = false; // Maybe implement this better later????

    bool a_down;
    bool b_down;
    bool x_down;
    bool y_down;
    bool lb_down;
    bool rb_down;
    bool start_down;
    bool select_down;

    bool a_up;
    bool b_up;
    bool x_up;
    bool y_up;
    bool lb_up;
    bool rb_up;
    bool start_up;
    bool select_up;

    bool a_held;
    bool b_held;
    bool x_held;
    bool y_held;
    bool lb_held;
    bool rb_held;
    bool start_held;
    bool select_held;

    float enableTime;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        //Cursor.visible = false;

        prev_gamepad_count = Gamepad.all.Count;
    }

    int prev_gamepad_count;

    private void Update()
    {
        if (prev_gamepad_count > 0 && Gamepad.all.Count == 0)
        {
            print("No controller connected");
        }

        GetButtonInfo();
    }

    void GetButtonInfo()
    {
        if (FailChecks()) return;

        bool a = keyboard ? (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)) : Gamepad.all[0].aButton.IsPressed();
        if (a) // ----------  A  ----------
        {
            a_down = !a_held;
            a_up = false;

            a_held = true;
        }
        else
        {
            a_down = false;
            a_up = !a_held;

            a_held = false;
        }

        bool b = keyboard ? (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Backspace)) : Gamepad.all[0].bButton.IsPressed();
        if (b)  // ----------  B  ----------
        {
            b_down = !b_held;
            b_up = false;

            b_held = true;
        }
        else
        {
            b_down = false;
            b_up = !b_held;

            b_held = false;
        }

        bool x = keyboard ? Input.GetKey(KeyCode.Space) : Gamepad.all[0].xButton.IsPressed();
        if (x)  // ----------  X  ----------
        {
            x_down = !x_held;
            x_up = false;

            x_held = true;
        }
        else
        {
            x_down = false;
            x_up = !x_held;

            x_held = false;
        }

        bool y = keyboard ? (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) : Gamepad.all[0].yButton.IsPressed();
        if (y)  // ----------  Y  ----------
        {
            y_down = !y_held;
            y_up = false;

            y_held = true;
        }
        else
        {
            y_down = false;
            y_up = !y_held;

            y_held = false;
        }

        if (!keyboard && Gamepad.all[0].leftShoulder.IsPressed())  // ----------  LB  ----------
        {
            lb_down = !lb_held;
            lb_up = false;

            lb_held = true;
        }
        else
        {
            lb_down = false;
            lb_up = !lb_held;

            lb_held = false;
        }

        if (!keyboard && Gamepad.all[0].rightShoulder.IsPressed())  // ----------  RB  ----------
        {
            rb_down = !rb_held;
            rb_up = false;

            rb_held = true;
        }
        else
        {
            rb_down = false;
            rb_up = !rb_held;

            rb_held = false;
        }

        bool start = keyboard ? Input.GetKey(KeyCode.Escape) : Gamepad.all[0].startButton.IsPressed();
        if (start)  // ---------  START  ---------
        {
            start_down = !start_held;
            start_up = false;

            start_held = true;
        }
        else
        {
            start_down = false;
            start_up = !start_held;

            start_held = false;
        }

        bool select = keyboard ? (Input.GetKey(KeyCode.R)) : Gamepad.all[0].selectButton.IsPressed();
        if (select)  // --------  SELECT  --------
        {
            select_down = !select_held;
            select_up = false;

            select_held = true;
        }
        else
        {
            select_down = false;
            select_up = !select_held;

            select_held = false;
        }

        if (Time.time < 0.5f) // prevent button_ups on the first frame
        {
            a_up = false;
            b_up = false;
            x_up = false;
            y_up = false;
            lb_up = false;
            rb_up = false;
            start_up = false;
            select_up = false;
        }
    }

    bool FailChecks()
    {
        if (Gamepad.all.Count == 0) return true;
        if (Time.time < enableTime) return true;

        return false;
    }

    public Vector2 LeftStick()
    {
        if (FailChecks()) return Vector2.zero;

        Vector2 l_stick = Vector2.zero;

        if (keyboard)
        {
            if (Input.GetKey(KeyCode.W)) l_stick += new Vector2(0, 1);
            if (Input.GetKey(KeyCode.A)) l_stick += new Vector2(-1, 0);
            if (Input.GetKey(KeyCode.S)) l_stick += new Vector2(0, -1);
            if (Input.GetKey(KeyCode.D)) l_stick += new Vector2(1, 0);

            if (Input.GetKey(KeyCode.UpArrow)) l_stick += new Vector2(0, 1);
            if (Input.GetKey(KeyCode.LeftArrow)) l_stick += new Vector2(-1, 0);
            if (Input.GetKey(KeyCode.DownArrow)) l_stick += new Vector2(0, -1);
            if (Input.GetKey(KeyCode.RightArrow)) l_stick += new Vector2(1, 0);

            l_stick = l_stick.normalized;
        }
        else
        {
            l_stick = Gamepad.all[0].leftStick.ReadValue();
        }

        return l_stick;
    }

    public Vector3 LeftStickCamera()
    {
        Vector3 cam_right = Vector3.Cross(Vector3.up, Camera.main.transform.forward);
        Vector3 cam_forward = Vector3.Cross(cam_right, Vector3.up);

        return cam_right * LeftStick().x + cam_forward * LeftStick().y;
    }

    public Vector2 RightStick()
    {
        if (FailChecks()) return Vector2.zero;

        return Gamepad.all[0].rightStick.ReadValue();
    }

    public bool A_Down()
    {
        if (FailChecks()) return false;
        return a_down;
    }

    public bool A_Up()
    {
        if (FailChecks()) return false;
        return a_up;
    }

    public bool A_Held()
    {
        if (FailChecks()) return false;
        return a_held;
    }

    public bool B_Down()
    {
        if (FailChecks()) return false;
        return b_down;
    }

    public bool B_Up()
    {
        if (FailChecks()) return false;
        return b_up;
    }

    public bool B_Held()
    {
        if (FailChecks()) return false;
        return b_held;
    }

    public bool X_Down()
    {
        if (FailChecks()) return false;
        return x_down;
    }

    public bool X_Up()
    {
        if (FailChecks()) return false;
        return x_up;
    }

    public bool X_Held()
    {
        if (FailChecks()) return false;
        return x_held;
    }

    public bool Y_Down()
    {
        if (FailChecks()) return false;
        return y_down;
    }

    public bool Y_Up()
    {
        if (FailChecks()) return false;
        return y_up;
    }

    public bool Y_Held()
    {
        if (FailChecks()) return false;
        return y_held;
    }

    public bool LB_Down()
    {
        if (FailChecks()) return false;
        return lb_down;
    }

    public bool LB_Up()
    {
        if (FailChecks()) return false;
        return lb_up;
    }

    public bool LB_Held()
    {
        if (FailChecks()) return false;
        return lb_held;
    }

    public bool RB_Down()
    {
        if (FailChecks()) return false;
        return rb_down;
    }

    public bool RB_Up()
    {
        if (FailChecks()) return false;
        return rb_up;
    }

    public bool RB_Held()
    {
        if (FailChecks()) return false;
        return rb_held;
    }

    public float LT()
    {
        if (FailChecks()) return 0;

        float lt;

        if (keyboard)
        {
            lt = Input.GetKey(KeyCode.S) ? 1 : 0;
        }
        else
        {
            lt = Gamepad.all[0].leftTrigger.ReadValue();
        }

        return lt;
    }

    public float RT()
    {
        if (FailChecks()) return 0;


        float rt;

        if (keyboard)
        {
            rt = Input.GetKey(KeyCode.W) ? 1 : 0;
        }
        else
        {
            rt = Gamepad.all[0].rightTrigger.ReadValue();
        }

        return rt;
    }

    public bool Start_Down()
    {
        if (FailChecks()) return false;
        return start_down;
    }

    public bool Start_Up()
    {
        if (FailChecks()) return false;
        return start_up;
    }

    public bool Start_Held()
    {
        if (FailChecks()) return false;
        return start_held;
    }

    public bool Select_Down()
    {
        if (FailChecks()) return false;
        return select_down;
    }

    public bool Select_Up()
    {
        if (FailChecks()) return false;
        return select_up;
    }

    public bool Select_Held()
    {
        if (FailChecks()) return false;
        return select_held;
    }

    public void DisabledUntil(float newEnableTime)
    {
        enableTime = newEnableTime;
    }
    */
}
