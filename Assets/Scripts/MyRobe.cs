using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyRobe : MonoBehaviour
{
    public float P1HDrift;
    public float P2HDrift;
    public float P2VDrift;
    public float LinePosCount;
    private List<Transform> _hingePoints;

    private LineRenderer _line;

    // Start is called before the first frame update
    void Start()
    {
        _hingePoints = transform.GetComponentsInChildren<Transform>().ToList();
        _hingePoints.RemoveAt(0);
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    private List<Vector3> GetLinePosList()
    {
        List<Vector3> linePosList = new List<Vector3>();
        List<Vector3> chainPosList = _hingePoints.Select(x => x.position).ToList();
        Vector3? preP2 = null;
        for (int i = 0; i < chainPosList.Count-1; i++)
        {
            float angle = 0;
            float angleEffect = 0;
            if (i < chainPosList.Count - 2)
            {
                angle = Vector2.Angle(chainPosList[i] - chainPosList[i + 1], chainPosList[i + 2] - chainPosList[i + 1]);
                angleEffect = 1 - (angle / 180f);
            }
            var ctrlPos = GetCtrlPoint(chainPosList[i], chainPosList[i + 1], angleEffect,preP2);
            preP2 = ctrlPos[1];
            for (int j = 0; j <LinePosCount ; j++)
            {
                Vector3 pos = Bezier(chainPosList[i], ctrlPos[0], ctrlPos[1], chainPosList[i + 1], j / LinePosCount);
                linePosList.Add(pos);
            }
        }

        return linePosList;
    }

    private void DrawLine()
    {
        var linePos =GetLinePosList();
        _line.positionCount = linePos.Count;
        _line.SetPositions(linePos.ToArray()); 
    }

    public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 temp;
        Vector3 p0p1=(1-t)*p0 +t*p1;
        Vector3 p1p2=(1-t)*p1 +t*p2;
        Vector3 p2p3=(1-t)*p2 +t*p3;
        Vector3 p0p1p2 =(1-t)*p0p1 +t*p1p2;
        Vector3 p1p2p3 =(1-t)*p1p2 +t*p2p3;
        temp =(1 - t)*p0p1p2 +t*p1p2p3;
        return temp;

    }

    private Vector3[] GetCtrlPoint(Vector3 p0, Vector3 p3, float angleEffect,Vector3? preP2 = null)
    {
        Vector3 p1,p2;
        p1=p0+(p3-p0)*P1HDrift;
        p2 =p0 +(p3-p0)* P2HDrift;
        if (preP2 != null)
        {
            p1 = p0 + (p0 - (Vector3)preP2);
        }

        p2 = (Vector2)p2 + Vector2.Perpendicular(p3 - p0).normalized * (angleEffect * P2VDrift);
        return new Vector3[] { p1, p2 };
    }


}
