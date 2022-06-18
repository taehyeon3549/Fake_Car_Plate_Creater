using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
 
public class ScreenShot : MonoBehaviour {
    public Camera camera;       //보여지는 카메라.
 
    private int resWidth;
    private int resHeight;
    private string path;
    private DirectoryInfo dir;
    
    private RenderTexture rt;
    private Texture2D screenShot;
    private Rect rec;
    // Use this for initialization
    void Start () {
        resWidth = Screen.width;
        resHeight = Screen.height;
        // path = Application.dataPath+"/ScreenShot/";
        path = "C:\\Users\\winitech\\Desktop\\TF\\RCNN\\Unity_Captured_txt\\";        
        dir = new DirectoryInfo(path);

        rt = new RenderTexture(resWidth, resHeight, 24);
        screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        rec = new Rect(0, 0, screenShot.width, screenShot.height);
        
    }
 
    public void ClickScreenShot(int n1,int n2,int n3,int n4,int n5,int n6,int n7,int k1)
    {
        int ck1 = k1 + 10;
        
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }
        string name;
        name = path + "default_plate_"+ck1+n1+n2+n3+n4+n5+n6+n7 + ".jpg";
        camera.targetTexture = rt;
        
        
        camera.Render();
        RenderTexture.active = rt;        
        screenShot.Apply();
        screenShot.ReadPixels(rec, 0, 0);
 
        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(name, bytes);

        System.GC.Collect();        
    }
}