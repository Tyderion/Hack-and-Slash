using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public int maxHealth = 100;
    public int curHealth = 100;

    public bool showHealthBar = false;

    public FontStyle fontStyle;
    public GUIStyle healthBarStyle;
    public GUIStyle healthBarBoxStyle_outer;
    public GUIStyle healthBarBoxStyle_inner;
    
    private Transform myTransform;

    private Rect healthBar;
    private Rect healthBarBox_inner;
    private Rect healthBarBox_outer;
    
    private float healthBarBorder = 1.0f;

    private Texture2D healthTexture; 
    private Texture2D backgroundTexture;
    private Texture2D borderTexture;

    private Font font;

    private Texture2D healthFull;
    private Texture2D healthDepleted;
 
    
    void initTextures()
    {
        healthTexture = new Texture2D(1, 1);
        for (int i = 0; i <=  healthTexture.width;i++)
            for (int j = 0; j <= healthTexture.height; j++)
                healthTexture.SetPixel(i,j,Color.red);
        healthTexture.Apply();
        
        backgroundTexture = new Texture2D(1, 1);
        for (int i = 0; i <=  backgroundTexture.width;i++)
            for (int j = 0; j <= backgroundTexture.height; j++)
                backgroundTexture.SetPixel(i,j,Color.cyan);
        backgroundTexture.Apply();
        
        borderTexture = new Texture2D(1, 1);
        for (int i = 0; i <=  borderTexture.width;i++)
            for (int j = 0; j <= borderTexture.height; j++)
                borderTexture.SetPixel(i,j,Color.black);
        borderTexture.Apply();
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

       
        
        healthBarStyle.normal.background = healthTexture;
        
        healthBarBoxStyle_outer.normal.background = borderTexture;
        healthBarBoxStyle_inner.normal.background = backgroundTexture;
        healthBarBoxStyle_outer.stretchWidth = false;
        healthBarBoxStyle_inner.stretchWidth = false;




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
        
        //healthBarBoxBigger = new Rect(healthBar.x-5,healthBar.y-5, healthBar.width+10, healthBar.height+10);




//        healthBarBoxStyle.fixedWidth = this.width;
//        healthBarBoxStyle.fixedHeight = healthBarBox.height;
//        healthBarBoxStyle.stretchWidth = false;
//

    }

    // Update is called once per frame
    void Update ()
    {
        AdjustCurrentHealth (0);
    }

    void OnGUI ()
    {

        //GUI.skin.box.fontStyle = fontStyle;

        if(showHealthBar)
        {
            //GUI.skin.box.stretchWidth = false;
            
            //GUI.Box(healthBarBox,"");
            //GUI.DrawTexture(healthBar, redTexture);
            GUI.Box(healthBarBox_outer,"",healthBarBoxStyle_outer);
            GUI.Box(healthBarBox_inner,"",healthBarBoxStyle_inner);
            
            GUI.Box(healthBar, myTransform.name + " = " + curHealth + "/" + maxHealth, healthBarStyle);
            
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
