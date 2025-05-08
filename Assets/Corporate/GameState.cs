public static class GameState
{
    public static int[] successes = new int[20];
    public static int[] attempts = new int[20];

    public static bool game_over = false;

    public static bool loaded = false;



    // settings

    public static bool fullscreen;
    //public static bool vsync = false;

    public static int rez_i = 3;
    public static int[,] resolutions = {
        { 1280, 720 },
        { 1366, 768 },
        { 1440, 900 },
        { 1920, 1080 },
        { 2650, 1440 },
        { 3840, 2160 },
    };

    public static int fps_i = 1;
    public static int[] FPSs = { 30, 60, 75, 120, 144, 165, 240 };

    public static bool motion_blur = true;

    public static int master_vol = 100;
    public static int music_vol = 100;
    public static int sfx_vol = 100;
    public static int ambient_vol = 100;

    public static float mouse_sensitivity = 0.3f;




    // Don't delete (breaks save/load)

    public static int currentFile;
    public static bool levelOver;


    // Don't delete (breaks transitions)

    public static bool currently_transitioning = false;


    // Don't delete 

    public static bool logo_done = false;
}