using Assets.Scripts.Constants;
using StateMachine.Action.Math;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;
using System.Collections.Generic;
using PathologicalGames;

public class CameraMovement : MonoBehaviour {
    public float speed = 10.0f;
    public Animator a;
    public Transform hero;
    public Vector2 direction;
    public Transform guiTextPrefab;
	// Use this for initialization
	void Start ()
	{
        direction = Vector2.right;
        
        
        // Weighted Multi Cue
        List<KeyValuePair<string, int>> myMultiCueWeights = new List<KeyValuePair<string, int>>();
        myMultiCueWeights.Add(new KeyValuePair<string, int>("Laser", 30));
        myMultiCueWeights.Add(new KeyValuePair<string, int>("Shot", 10));
        myMultiCueWeights.Add(new KeyValuePair<string, int>("strike", 10));
        AudioManager.Instance.createMultiCueRandom("MyMultiCue", myMultiCueWeights);

        // Parallel Multi Cue
        List<string> parallelCueList = new List<string>();
        parallelCueList.Add("strike");
        parallelCueList.Add("Laser");
        parallelCueList.Add("Dash");
        AudioManager.Instance.createMultiCueParallel("MyParallel", parallelCueList);

        // Sequential Multi Cue
        Dictionary<int, string> seqList = new Dictionary<int, string>();
        seqList[0] = "HackandSlash";
        seqList[1] = "HackandSlash";
        seqList[2] = "HackandSlash";
        AudioManager.Instance.createMultiCueSequential("MySequential", seqList);
        

       // AudioManager.Instance.playLoop("Hack and Slash");
	}

    void SetAttacksFalse()
    {
        a.SetBool("AttackRight", false);
        a.SetBool("AttackLeft", false);
        a.SetBool("AttackUp", false);
        a.SetBool("AttackDown", false);
        a.SetBool("ShootRight", false);
        a.SetBool("ShootLeft", false);
        a.SetBool("ShootUp", false);
        a.SetBool("ShootDown", false);
    }

