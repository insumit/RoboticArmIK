//using UnityEngine;
//using System.Collections;

//public class CameraCapture : MonoBehaviour
//{
//    public Camera cam;
//    RenderTexture renderTexture;
//    //public Texture2D RenderResult;
//    public Vector2 targetSize;
//    Texture2D lastPicture;
//    void Start()
//    {
//        //cam = GetComponent<Camera>();
//        renderTexture = new RenderTexture((int)targetSize.x, (int)targetSize.y, 24);
//        cam.targetTexture = renderTexture;
//        cam.Render();
//    }


//    void OnGUI()
//    {
//        cam.Render();
//        //if (GUILayout.Button("Capture"))
//        //{
//        //    //RenderTexture.active = renderTexture;
//        //    Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
//        //    texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
//        //    byte[] bytes;
//        //    bytes = texture.EncodeToPNG();
//        //    System.IO.File.WriteAllBytes("screenshot.png", bytes);
//        //    Debug.Log(Application.persistentDataPath + "/" + "screenshot.png");
//        //    lastPicture = texture;
//        //}

//        GUILayout.Box(cam.targetTexture, GUILayout.Width(targetSize.x), GUILayout.Height(targetSize.y));
//    }


//void OnPostRender()
//{
//    // Camera owner = GetComponent<Camera>();
//    RenderTexture target = cam.targetTexture;
//    if (target == null)
//        return;
//    Texture2D RenderResult = new Texture2D(target.width, target.height, TextureFormat.RGB24, true);
//    Rect rect = new Rect(0, 0, target.width, target.height);
//    RenderResult.ReadPixels(rect, 0, 0, true);
//    byte[] bytes;
//    bytes = RenderResult.EncodeToPNG();
//    System.IO.File.WriteAllBytes("screenshot.png", bytes);
//    Debug.Log(Application.persistentDataPath + "/" + "screenshot.png");

//}
//}
