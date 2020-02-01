using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayFiller : MonoBehaviour
{
    public GameObject basePrefab;

    [SerializeField]
    private List<BodyPartItem> m_trayItemsList = new List<BodyPartItem>();
    private List<BodyPartItem> m_hairList = new List<BodyPartItem>();
    private static int partCount = 10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateRandomBodyPart();
        }
    }

    private void Tray(BodyPartItem list)
    {

    }

    public void CreateBodyPart(BodyPartItem item)
    {
        partCount++;
        GameObject obj = Instantiate(basePrefab, Vector3.zero, Quaternion.identity);
        obj.name = string.Format("body-part-{0}-[{1}]", partCount, item.name);
    }

    [ContextMenu("Create Random Body Part")]
    public void CreateRandomBodyPart()
    {
        BodyPartItem item = m_trayItemsList[Random.Range(0, m_trayItemsList.Count)];

        CreateBodyPart(item);
    }
}
