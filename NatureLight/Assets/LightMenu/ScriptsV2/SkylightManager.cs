using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NatureLight
{
    //单个天窗的属性
    public struct SkylightParameter
    {
        public Vector3 Position;
        public float Width;
        public float Height;
    }

    //天窗的面板
    public struct SkylightProperty
    {
        public float Width;
        public float Height;
        public float Offset;
        public float GroundHeight;
    }


    public class SkylightManager : MonoBehaviour
    {
        [System.Serializable]
        public struct SkylightRef
        {
            public GameObject Tile;
            public GameObject RightCube;
            public GameObject LiftCube;
            [Space(5)]
            public GameObject Tile1;
            public GameObject RightCube1;
            public GameObject LiftCube1;

            public GameObject LightSolution;
        }

        [Header("天窗")]
        public SkylightRef Bottom;
        public SkylightRef Middle;
        public SkylightRef Top;

        [Header("菜单")]
        public MenuManager CanvasMenuManager;

        //目前的天窗引用
        private SkylightRef CurrentSkylight;

        //目前天窗的面板属性
        private SkylightProperty CurrentSkylightProperty;

        private Vector3 LiftPosition;
        private Vector3 LiftPosition1;
        float Length;


        // Use this for initialization
        void Start()
        {
          
            //默认是下窗
            CurrentSkylight = Bottom;
            CurrentSkylightProperty.Width = (CurrentSkylight.RightCube.transform.position.x - 0.5f * CurrentSkylight.RightCube.transform.localScale.x) - (CurrentSkylight.LiftCube.transform.position.x + 0.5f * CurrentSkylight.LiftCube.transform.localScale.x);
            CurrentSkylightProperty.Height = 1.0f;
            CurrentSkylightProperty.Offset = CurrentSkylight.LiftCube.transform.localScale.x + 0.5f * CurrentSkylightProperty.Width;
            CurrentSkylightProperty.GroundHeight = CurrentSkylight.LiftCube.transform.position.y - (-3.11f);

            //LiftPosition = CurrentSkylight.LiftCube.transform.position - Vector3.right * 0.5f * CurrentSkylight.LiftCube.transform.localScale.x;
            //LiftPosition1 = CurrentSkylight.LiftCube1.transform.position - Vector3.right * 0.5f * CurrentSkylight.LiftCube1.transform.localScale.x;
            //Length = (CurrentSkylight.RightCube.transform.position.x + 0.5f * CurrentSkylight.RightCube.transform.localScale.x) - (CurrentSkylight.LiftCube.transform.position.x - 0.5f * CurrentSkylight.LiftCube.transform.localScale.x);


            //更新面板上的属性值
            UpdateSkylightProperty();
        }

        // Update is called once per frame
        void Update()
        {

        }


        //改变窗户的形式，上中下
        public void SwitchSkylightForm(int Form)
        {
            switch (Form)
            {
                case 0:
                    CurrentSkylight.Tile.SetActive(true);
                    CurrentSkylight.Tile.SetActive(true);
                    CurrentSkylightProperty.Width = 0;
                    CurrentSkylightProperty.Height = 0;
                    CurrentSkylightProperty.Offset = 0;
                    CurrentSkylightProperty.GroundHeight = 0;
                    break;
                case 1:
                    SwitchSkylight(Bottom);
                    break;
                case 2:
                    SwitchSkylight(Middle);
                    break;
                case 3:
                    SwitchSkylight(Top);
                    break;
                default:
                    break;
            }
        }

        //通过新的面板属性改变天窗
        public void SetSkylightPorperty(SkylightProperty NewProperty)
        {
            LiftPosition = CurrentSkylight.LiftCube.transform.position - Vector3.right * 0.5f * CurrentSkylight.LiftCube.transform.localScale.x;
            LiftPosition1 = CurrentSkylight.LiftCube1.transform.position - Vector3.right * 0.5f * CurrentSkylight.LiftCube1.transform.localScale.x;
            Length = (CurrentSkylight.RightCube.transform.position.x + 0.5f * CurrentSkylight.RightCube.transform.localScale.x) - (CurrentSkylight.LiftCube.transform.position.x - 0.5f * CurrentSkylight.LiftCube.transform.localScale.x);

            CurrentSkylight.LiftCube.transform.position = LiftPosition + Vector3.right * 0.5f * (NewProperty.Offset - 0.5f * NewProperty.Width);

            CurrentSkylight.LiftCube.transform.localScale = new Vector3((NewProperty.Offset - 0.5f * NewProperty.Width), 0.01f, 1);//有问题
            CurrentSkylight.RightCube.transform.position = LiftPosition + Vector3.right * (Length - (0.5f * (Length - (NewProperty.Offset + 0.5f * NewProperty.Width))));
            CurrentSkylight.RightCube.transform.localScale = new Vector3((Length - (NewProperty.Offset + 0.5f * NewProperty.Width)), 0.01f, 1);

            CurrentSkylight.LiftCube1.transform.position = LiftPosition1 + Vector3.right * 0.5f * (NewProperty.Offset - 0.5f * NewProperty.Width);
            CurrentSkylight.LiftCube1.transform.localScale = new Vector3((NewProperty.Offset - 0.5f * NewProperty.Width), 0.01f, 1);
            CurrentSkylight.RightCube1.transform.position = LiftPosition1 + Vector3.right * (Length - (0.5f * (Length - (NewProperty.Offset + 0.5f * NewProperty.Width))));
            CurrentSkylight.RightCube1.transform.localScale = new Vector3((Length - (NewProperty.Offset + 0.5f * NewProperty.Width)), 0.01f, 1);

        }


        //更新面板属性
        public void UpdateSkylightProperty()
        {
            //面板属性改变
            CurrentSkylightProperty.Width = (CurrentSkylight.RightCube.transform.position.x - 0.5f * CurrentSkylight.RightCube.transform.localScale.x) - (CurrentSkylight.LiftCube.transform.position.x + 0.5f * CurrentSkylight.LiftCube.transform.localScale.x);
            CurrentSkylightProperty.Height = 1.0f;
            CurrentSkylightProperty.Offset = CurrentSkylight.LiftCube.transform.localScale.x + 0.5f * CurrentSkylightProperty.Width;
            CurrentSkylightProperty.GroundHeight = CurrentSkylight.LiftCube.transform.position.y - (-3.11f);
    
            //更新面板上的属性值
            CanvasMenuManager.UpdateSkylightPropertyValue(CurrentSkylightProperty);
        }



        //切换天窗
        private void SwitchSkylight(SkylightRef TargetSkylight)
        {
            //相关物体的激活
            CurrentSkylight.Tile.SetActive(true);
            CurrentSkylight.Tile1.SetActive(true);
            CurrentSkylight.LightSolution.SetActive(false);

            CurrentSkylight = TargetSkylight;
            CurrentSkylight.Tile.SetActive(false);
            CurrentSkylight.Tile1.SetActive(false);
            CurrentSkylight.LightSolution.SetActive(true);

            //更新面板上的属性值
            UpdateSkylightProperty();
        }

    }
}

