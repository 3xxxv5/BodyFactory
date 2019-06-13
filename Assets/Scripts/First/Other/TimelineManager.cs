using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineManager : MonoBehaviour {
    PlayableDirector BudCameraAnim;
    PlayableDirector BudBlackAnim;
    public Bud budTrigger;
    bool hasPlay = false;
    public static bool animPaused=false;
	void Start () {
        BudCameraAnim = transform.Find("BudCameraAnim").GetComponent<PlayableDirector>();
        BudBlackAnim = transform.Find("BudBlackAnim").GetComponent<PlayableDirector>();      
	}
	

	void Update () {

        if (BudCameraAnim.state == PlayState.Playing)
        {
           animPaused = true;
        }
        else
        {
           animPaused = false;
        }

        if (budTrigger != null)
        {
            if (budTrigger.hasGrowrn)
            {               
                if (!hasPlay)
                {
                    BudCameraAnim.Play();
                    BudBlackAnim.Play();
                    hasPlay = true;                    
                }       
            }
        }       
    }
}
