
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



public class Camera  {
   public enum CameraEnum { TopDownCamera, FirstCamera, FollowCamera, AboveCamera }
   Object3D agent;
   private int terrainCenter, offset = 300;
   private Matrix viewMatrix;
   private string name;
   private Stage scene;
   CameraEnum cameraCase;

   public Camera(Stage aScene, CameraEnum cameraType) { 
      name = "Whole stage";
      scene = aScene;
      cameraCase = cameraType;
      terrainCenter = scene.TerrainSize / 2;
      updateViewMatrix();
      }

   public Camera(Stage aScene, Object3D anAgentObject, CameraEnum cameraType) {
      scene = aScene;
      agent = anAgentObject;
      cameraCase = cameraType;
      }
      
   // Properties

   public string Name {
      get { return name; }
      set { name = value; }}
         
   public Matrix ViewMatrix {
      get { return viewMatrix;}}

   public void updateViewMatrix() { 
      switch (cameraCase) {
         case CameraEnum.TopDownCamera:
         viewMatrix = Matrix.CreateLookAt(
            new Vector3(terrainCenter, scene.FarYon - 50, terrainCenter),
            new Vector3(terrainCenter, 0, terrainCenter), 
            new Vector3(0, 0, -1));
            break;
         case CameraEnum.FirstCamera:
            viewMatrix = Matrix.CreateLookAt(agent.Translation,
               agent.Translation + agent.Forward, agent.Orientation.Up);
            viewMatrix *= Matrix.CreateTranslation(0, -offset, 0);
            break;
         case CameraEnum.FollowCamera:
            viewMatrix = Matrix.CreateLookAt(agent.Translation,
               agent.Translation + agent.Forward, agent.Orientation.Up);
            viewMatrix *= Matrix.CreateTranslation(0, -2 * offset, -8 * offset);
            break;
         case CameraEnum.AboveCamera:
            viewMatrix = Matrix.CreateLookAt(
               new Vector3(agent.Translation.X, agent.Translation.Y + 3 * offset,
               agent.Translation.Z), 
               agent.Translation, new Vector3(0, 0, -1));
            break;
         }
      }           
   }
}
