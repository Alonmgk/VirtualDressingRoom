using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PersonController : MonoBehaviour
{
    public Slider hips, waist, shoulders;
    private Vector3[] vertices;
    private Mesh mesh;
    public int[] hipVerts, waistVerts, shoulderVerts;

    public float hipRange,fallOff;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = (Vector3[]) mesh.vertices.Clone();
	    //StartCoroutine(CheckVerts());
    }

    IEnumerator CheckVerts(){
	Vector3[] verts = new Vector3[vertices.Length];
	for(int i=0;i<vertices.Length;i++)
	   verts[i]=vertices[i];
	
	for(int i=35;i<verts.Length;i++){
	    Debug.Log(i);
	    verts[i] = Vector3.zero;
	    mesh.vertices = verts;
	    yield return new WaitForSeconds(1);
	    verts[i] = vertices[i];
	    mesh.vertices = verts;
	}
    }

    public void UpdateHips()
    {
        Vector3[] newVerts = new Vector3[vertices.Length];
        for(int i =0;i<vertices.Length;i++)
        {
            if (hipVerts.Contains(i))
            {
                //float distance = Mathf.Min(Vector3.Distance(vertices[i], vertices[0]), Vector3.Distance(vertices[i], vertices[1]));
                newVerts[i] = Vector3.Scale(vertices[i],new Vector3((hips.value * hipRange),1,0));
            } 
            else if (waistVerts.Contains(i))
            {
                //float distance = Mathf.Min(Vector3.Distance(vertices[i], vertices[0]), Vector3.Distance(vertices[i], vertices[1]));
                newVerts[i] = Vector3.Scale(vertices[i],new Vector3((waist.value * hipRange),1,0));
            } else if (shoulderVerts.Contains(i))
            {
                //float distance = Mathf.Min(Vector3.Distance(vertices[i], vertices[0]), Vector3.Distance(vertices[i], vertices[1]));
                newVerts[i] = Vector3.Scale(vertices[i],new Vector3((hips.value * hipRange),0,0));
            } else
                newVerts[i] = vertices[i];
        }

        mesh.vertices = newVerts;


    }
}
