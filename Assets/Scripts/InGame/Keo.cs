using UnityEngine;

public class Keo : MonoBehaviour {
    public int Row { get; set; }
    public int Column { get; set; }
    public string type { get; set; }
    public bool addtoList { get; set; }
    public bool big { get; set; }
    public bool isCombo { get; set; }
    public bool istarget { get; set; }
    public bool isCheck { get; set; }
    public bool scale { get; set; }

    public bool reset { get; set; }

    public GameData.KEOCOLLECTION NameCollection { get; set; }
    void Update()
    {
       
        if (big)
        {
            transform.localScale +=transform.localScale*2f*Time.deltaTime;
            return;
        }
        if (isCombo)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameData.TargetCombo.transform.position, Time.deltaTime * GameControll.speedCandyFall);
            return;
        }
        if (reset)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameData.vecReset, Time.deltaTime * GameControll.speedCandyFall);
            return;
        }

        if (scale)
        {
            scale = false;
            GetComponentInChildren<Animator>().Play("Scale");
        }
       
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(GameControll.startPointX + GetComponent<Collider2D>().bounds.size.x * Column + Column*GameControll.Spacing, GameControll.startPointY + GetComponent<Collider2D>().bounds.size.y * Row + Row*GameControll.Spacing, 0), GameControll.speedCandyFall * Time.deltaTime);
       
        
    }
    void OnDestroy()
    {
        if (big)
        {
            if (isCombo)
            {
                SoundController.Intance.Play(SoundController.CLIP.COMBO);
            }
            else
            {
                SoundController.Intance.Play(SoundController.CLIP.EAT);
            }            
            GameObject ef = (GameObject)Instantiate(GameData.loadEF(type), this.transform.position, Quaternion.identity);
            ef.transform.localScale = this.transform.localScale*1;
            ef.AddComponent<AutoDestroy>(); 
        }        
    }
    
}
