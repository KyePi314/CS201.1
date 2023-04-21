using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FollowMouse : MonoBehaviour
{
    
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}