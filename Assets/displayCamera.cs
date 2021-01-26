using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayCamera : MonoBehaviour
{
    public Camera cam;
    RenderTexture renderTexture;
    //public Texture2D RenderResult;
   public Vector2 targetSize;
    //Texture2D lastPicture;
    void Start()
    {
        //cam = GetComponent<Camera>();
        renderTexture = new RenderTexture((int)targetSize.x, (int)targetSize.y, 24);
        cam.targetTexture = renderTexture;
    }
    private void Update()
    {
        cam.Render();
        var renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = cam.targetTexture;
    }
    
}
