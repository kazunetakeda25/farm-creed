using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public float timeDes { get; set; }
	// Use this for initialization
	void Start () {
        if (timeDes != 0)
        {
            Destroy(this.gameObject, timeDes);
        }
        else
        {
            Destroy(this.gameObject, 0.3f);
        }
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //void OnDisable()
    //{
    //    Destroy(this.gameObject);
    //}
}
