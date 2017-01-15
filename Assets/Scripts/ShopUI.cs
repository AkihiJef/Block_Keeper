using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text text = GetComponent<Text>();

        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200, 300);
        rt.transform.position = new Vector2(10, 10);
        rt.transform.Rotate(new Vector3(0, 0, 45));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
