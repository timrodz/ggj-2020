using System.Collections.Generic;
using UnityEngine;

public class BodyPartSpawner : MonoBehaviour
{
	public GameObject BasePrefab;
    
    [SerializeField]
	private List<BodyPartItem> m_bodyPartItemList = new List<BodyPartItem>();

	private static int partCount = 0;

	public void CreateBodyPart(BodyPartItem item) {
		partCount++;
        GameObject obj = Instantiate(BasePrefab, Vector3.zero, Quaternion.identity);
		// obj.transform.position = item.Position;
		// obj.transform.eulerAngles = item.Rotation;
		obj.name = string.Format("body-part-{0}_parent:[{1}]", partCount, item.name);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
			CreateRandomBodyPart();
		}
    }
    
    [ContextMenu("Create Random Body Part")]
    public void CreateRandomBodyPart() {
		BodyPartItem item = m_bodyPartItemList[Random.Range(0, m_bodyPartItemList.Count)];

		CreateBodyPart(item);
	}
}
