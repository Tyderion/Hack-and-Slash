using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public int maxHealth = 100;
    public int curHealth = 100;

    public bool showHealthBar = false;

    public FontStyle fontStyle;

    private GUIStyle healthBarStyle;
     private GUIStyle healthBarStyleText;
    private GUIStyle healthBarBoxStyle_outer = null;
    private GUIStyle healthBarBoxStyle_inner = null;

    private Transform myTransform;

    private Rect healthBar;
    private Rect healthBarBox_inner;
    private Rect healthBarBox_outer;

    private float healthBarBorder = 1.0f;

    private Texture2D backgroundTexture;
    private Texture2D borderTexture;
    private Texture2D clearTexture;

    private Font font;


    // Initializes border and background textures.
    void initTextures()
    {



        backgroundTexture = new Texture2D(1, 1);
        backgroundTexture.SetPixel(0,0,Color.cyan);
        backgroundTexture.Apply();

        borderTexture = new Texture2D(1, 1);
        borderTexture.SetPixel(0,0,Color.black);
        borderTexture.Apply();
        
        
        clearTexture = new Texture2D(1, 1);
        clearTexture.SetPixel(0,0,Color.clear);
        clearTexture.Apply();
    }

    Texture2D healthTexture()
    {
        Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        float healthPercentage = ((float)curHealth+maxHealth/5)/((float)maxHealth+maxHealth/5);
        if (healthPercentage < 0.2)
            healthPercentage -= 0.1f;
        if (healthPercentage < 0) 
            healthPercentage = 0;
//        else
//            healthPercentage = 1;
        tex.SetPixel(0,0,new Color(1-healthPercentage,healthPercentage,0));

//        for (int i = 0; i <=  tex.width;i++)
//        {
//            for (int j = 0; j <= tex.height; j++)
//            {
//                //tex.SetPixel(i,j,new Color((1-healthPercentage)*255 ,healthPercentage*255,0));
//                tex.SetPixel(i,j,new Color(0,healthPercentage*255,0));
//                //tex.SetPixel(i,j,new Color(150,105,0f));
//            }
//        }
        tex.Apply();
        if (tag == "Enemy")
        {
            Debug.Log("HealthPercentage: " + healthPercentage);
            //Debug.Log("New Health Color: rgb("+ (1-healthPercentage)*255 +","+healthPercentage*255+","+0+")");
        }
        ;
        return tex;
    }

    // Use this for initialization
    void Start ()
    {
        initTextures();
           myTransform = transform;

        fontStyle = FontStyle.Normal;

         // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps


        // connect texture to material of GameObject this script is attached to
  //      renderer.material.mainTexture = texture;

        int width = Screen.width/2;
        healthBar = new Rect(10, 0,width, 20);









        switch (tag) {
        case "Player":
            healthBar.y = 10;
            showHealthBar = true;
            break;
        case "Enemy":
            healthBar.y = 60;
//            Debug.Log("Enemy Health Bar set for " + myTransform.name);
//            Targeting targeting = (Targeting)GameObject.FindGameObjectWithTag("Player").GetComponent("Targeting");
//            if(targeting != null)
//                healthBar.y = 40 + (30 * targeting.targets.IndexOf(myTransform));
            break;
        }
        healthBarBox_inner = new Rect(healthBar.x, healthBar.y, healthBar.width, healthBar.height);
        healthBarBox_outer = new Rect(healthBar.x-healthBarBorder, healthBar.y-healthBarBorder, healthBar.width+2*healthBarBorder, healthBar.height+2*healthBarBorder);

    }




    // Update is called once per frame
    void Update ()
    {
        AdjustCurrentHealth (0);
    }


    void createStyles()
    {
        if (healthBarBoxStyle_inner == null)
        {
            healthBarBoxStyle_inner = new GUIStyle();
            healthBarBoxStyle_outer = new GUIStyle();
            healthBarBoxStyle_outer.normal.background = borderTexture;
            healthBarBoxStyle_inner.normal.background = backgroundTexture;
            healthBarBoxStyle_outer.stretchWidth = false;
            healthBarBoxStyle_inner.stretchWidth = false;
            healthBarBoxStyle_inner.normal.textColor = Color.gray;
            
            healthBarStyle = new GUIStyle(GUI.skin.box);
            healthBarStyle.fixedHeight=0;
            healthBarStyle.fixedWidth = 0;
            
            healthBarStyle.normal.textColor = Color.black;
            
            healthBarStyleText = new GUIStyle(healthBarStyle);
            healthBarStyleText.stretchWidth = false;
            healthBarStyleText.normal.background = clearTexture;
            
        }
        healthBarStyle.normal.background = healthTexture();
    }

    void OnGUI ()
    {


        //GUI.skin.box.fontStyle = fontStyle;

        if(showHealthBar)
        {
             createStyles();
            //GUI.skin.box.stretchWidth = false;

            //GUI.Box(healthBarBox,"");
            //GUI.DrawTexture(healthBar, redTexture);
            GUI.Box(healthBarBox_outer,"",healthBarBoxStyle_outer);
            GUI.Box(healthBarBox_inner,"",healthBarBoxStyle_inner);
            GUI.Box(healthBar,"", healthBarStyle);
            GUI.Box(healthBarBox_inner,myTransform.name + " = " + curHealth + "/" + maxHealth,healthBarStyleText);
            

            //GUI.Box(healthBarBoxBigger,"",healthBarBoxStyle2);
        }

    }

    public void AdjustCurrentHealth (int adj)
    {
        curHealth += adj;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
        if (curHealth < 1){
            Targeting targeting = (Targeting)GameObject.FindGameObjectWithTag("Player").GetComponent("Targeting");
            targeting.RemoveTarget(myTransform);
            Destroy(gameObject);
        }

//            curHealth = 0;
        healthBar.width = (Screen.width / 2) * (curHealth / (float)maxHealth);
    }
}