	// Update is called once per frame
	void Update()
	{
        handleMusicTest();

	    if (a.GetBool("AttackUp") || a.GetBool("AttackDown") ||
	        a.GetBool("AttackLeft") || a.GetBool("AttackRight") ||
            a.GetBool("ShootUp") || a.GetBool("ShootDown") ||
            a.GetBool("ShootLeft") || a.GetBool("ShootRight"))
	    {
	        return;
	    }

	    if (Input.GetButtonDown("Attack1") && 
            !a.GetBool("AttackUp") && !a.GetBool("AttackDown") && !a.GetBool("AttackLeft") && !a.GetBool("AttackRight") && 
            !a.GetBool("ShootUp") && !a.GetBool("ShootDown") && !a.GetBool("ShootLeft") && !a.GetBool("ShootRight"))
	    {
            AudioManager.Instance.playCue("strike",gameObject);

            if (direction == Vector2.right)
	        {
                a.SetBool("AttackRight", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
	        }
	        if (direction == -Vector2.right)
	        {
                a.SetBool("AttackLeft", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
	        }
	        if (direction == Vector2.up)
	        {
                a.SetBool("AttackUp", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
	        }
	        if (direction == -Vector2.up)
	        {
                a.SetBool("AttackDown", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
	        }
            Vector3 dmgSpawnPoint = new Vector3(hero.transform.position.x +direction.x, hero.transform.position.y + direction.y, 0);
            Transform myInstance = PoolManager.Pools["DamageTextPool"].Spawn(guiTextPrefab);
            myInstance.Translate(dmgSpawnPoint);
            return;
	    }
        else if (Input.GetButtonDown("Attack2") &&
            !a.GetBool("AttackUp") && !a.GetBool("AttackDown") && !a.GetBool("AttackLeft") && !a.GetBool("AttackRight") && 
            !a.GetBool("ShootUp") && !a.GetBool("ShootDown") && !a.GetBool("ShootLeft") && !a.GetBool("ShootRight"))
        {
            if (direction == Vector2.right)
            {
                a.SetBool("ShootRight", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
            }
            if (direction == -Vector2.right)
            {
                a.SetBool("ShootLeft", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
            }
            if (direction == Vector2.up)
            {
                a.SetBool("ShootUp", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
            }
            if (direction == -Vector2.up)
            {
                a.SetBool("ShootDown", true);
                a.SetBool("Idle", false);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
            }
            return;
        }
        else
	    {
            float vert = Mathf.Abs(Input.GetAxis("VerticalAxis")) > Mathf.Abs(Input.GetAxis("VerticalAxisJoystick")) ? Input.GetAxis("VerticalAxis") : Input.GetAxis("VerticalAxisJoystick");
            float horz = Mathf.Abs(Input.GetAxis("HorizontalAxis")) > Mathf.Abs(Input.GetAxis("HorizontalAxisJoystick")) ? Input.GetAxis("HorizontalAxis") : Input.GetAxis("HorizontalAxisJoystick");

            if (Mathf.Abs(vert) < 0.2f && Mathf.Abs(horz) < 0.2f)
	        {
                a.SetBool("Idle", true);
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
	            return;
	        }

            vert *= Time.deltaTime * speed;
            horz *= Time.deltaTime * speed;
	        hero.Translate(horz, vert, 0);

	        if (Mathf.Abs(horz) >= Mathf.Abs(vert))
	        {
	            if (horz > 0.01)
	            {
	                direction = Vector2.right;

	                a.SetBool("Right", true);
	                a.SetBool("Left", false);
	                a.SetBool("Up", false);
	                a.SetBool("Down", false);
                    a.SetBool("Idle", false);
	            }
                else if (horz < -0.01)
	            {
	                direction = -Vector2.right;

	                a.SetBool("Left", true);
	                a.SetBool("Up", false);
	                a.SetBool("Down", false);
	                a.SetBool("Right", false);
                    a.SetBool("Idle", false);

	            }
	        }
            else if (Mathf.Abs(vert) > Mathf.Abs(horz))
	        {
	            if (vert > 0.01)
	            {
	                direction = Vector2.up;

	                a.SetBool("Right", false);
	                a.SetBool("Left", false);
	                a.SetBool("Up", true);
	                a.SetBool("Down", false);
                    a.SetBool("Idle", false);
	            }
                else if (vert < -0.01)
	            {
	                direction = -Vector2.up;

	                a.SetBool("Left", false);
	                a.SetBool("Up", false);
	                a.SetBool("Down", true);
	                a.SetBool("Right", false);
                    a.SetBool("Idle", false);
	            }
	        }
	        else
	        {
                a.SetBool("Idle", true);
	            a.SetBool("Right", false);
	            a.SetBool("Left", false);
	            a.SetBool("Up", false);
	            a.SetBool("Down", false);
	        }
	    }
    }

    void handleMusicTest(){
        
        // sound system checking
        // WORKING
        if (Input.GetKeyDown(KeyCode.Keypad0))
            AudioManager.Instance.playCueDelayed("HackandSlash", 2.0f);
        
        // WORKING
        if (Input.GetKeyDown(KeyCode.Keypad1))
            AudioManager.Instance.playLoop("HackandSlash");

        // WORKING
        if (Input.GetKeyDown(KeyCode.Keypad2))
            AudioManager.Instance.pauseLoop("HackandSlash");

        // WORKING
        if (Input.GetKeyDown(KeyCode.Keypad3))
            AudioManager.Instance.stopLoop("HackandSlash");

        // WORKING
        if (Input.GetKeyDown(KeyCode.Keypad4))
            AudioManager.Instance.playMultiCue("MyMultiCue");
        
        // WORKING 
        if (Input.GetKeyDown(KeyCode.Keypad5))
            AudioManager.Instance.playMultiCue("MyParallel");
        
        /* WORKING */
        if (Input.GetKeyDown(KeyCode.Keypad6))
            AudioManager.Instance.playMultiCue("MySequential");

        /* WORKING */
        if (Input.GetKeyDown(KeyCode.Keypad7))
            AudioManager.Instance.playLoop("strike");

        // WORKING
        if (Input.GetKeyDown(KeyCode.Keypad8))
            AudioManager.Instance.pauseLoop("strike");

        // WORKING
        if (Input.GetKeyDown(KeyCode.Keypad9))
            AudioManager.Instance.stopLoop("strike");

    }
}
