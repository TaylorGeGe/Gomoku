using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[RequireComponent(typeof(People))]
//[HelpURL("http://www.baidu.com")]
//[AddComponentMenu("Learning/People")]

public class People : Editor
{
    [Header("BaseInfo")]
    [Multiline(5)]
    public string name;
    [Range(-2, 2)]
    public int age;

    [Space(100)]
    [Tooltip("用于设置性别")]
    public string sex;
    [ContextMenu("OutputInfo")]
    void OutputInfo()
    {
      //  print(name + " " + age);
    }
}
