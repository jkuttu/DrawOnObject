using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    Color brushColor = Color.red;
    // Update is called once per frame
    void Update()
    {
        // Vector3 markerDirection = transform.TransformDirection(Vector3.forward);
        

        Ray paintRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Input.mousePosition,paintRay.direction,Color.green);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            brushColor = Random.ColorHSV(0.5f,1f);
        }

        if (Physics.Raycast(paintRay, out RaycastHit hit, 100f))
        {


            Renderer rend = hit.transform.GetComponent<Renderer>();
            Texture2D tex = rend.material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;

            Debug.Log(pixelUV);
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;
            Debug.Log("after "+pixelUV);
            Debug.Log("----------------------------");

           // CleanCanvas(tex);
            for (int i = 0; i < 20; i++)
            {

                 int x = (int)pixelUV.x;
                int y = (int)pixelUV.y;

                // y += i;
                // x += i;
                CircleBresenham(x,y,i,tex);

                //tex.SetPixel(x, y, brushColor);
                
            }
           

            tex.Apply();

        }

    }

#region CleanCanvasCode
    private static void CleanCanvas(Texture2D tex)
    {
        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {


                // int x = (int)pixelUV.x;
                // int y = (int)pixelUV.y;

                // y += i;
                // x += i;

                tex.SetPixel(i, j, Color.white);
            }
        }
    }
#endregion
    void CircleBresenham(int xc,int yc ,int r,Texture2D tex)
    {
        int x=0,y=r;
        int d = 3-2 *r;

        DrawCircle(xc,yc,x,y,tex);
        while(y>=x)
        {
            x++;
            if(d>0)
            {
                y--;
                d=d+4*(x-y)+10;
            }
            else {
                d = d+4*x+6;
            }

            DrawCircle(xc,yc,x,y,tex);

        }
    }

    void DrawCircle(int xc,int yc,int x,int y,Texture2D tex)
    {
        tex.SetPixel(xc+x,yc+y,brushColor);
        tex.SetPixel(xc-x,yc+y,brushColor);
        tex.SetPixel(xc+x,yc-y,brushColor);
        tex.SetPixel(xc-x,yc-y,brushColor);

        tex.SetPixel(xc+y,yc+x,brushColor);
        tex.SetPixel(xc-y,yc+x,brushColor);
        tex.SetPixel(xc+y,yc-x,brushColor);
        tex.SetPixel(xc-y,yc-x,brushColor);

    }
}
