using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreateAndOperate;

namespace NatureLight
{
    struct WindowParameterStruct
    {
        public Vector3 Position;
        public float Width;
        public float Height;
    }

    public struct WindowProperty
    {
        public float Width;
        public float Height;
        public float SlidWidth;
        public float GroundHeight;
    }

    public class WindowManager : MonoBehaviour
    {

        //窗户
        private List<GameObject> windows = new List<GameObject>();

        //面板属性
        private WindowProperty CurrentProperty;

        [Header("菜单")]
        public MenuManager CanvasMenuManager;


        public GameObject WindowPrefab;

        public GameObject Plane;

        public Vector3 LiftPosiition;
        public float Length;

        //窗户的参数，用于挖洞
        [HideInInspector]
        public Vector4[] SectionDirX = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionDirY = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionDirZ = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionCentre = new Vector4[10];
        [HideInInspector]
        public Vector4[] SectionScale = new Vector4[10];


        private Transform Box;
        private IlluminatedPlane PlaneScript;


        // Use this for initialization
        void Start()
        {
            PlaneScript = Plane.GetComponent<IlluminatedPlane>();
        }

        // Update is called once per frame
        void Update()
        {



            //if (SeletedGameObject.Count > 0)
            //{
            //    if (SeletedGameObject[0].GetComponent<CreatedWindow>() != null)
            //    {
            //        //设置光照平面上的材质参数
            //        WindowPara = SeletedGameObject[0].GetComponent<CreatedWindow>().GetWindowParameter();
            //        PlaneMaterial.SetVector("_WindowPosition", WindowPara.WindowPosition);
            //        PlaneMaterial.SetFloat("_WindowWidth", WindowPara.WindowWidth);
            //        PlaneMaterial.SetFloat("_WindHeight", WindowPara.WindowHeight);

            //        //设置面板上的参数
            //        Canvas.GetComponent<DataManager>().SetWinderPara(WindowPara);
            //    }

            //}



            for (int i = 0; i < windows.Count; i++)
            {
                Box = windows[i].GetComponent<SideWindow>().CrossSectionBox.transform;
                SectionDirX[i] = Box.right;
                SectionDirY[i] = Box.up;
                SectionDirZ[i] = Box.forward;
                SectionCentre[i] = Box.position;
                SectionScale[i] = Box.localScale;

                PlaneScript.WindowPositionArray[i] = Box.position;
                PlaneScript.WindowWidthArray[i] = Box.localScale.x;
                PlaneScript.WindowHeightArray[i] = Box.localScale.y;

            }

            PlaneScript.WindowsCount = windows.Count;
           



            //将窗户参数组传入shader中
            Shader.SetGlobalVectorArray("_SectionDirX", SectionDirX);
            Shader.SetGlobalVectorArray("_SectionDirY", SectionDirY);
            Shader.SetGlobalVectorArray("_SectionDirZ", SectionDirZ);
            Shader.SetGlobalVectorArray("_SectionCentre", SectionCentre);
            Shader.SetGlobalVectorArray("_SectionScale", SectionScale);
            Shader.SetGlobalInt("_Count", windows.Count);

        }


        //改变窗户的形式，上中下
        public void SwitchWindowForm(int Form)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                GameObject.Destroy(windows[i]);
            }

            windows.Clear();

            //相关物体的激活
            for (int i = 0; i < Form; i++)
            {
                Debug.Log("chuangjian window");
                GameObject CreatedWindow = GameObject.Instantiate(WindowPrefab);

                CreatedWindow.transform.position = new Vector3(LiftPosiition.x + (i+1) * Length /(Form+1), LiftPosiition.y, LiftPosiition.z);

                windows.Add(CreatedWindow);
            }

            //更新面板上的属性值
            UpdateWindowProperty();
        }

        //通过新的面板属性改变天窗
        public void SetWindowPorperty(WindowProperty NewProperty)
        {
  
            SideWindow.WindowParameter NewPara = new SideWindow.WindowParameter();
    
            for (int i = 0;i<windows.Count; i++)
           {
                Debug.Log(NewProperty.Width);
                NewPara.WindowWidth = NewProperty.Width;
                NewPara.WindowHeight = NewProperty.Height;
                NewPara.WindowPosition.Set(windows[i].transform.position.x, NewProperty.GroundHeight + (-3.11f), windows[i].transform.position.z);
                windows[i].GetComponent<SideWindow>().SetWindowPara(NewPara);
           }
        }


        //更新面板属性
        public void UpdateWindowProperty()
        {
            //面板属性改变

            if (windows.Count > 0)
            {
                CurrentProperty.Width = windows[0].GetComponent<SideWindow>().GetWindowParameter().WindowWidth;
                CurrentProperty.Height = windows[0].GetComponent<SideWindow>().GetWindowParameter().WindowHeight;
                CurrentProperty.SlidWidth = 0.2f;
                CurrentProperty.GroundHeight = windows[0].GetComponent<SideWindow>().GetWindowParameter().WindowPosition.y - (-3.11f);
            }
            else
            {
                CurrentProperty.Width = 0;
                CurrentProperty.Height = 0;
                CurrentProperty.SlidWidth = 0;
                CurrentProperty.GroundHeight = 0;
            }
                //更新面板上的属性值
                CanvasMenuManager.UpdateWindowPropertyValue(CurrentProperty);
        }


        //更新横截面和 光照面上的数组
        public  void UpdateArray()
        {

        }
    }
}




