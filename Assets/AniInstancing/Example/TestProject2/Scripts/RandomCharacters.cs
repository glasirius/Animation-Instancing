 /// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;
  
//[RequireComponent(typeof(Animator))]  

//Name of class must be name of file as well

public class RandomCharacters : MonoBehaviour {
	
	public float AvatarRange = 25;

	protected Animator avatar;
	private AnimationInstancing.AnimationInstancing instancing;
	private float SpeedDampTime = .25f;	
	private float DirectionDampTime = .25f;	
	private Vector3 TargetPosition = new Vector3(0,0,0);
	
	// Use this for initialization
	void Start () 
	{
        if (!AnimationInstancing.AnimationInstancingMgr.Instance.UseInstancing)
        {
            avatar = GetComponent<Animator>();
        }
        else
        {
            instancing = GetComponent<AnimationInstancing.AnimationInstancing>();
            Debug.Assert(instancing);
            if (instancing == null)
            {
                gameObject.SetActive(false);
            }
        }
        if (AnimationInstancing.AnimationInstancingMgr.Instance.UseInstancing)
        {
            transform.position = new Vector3(UnityEngine.Random.Range(-20f, 20f), 0, UnityEngine.Random.Range(-20f, 20f));
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Dude_instancing")
    //    {
    //        Debug.Log("OnCollisionEnter");
    //    }
    //}

    void Update () 
	{
        if (AnimationInstancing.AnimationInstancingMgr.Instance.UseInstancing)
        {
            //if (instancing == null)
            //{
            //    gameObject.SetActive(false);
            //    return;
            //}
            //if (instancing.IsPause())
            //    instancing.CrossFade(0, 0.2f);
            //var trs = transform;
            //var pos = trs.position;
            //if (Vector3.SqrMagnitude(TargetPosition - pos) > 25)
            //{

            //    //avatar.SetFloat("Speed", 1, SpeedDampTime, Time.deltaTime);

            //    Vector3 curentDir = trs.rotation * Vector3.forward;
            //    Vector3 wantedDir = (TargetPosition - pos).normalized;

            //    //trs.Translate(wantedDir * 1.0f * Time.deltaTime);
            //    //trs.rotation.SetLookRotation(wantedDir);
            //    //gameObject.trs.rotation.SetLookRotation(wantedDir);
            //    //gameObject.trs.rotation = Quaternion.LookRotation(TargetPosition - pos);
            //    trs.rotation = Quaternion.RotateTowards(trs.rotation, Quaternion.LookRotation(TargetPosition - pos), 8.0f);
            //}
            //else
            //{

            //    //if (avatar.GetFloat("Speed") < 0.01f)
            //    {
            //        instancing.PlayAnimation(UnityEngine.Random.Range(0, 2));
            //        //instancing.PlayAnimation(1);
            //        //instancing.CrossFade(1, 0.1f);
            //        TargetPosition = new Vector3(UnityEngine.Random.Range(-AvatarRange, AvatarRange), 0, UnityEngine.Random.Range(-AvatarRange, AvatarRange));
            //        trs.rotation = Quaternion.RotateTowards(trs.rotation, Quaternion.LookRotation(TargetPosition - pos), 0.1f);
            //        //gameObject.trs.rotation = Quaternion.LookRotation(TargetPosition - pos);
            //    }
            //}
        }
        else if (avatar)
        {
            int rand = UnityEngine.Random.Range(0, 50);

            avatar.SetBool("Jump", rand == 20);
            avatar.SetBool("Dive", rand == 30);

            if (Vector3.Distance(TargetPosition, avatar.rootPosition) > 5)
            {
                avatar.SetFloat("Speed", 1, SpeedDampTime, Time.deltaTime);

                Vector3 curentDir = avatar.rootRotation * Vector3.forward;
                Vector3 wantedDir = (TargetPosition - avatar.rootPosition).normalized;

                if (Vector3.Dot(curentDir, wantedDir) > 0)
                {
                    avatar.SetFloat("Direction", Vector3.Cross(curentDir, wantedDir).y, DirectionDampTime, Time.deltaTime);
                }
                else
                {
                    avatar.SetFloat("Direction", Vector3.Cross(curentDir, wantedDir).y > 0 ? 1 : -1, DirectionDampTime, Time.deltaTime);
                }
            }
            else
            {
                avatar.SetFloat("Speed", 0, SpeedDampTime, Time.deltaTime);

                if (avatar.GetFloat("Speed") < 0.01f)
                {
                    TargetPosition = new Vector3(UnityEngine.Random.Range(-AvatarRange, AvatarRange), 0, UnityEngine.Random.Range(-AvatarRange, AvatarRange));
                }
            }
        }
    }
}
