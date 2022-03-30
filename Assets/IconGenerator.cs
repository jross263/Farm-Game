using UnityEngine;

public class IconGenerator : MonoBehaviour
{
    [SerializeField] private string folderPath;
    [SerializeField] private string fileName;
    
    private Camera _camera;

    [ContextMenu("Screenshot")]
    private void TakeScreenshot()
    {
        if(_camera == null)
        {
            _camera = GetComponent<Camera>();
        }
        RenderTexture rt = new RenderTexture(256, 256, 24);
        _camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        _camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        _camera.targetTexture = null;
        RenderTexture.active = null;
        if(Application.isEditor){
            DestroyImmediate(rt);
        }
        else{
            Destroy(rt);
        }
        byte[] bytes = screenShot.EncodeToPNG();
        print(Application.dataPath);
        System.IO.File.WriteAllBytes(Application.dataPath + "/" + folderPath + "/" + fileName  + ".png", bytes);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
        
    }
}
