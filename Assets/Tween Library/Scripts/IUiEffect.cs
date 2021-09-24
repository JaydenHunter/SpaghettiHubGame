///Tomas Munro's Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts
{ 
public interface IUiEffect
{
    // Start is called before the first frame update
    IEnumerator Execute();
}
}
