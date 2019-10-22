using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour {
    static public ProjectileLine S;

    [Header("Set in Inspector")]
    public float minDist = 0.1f;
    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;
    void Awake()
       
    {
        S = this;
        line = GetComponent<LineRenderer>();

        line.enabled = false;
       
        points = new List<Vector3>();
    }
    
    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null)
            {
              
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }
    
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }
    public void AddPoint()
    {
      
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
          
            return;
        }
        if (points.Count == 0)
        { // If this is the launch point...
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS; 
                                                       
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            // Sets the first two points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            // Enables the LineRenderer
            line.enabled = true;
        }
        else
        {
            // Normal behavior of adding a point
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }
    // Returns the location of the most recently added point
    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                // If there are no points, returns Vector3.zero
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }
    void FixedUpdate()
    {
        if (poi == null)
        {
            // If there is no poi, search for one
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                {
                    return; // Return if we didn't find a poi
                }
            }
            else
            {
                return; // Return if we didn't find a poi
            }
        }
        // If there is a poi, it's loc is added every FixedUpdate
        AddPoint();
        if (FollowCam.POI == null)
        {
            // Once FollowCam.POI is null, make the local poi nulll too
            poi = null;
        }
    }
}