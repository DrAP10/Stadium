using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G9ModifyTerrain : MonoBehaviour {
    public Terrain terr; // terrain to modify 
    int hmWidth; // heightmap width 
    int hmHeight; // heightmap height


    int posXInTerrain; // position of the game object in terrain width (x axis)
    int posYInTerrain; // position of the game object in terrain height (z axis)


    int size = 0; // the diameter of terrain portion that will raise under the game object 
    float desiredHeight = 0; // the height we want that portion of terrain to be
    
    int counter = 1;
    int textureExtraSize = 8;
    public int IncrementLoop = 4;
    public int IncrementAmount = 2;
    public int MaxSize = 26;

    void Start()
    {
        //terr = Terrain.activeTerrain;
        hmWidth = terr.terrainData.heightmapWidth;
        hmHeight = terr.terrainData.heightmapHeight;

    }


    public void LowerTerrain()
    {
        // get the normalized position of this game object relative to the terrain
        Vector3 tempCoord = (transform.position - terr.gameObject.transform.position);
        Vector3 coord;
        coord.x = tempCoord.x / terr.terrainData.size.x;
        coord.y = tempCoord.y / terr.terrainData.size.y;
        coord.z = tempCoord.z / terr.terrainData.size.z;
        // get the position of the terrain heightmap where this game object is
        posXInTerrain = (int)(coord.x * hmWidth);
        posYInTerrain = (int)(coord.z * hmHeight);
        // we set an offset so that all the raising terrain is under this game object
        int offset = size / 2;
        // get the heights of the terrain under this game object
        float[,] heights = terr.terrainData.GetHeights(posXInTerrain - offset, posYInTerrain - offset, size, size);
        // get the texture map of the terrain under this game object
        float[,,] alphaData = terr.terrainData.GetAlphamaps(posXInTerrain - offset - (textureExtraSize/2),
            posYInTerrain - offset - (textureExtraSize / 2), size + textureExtraSize, size + textureExtraSize);
        // we set each sample of the terrain in the size to the desired height
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                //only allow coord into a circle
                if(Vector2.Distance(new Vector2(size/2,size/2),new Vector2(i,j)) <= size / 2)
                {
                    heights[i, j] -= 0.0001f;
                }
            }
        // we set each sample of the terrain in the size with the desired texture
        for (int i = 0; i < size + textureExtraSize; i++)
            for (int j = 0; j < size + textureExtraSize; j++)
            {
                //only allow coord into a circle
                if (Vector2.Distance(new Vector2((size + textureExtraSize) / 2, (size + textureExtraSize) / 2),
                    new Vector2(i, j)) <= (size + textureExtraSize) / 2)
                {
                    alphaData[i, j, 0] = 0;
                    alphaData[i, j, 1] = 1;
                    alphaData[i, j, 2] = 0;
                }
            }
        // go raising the terrain slowly
        desiredHeight += Time.deltaTime;
        // set the new height
        terr.terrainData.SetHeights(posXInTerrain - offset, posYInTerrain - offset, heights);
        // set the new texture
        terr.terrainData.SetAlphamaps(posXInTerrain - offset - (textureExtraSize / 2),
            posYInTerrain - offset - (textureExtraSize / 2), alphaData);
        //increase size
        if (size < MaxSize && counter% IncrementLoop == 0)
                size += IncrementAmount;
        counter++;
    }
    private void OnDestroy()
    {
        // get the normalized position of this game object relative to the terrain
        Vector3 tempCoord = (transform.position - terr.gameObject.transform.position);
        Vector3 coord;
        coord.x = tempCoord.x / terr.terrainData.size.x;
        coord.y = tempCoord.y / terr.terrainData.size.y;
        coord.z = tempCoord.z / terr.terrainData.size.z;
        // get the position of the terrain heightmap where this game object is
        posXInTerrain = (int)(coord.x * hmWidth);
        posYInTerrain = (int)(coord.z * hmHeight);
        // we set an offset so that all the raising terrain is under this game object
        int offset = size / 2;
        // get the heights of the terrain under this game object
        float[,] heights = terr.terrainData.GetHeights(posXInTerrain - offset, posYInTerrain - offset, size, size);
        // get the texture map of the terrain under this game object
        float[,,] alphaData = terr.terrainData.GetAlphamaps(posXInTerrain - offset - (textureExtraSize / 2),
            posYInTerrain - offset - (textureExtraSize / 2), size + textureExtraSize, size + textureExtraSize);
        // we set each sample of the terrain in the size to the desired height
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                //only allow coord into a circle
                if (Vector2.Distance(new Vector2(size / 2, size / 2), new Vector2(i, j)) <= size / 2)
                {
                    heights[i, j] = 0.1666667f;
                }
            }
        // we set each sample of the terrain in the size with the desired texture
        for (int i = 0; i < size + textureExtraSize; i++)
            for (int j = 0; j < size + textureExtraSize; j++)
            {
                //only allow coord into a circle
                if (Vector2.Distance(new Vector2((size + textureExtraSize) / 2, (size + textureExtraSize) / 2), 
                    new Vector2(i, j)) <= (size + textureExtraSize) / 2)
                {
                    alphaData[i, j, 0] = 0.8f;
                    alphaData[i, j, 1] = 0;
                    alphaData[i, j, 2] = 0.2f;
                }
            }
        // set the new height
        terr.terrainData.SetHeights(posXInTerrain - offset, posYInTerrain - offset, heights);
        terr.terrainData.SetAlphamaps(posXInTerrain - offset - (textureExtraSize / 2), 
            posYInTerrain - offset - (textureExtraSize / 2), alphaData);
    }
}
