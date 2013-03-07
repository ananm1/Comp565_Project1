

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

namespace AGXNASK {

/// <summary>
/// A collection of brick.x Models. 
/// Used for path finding and obstacle avoidance algorithms
/// 
/// 1/25/2012 last changed
/// </summary>
public class Wall : Model3D {

public Wall(Stage theStage, string label, string meshFile)  : base(theStage, label, meshFile) {
   isCollidable = true;
   // "just another brick in the wall", Pink Floyd
   int spacing = stage.Terrain.Spacing;
   Terrain terrain = stage.Terrain;
   int wallBaseX = 300;
   int wallBaseZ = 448;
   int xPos, zPos;
   // 8 right
   for (int i = 0; i < 7; i++) {
      xPos =  i + wallBaseX;
      zPos =  wallBaseZ;
      addObject(new Vector3(xPos * spacing, terrain.surfaceHeight(xPos, zPos), zPos * spacing), Vector3.Up, 0.0f);
      }
   // up 7 then down 18
   for (int i = 0; i < 18; i++) {
      xPos =  wallBaseX + 7;
      zPos =  i - 7 + wallBaseZ;
      addObject(new Vector3(xPos * spacing, terrain.surfaceHeight(xPos, zPos), zPos * spacing), Vector3.Up, 0.0f);
      }
   // 4 up, after skipping 3 left
   for (int i = 0; i < 4; i++) {
      xPos =  wallBaseX + 1;
      zPos =  wallBaseZ + 10 - i;
      addObject(new Vector3(xPos * spacing, terrain.surfaceHeight(xPos, zPos), zPos * spacing), Vector3.Up, 0.0f);
      }
   //  up 1 left 8
   for (int i = 0; i < 8; i++) {
      xPos =  -i + wallBaseX + 1;
      zPos =  wallBaseZ + 6;
      addObject(new Vector3(xPos * spacing, terrain.surfaceHeight(xPos, zPos), zPos * spacing), Vector3.Up, 0.0f);
      }
   // up 12    
   for (int i = 0; i < 12; i++) {
      xPos =  wallBaseX - 6;
      zPos =  -i + wallBaseZ + 5;
      addObject(new Vector3(xPos * spacing, terrain.surfaceHeight(xPos, zPos), zPos * spacing), Vector3.Up, 0.0f);
      }
   // 8 right
   for (int i = 0; i < 8; i++) {
      xPos =  i + wallBaseX - 6;
      zPos =  wallBaseZ - 6;
      addObject(new Vector3(xPos * spacing, terrain.surfaceHeight(xPos, zPos), zPos * spacing), Vector3.Up, 0.0f);
      }
   // up 2
   for (int i = 0; i < 2; i++) {
      xPos =  wallBaseX + 1;
      zPos =  wallBaseZ - 6 - i;
      addObject(new Vector3(xPos * spacing, terrain.surfaceHeight(xPos, zPos), zPos * spacing), Vector3.Up, 0.0f);
      }
   }
}
}