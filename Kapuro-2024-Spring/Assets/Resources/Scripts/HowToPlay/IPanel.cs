using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPanel 
{
    abstract void show(); //パネルをアクティブに切り替え
    abstract void hide(); //パネルを非アクティブに切り替え
    abstract bool isActive(); //パネルがアクティブかどうか
}