﻿using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour, IDetection
{

    private PursuePlayer _pursue;
	private float _turnedAround;
	private float _checkTime;
    private bool _detectPlayer;

    private const int LayerLiving = 9;
    private LayerMask _livingLayerMask;

    public string PlayerTag = "Player";


	Ray2D _lineOfSight;
    private int _sightDistance;

	// Use this for initialization
	void Start () {
		_turnedAround = -1;
	    _livingLayerMask = 1 << LayerLiving; // should be 9?
	    _pursue = gameObject.GetComponent<PursuePlayer>();
	    _sightDistance = 10;
	    _detectPlayer = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _checkTime -= 0.1f;

		if (_checkTime <= 0) {
			CheckLineOfSight();
		    _checkTime = 0.5f;
		}
       
	}


    public void CheckLineOfSight()
	{
		_lineOfSight = new Ray2D(new Vector2(gameObject.transform.position.x + 0.9f * _turnedAround, gameObject.transform.position.y) , Vector2.right * _turnedAround);
        RaycastHit2D detectPlayer = Physics2D.Raycast(_lineOfSight.origin, _lineOfSight.direction, _sightDistance, _livingLayerMask); // distance is x distance
		Debug.DrawRay(_lineOfSight.origin, _lineOfSight.direction);
        
        if (detectPlayer.collider != null && detectPlayer.collider.tag == PlayerTag)
        {
            _detectPlayer = true; // redundant for now
            _pursue.enabled = true;
            _pursue.Chase(detectPlayer.collider.transform.gameObject);
        }
        else
        {
            _detectPlayer = false;
            //prob calcs
            _pursue.enabled = false;
        }
	}

    public void SetTurnAround(int turnAround)
    {
        _turnedAround = turnAround;
    }

}