using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShikkuiStatus
{
    private int status = 0;
    /*
    0: 壁に漆喰なし、こて板に漆喰あり
    1: 壁に漆喰あり
    2: 壁に漆喰なし、こて板に漆喰なし
    */

    public int Status
    {
        get { return status; }
        set { status = value; }
    }
}
