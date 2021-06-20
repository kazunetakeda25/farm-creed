using UnityEngine;

public class ItemLoading : MonoBehaviour {
    private float timer;
    private Sprite sprite;
    private Vector3 target;
	// Use this for initialization
	void Start () {
        target = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        timer = 0.1f;
        sprite = GetComponent<SpriteRenderer>().sprite;

        //GameObject bong = new GameObject();
        //bong.transform.position = Vector3.zero;
        //bong.AddComponent<SpriteRenderer>().sprite = sprite;
        //bong.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        //bong.AddComponent<TestMatrix>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(target, new Vector3(0, 0, -1), 5);
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0.1f;
            GameObject bong = new GameObject();
            bong.transform.position = transform.position;
            bong.AddComponent<SpriteRenderer>().sprite = sprite;
            bong.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder-1;
            bong.AddComponent<Bong>();
            bong.gameObject.transform.localScale = transform.localScale;
            //print(bong.transform.worldToLocalMatrix);
        }
	}
}

public class Bong : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        Destroy(gameObject, 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update ()
    {
        spriteRenderer.color -= new Color(0, 0, 0, Time.deltaTime);
    }
}

public class TestMatrix : MonoBehaviour
{
    public GameObject x;
    void Start()
    {
    }
}
