using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NatureLight
{
    public class IlluminatedPlane : MonoBehaviour
    {

        private Material PlaneMaterial;

        public Vector4[] WindowPositionArray = new Vector4[10];
        public float[] WindowWidthArray = new float[10]; 
        public float[] WindowHeightArray = new float[10];
        public int WindowsCount = 0;

        [Header("等照线相邻采光系数的差")]
        public float Range = 0.05f;


        // Use this for initialization
        void Start()
        {
            PlaneMaterial = this.transform.GetComponent<Renderer>().material;

        }

        // Update is called once per frame
        void Update()
        {
            PlaneMaterial.SetVectorArray("_WindowPositionArray", WindowPositionArray);
            PlaneMaterial.SetFloatArray("_WindowWidthArray", WindowWidthArray);
            PlaneMaterial.SetFloatArray("_WindowHeightArray", WindowHeightArray);
            PlaneMaterial.SetInt("_WindowsCount", WindowsCount);
            PlaneMaterial.SetFloat("_Range", Range);


            //for(int i= 0;i<WindowsCount;i++ )
            //{
            //    float Factor = 0.0f;
            //    Vector3 PointPosition = new Vector3(WindowPositionArray[i].x, transform.position.y, WindowPositionArray[i].z);

            //    for()
            //    Factor = GetIllumination(PointPosition, WindowPositionArray[i], WindowWidthArray[i], WindowHeightArray[i]);
            //    if ((Mathf.Floor(Factor * 1000) % (Range * 1000)) / 1000.0 == 0)
            //    {

            //    }
            //    else
            //    {

            //    }
      
            //}



        }


        //传参数来设置侧窗

        void SetWindowPara(List<WindowParameterStruct> newWindowsPara)
        {
            if(newWindowsPara.Count<10)
            {
                WindowsCount = newWindowsPara.Count;
                for (int i = 0; i < newWindowsPara.Count; i++)
                {
                    WindowPositionArray[i] = newWindowsPara[i].Position;
                    WindowWidthArray[i] = newWindowsPara[i].Width;
                    WindowHeightArray[i] = newWindowsPara[i].Height;
                }
            }
            else
            {
                Debug.Log("The Count of Windows should not beyond 10");
            }

        }


        //采光系数的计算
        float GetIllumination(Vector3 PointPosition, Vector3 WindowPosition, float WindowWidth, float WindowHeight)
        {

            float ElevationAngle_1 = Mathf.Atan(((WindowPosition.y + 0.5f * WindowHeight) - PointPosition.y) / (WindowPosition.z - PointPosition.z));

            float ElevationAngle_2 = Mathf.Atan(((WindowPosition.y - 0.5f * WindowHeight) - PointPosition.y) / (WindowPosition.z - PointPosition.z));

            float DirectionAngle_1 = Mathf.Atan(((WindowPosition.x + 0.5f * WindowWidth) - PointPosition.x) / (WindowPosition.z - PointPosition.z));

            float DirectionAngle_2 = Mathf.Atan(((WindowPosition.x - 0.5f * WindowWidth) - PointPosition.x) / (WindowPosition.z - PointPosition.z));

            float Sub = 4.0f * Mathf.Pow(Mathf.Sin(ElevationAngle_2), 3) - 3.0f * Mathf.Pow(Mathf.Cos(ElevationAngle_2), 2) - 4.0f * Mathf.Pow(Mathf.Sin(ElevationAngle_1), 3) + 3.0f * Mathf.Pow(Mathf.Cos(ElevationAngle_1), 2);
            //采光系数
            float DaylightFactor = (DirectionAngle_2 - DirectionAngle_1) * Sub / (14.0f * 3.1416f);

            //这个点的照度
            return DaylightFactor;
        }


    }
}
