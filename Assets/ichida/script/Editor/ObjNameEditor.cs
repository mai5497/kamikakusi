//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(HintObj))]
//public class ObjNameEditor : Editor {

//    HintObj _hintObj;

//    private void OnEnable() {
//    }

//    public override void OnInspectorGUI() {
//        //----- スクリプトを表示 -----
//        DrawDefaultInspector();

//        _hintObj = target as HintObj;
//        EditorGUILayout.LabelField("--------------- 人の目 ---------------");
//        _hintObj.objName = EditorGUILayout.TextField("オブジェクト名", _hintObj.objName);
//        _hintObj._isHatenaHuman = EditorGUILayout.Toggle("？？？の変換", _hintObj._isHatenaHuman);

//        EditorGUILayout.LabelField("--------------- 狐の目 ---------------");
//        _hintObj.objName = EditorGUILayout.TextField("オブジェクト名", _hintObj.uraObjName);
//        _hintObj._isHatenaFox = EditorGUILayout.Toggle("？？？の変換", _hintObj._isHatenaFox);

//    }
//}
