using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRes : MonoBehaviour
{

    RenderTexture screenTexture;
    Camera gameCam;

    //Number of Pixels Tall and Wide to render game screen
    //Independant of window size, monitor resolution, fullscreen, etc
    public Vector2 ScreenDimensions = new Vector2(200, 300);

    //What PPU are your art assets set to?
    //can be any number, but should be consistent
    public int PixPerUnit = 16;

    //Perfect Scaling; screen only multiplies evenly by x2,x3,x4, etc
    public bool perfectScale = false;

    //Color of Borders; screen is cleared with this color
    public Color letterboxColor = Color.gray;

    // Initialize
    void Start()
    {
        //Initialize
        gameCam = gameObject.GetComponent<Camera>();
        //Error Handling
        Debug.Assert(gameCam != null, "No Camera Component Assigned");

        //Create Screen Texture
        screenTexture = new RenderTexture((int)ScreenDimensions.x, (int)ScreenDimensions.y, 0, RenderTextureFormat.ARGB32);
        screenTexture.filterMode = FilterMode.Point;

        //Initialize Screen Texture
        screenTexture.width = (int)ScreenDimensions.x;
        screenTexture.height = (int)ScreenDimensions.y;

        //Initialize Game Camera Variables
        gameCam.orthographicSize = screenTexture.height / PixPerUnit / 2;

        //Set camera to render to screenTexture instead of player window, so we can handle window ourselves
        gameCam.targetTexture = screenTexture;
    }

    //Determine Rect of where Game Screen sits in Player  (with appropriate Letterbox)
    private Rect scaleRect(int game_width, int game_height, int screen_width, int screen_height)
    {
        //Initialize with local vars for compressed code
        float sw = screen_width;
        float sh = screen_height;
        float gw = game_width;
        float gh = game_height;
        //Find the right scale to multiply the game screen by
        float scale = Mathf.Min(sw / gw, sh / gh);
        //If perfectScale is set, only multiply with whole numbers
        if (perfectScale)
        {
            scale = Mathf.Max(Mathf.Floor(scale), 1.0F);
        }
        //Place Game in center of Window
        Rect screen = new Rect(
            sw / 2.0F - (gw * scale) / 2.0F,
            sh / 2.0F - (gh * scale) / 2.0F,
            gw * scale,
            gh * scale
            );
        return screen;
    }

    //Draw Game to GUI Buffer, which acts independantly of cameras
    private void OnGUI()
    {
        //First clear screen (since a camera isn't doing it for us
        GL.Clear(true, true, letterboxColor);

        //Create a rect indicating the Game's position in the Window
        int screen_width = Screen.width;
        int screen_height = Screen.height;
        Rect screen = scaleRect(screenTexture.width, screenTexture.height,
            screen_width, screen_height);

        //Draw the 'Game' in screenTexture to the player's Window
        GUI.DrawTexture(screen, screenTexture);
    }

    //GUI aid: draw the area which will be captured by the camera
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(
            ScreenDimensions.x / PixPerUnit,
            ScreenDimensions.y / PixPerUnit, 1));
    }
}
