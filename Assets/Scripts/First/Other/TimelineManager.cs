using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineManager : MonoBehaviour {
    PlayableDirector BudCameraAnim;
    PlayableDirector BudBlackAnim;
    PlayableDirector IntroPlayerAnim;
    PlayableDirector IntroBlackAnim;
    public Bud budTrigger;
    bool hasPlay = false;
    Hair_PlayerMove playerMove;
	void Start () {
        BudCameraAnim = transform.Find("BudCameraAnim").GetComponent<PlayableDirector>();
        BudBlackAnim = transform.Find("BudBlackAnim").GetComponent<PlayableDirector>();
        IntroPlayerAnim = transform.Find("IntroPlayerAnim").GetComponent<PlayableDirector>();
        IntroBlackAnim = transform.Find("IntroBlackAnim").GetComponent<PlayableDirector>();
        playerMove = GameObject.FindWithTag("Player").GetComponent<Hair_PlayerMove>();
	}
	

	void Update () {

        if (BudCameraAnim.state == PlayState.Playing||IntroPlayerAnim.state==PlayState.Playing)
        {
            playerMove.animPaused = true;
            if (IntroPlayerAnim.state == PlayState.Playing && playerMove.animPaused)
            {
                playerMove.transform.position = new Vector3(playerMove.transform.position.x, 0.25f, playerMove.transform.position.z);
            }
        }
        else
        {
            playerMove.animPaused = false;
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
