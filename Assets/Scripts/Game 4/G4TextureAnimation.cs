using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G4TextureAnimation : MonoBehaviour {

    public float HORIZONTAL_SPEED = -0.2f;
    public float VERTICAL_SPEED = -0.3f;
    Material wallMaterial;
	// Use this for initialization
	void Start () {
        wallMaterial = GetComponent<MeshRenderer>().materials[1];

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 currentOffset = wallMaterial.mainTextureOffset;
        wallMaterial.mainTextureOffset = new Vector2((HORIZONTAL_SPEED * Time.deltaTime + currentOffset.x) % 1,
            (VERTICAL_SPEED * Time.deltaTime + currentOffset.y) % 1);
	}
}
