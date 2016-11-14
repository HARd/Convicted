//#if UNITY_EDITOR
//using UnityEditor;
//#endif

[System.Serializable]
public class Item
{
	public string name;
	public int text_id = 0;
	public int hint_id;
	public bool contraband;
	public bool hidden;
	public int consealment;
	public float cost;
	public float damage;

//	#if UNITY_EDITOR
//	SerializedProperty serializedName;
//	SerializedProperty serializedText_id;
//	SerializedProperty serializedHint_id;
//	SerializedProperty serializedContraband;
//	SerializedProperty serializedCost;
//
//	public virtual void OnEnable(SerializedProperty sp)
//	{
//		serializedName = sp.FindPropertyRelative("name");
//		serializedText_id = sp.FindPropertyRelative("text_id");
//		serializedHint_id = sp.FindPropertyRelative("hint_id");
//		serializedContraband = sp.FindPropertyRelative("contraband");
//		serializedCost = sp.FindPropertyRelative("cost");
//	}
//
//	public virtual void DrawItem()
//	{
//		serializedName.stringValue = EditorGUILayout.TextField("Name", name);
//		serializedText_id.intValue = EditorGUILayout.IntField("Text_id", text_id);
//		serializedHint_id.intValue = EditorGUILayout.IntField("Hint_id", hint_id);
//		serializedContraband.boolValue = EditorGUILayout.Toggle("Contraband", contraband);
//		serializedCost.floatValue = EditorGUILayout.FloatField("Cost", cost);
//	}
//	#endif
}
