
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AGXNASK 
{

public class Terrain : IndexVertexBuffers {
   protected VertexPositionColor[] vertex;  // stage vertices    
   private int height, width, multiplier = 20, spacing;
   private int[,] terrainHeight;
   private BasicEffect effect;
   private GraphicsDevice display;
   private Texture2D heightTexture, colorTexture;
   private Microsoft.Xna.Framework.Color[] heightMap, colorMap;


   public Terrain(Stage theStage, string label, string heightFile, string colorFile) 
      : base (theStage, label) 
      {
      int i;
      range = stage.Range;
      width = height = range;
      nVertices = width * height;
      terrainHeight = new int[width, height];
      vertex = new VertexPositionColor[nVertices];
      nIndices = (width -1) * (height - 1) * 6;
      indices = new int[nIndices];  // there are 6 indices 2 faces / 4 vertices 
      spacing = stage.Spacing;
      // set display information 

      display = stage.Display;
    
      effect = stage.SceneEffect;
      heightTexture = stage.Content.Load<Texture2D>(heightFile);
      heightMap = 
         new Microsoft.Xna.Framework.Color[width * height];
      heightTexture.GetData<Microsoft.Xna.Framework.Color>(heightMap);
      // create colorMap values from colorTexture
      colorTexture = stage.Content.Load<Texture2D>(colorFile);
      colorMap = 
         new Microsoft.Xna.Framework.Color[width * height];
      colorTexture.GetData<Microsoft.Xna.Framework.Color>(colorMap);
      // create  vertices for terrain
      Vector4 vector4;
      int vertexHeight;
      i = 0;
      for (int z = 0; z < height; z++)
         for (int x = 0; x < width; x++)  {
            vector4 = heightMap[i].ToVector4();       // convert packed Rgba32 values to floats
            vertexHeight = (int) (vector4.X * 255);   // scale vertexHeight 0..255
            vertexHeight *= multiplier;               // multiply height
            terrainHeight[x, z] = vertexHeight;       // save height for navigation
            vertex[i] = new VertexPositionColor(
               new Vector3(x * spacing, vertexHeight, z * spacing), 
               new Color(colorMap[i].ToVector4()));
            i++;
            }
      // free up unneeded maps
      colorMap = null;
      heightMap = null; 
      // set indices clockwise from point of view
      i = 0;
      for (int z = 0; z < height - 1; z++)
         for (int x = 0; x < width - 1; x++) {
            indices[i++] = z * width + x;
            indices[i++] = z * width + x + 1;
            indices[i++] = (z + 1) * width + x;
            indices[i++] = (z + 1) * width + x;
            indices[i++] = z * width + x + 1;
            indices[i++] = (z + 1) * width + x + 1;
            }
      
      // create VertexBuffer and store on GPU
      vb = new VertexBuffer(display, typeof(VertexPositionColor), vertex.Length, BufferUsage.WriteOnly);
      vb.SetData<VertexPositionColor>(vertex); // , 0, vertex.Length);
      // create IndexBuffer and store on GPU
      ib = new IndexBuffer(display,typeof(int), indices.Length, BufferUsage.WriteOnly);
      IB.SetData<int>(indices);     
      }

   // Properties

   public int Spacing 
   {
      get { return stage.Spacing;} 
   }

   public float surfaceHeight(int x, int z) {
      if (x < 0 || x > 511 || z < 0 || z > 511) return 0.0f;  // index valid ?
      return (float) terrainHeight[x,z]; }

   public override void  Draw(GameTime gameTime) {
      effect.VertexColorEnabled = true;
      if (stage.Fog) {
            effect.FogColor = Color.CornflowerBlue.ToVector3();
            effect.FogStart = stage.FogStart;
            effect.FogEnd = stage.FogEnd;
            effect.FogEnabled = true;
            }
         else effect.FogEnabled = false;
      effect.DirectionalLight0.DiffuseColor = stage.DiffuseLight;
      effect.AmbientLightColor = stage.AmbientLight;
      effect.DirectionalLight0.Direction = stage.LightDirection;
      effect.DirectionalLight0.Enabled = true;             
      effect.View = stage.View;
      effect.Projection = stage.Projection;
      effect.World = Matrix.Identity;
      foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
         pass.Apply(); 
         display.SetVertexBuffer(vb);
         display.Indices = ib;
         display.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,nVertices,
            0, nIndices/3);
         }
      }
   }
}
