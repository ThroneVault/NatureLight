using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NatureLight
{
    public class MenuManager : MonoBehaviour
    {

        public enum InputForm
        {
            InputField,Slider
        }

        [Header("菜单内的引用")]
        [Header("侧窗的属性")]
        public GameObject SideWindowWidth;
        public GameObject SideWindowHeight;
        public GameObject SideWindowGroundHeight;

        [Header("天窗的属性")]
        public GameObject SkylightWidth;
        public GameObject SkylightHeight;
        public GameObject SkylightOffset;
        public GameObject SkylightGroundHeight;


        [Header("菜单外的引用")]
        [Tooltip("侧窗窗户")]
        public GameObject WindowManager;

        [Tooltip("天窗窗户")]
        public GameObject SkylightManager;

        [Tooltip("光照面")]
        public GameObject IlluminatedPlane;

        //面板的属性值
        private SkylightProperty DisplayProperty = new SkylightProperty();

        //侧窗面板的属性值
        private WindowProperty DisplayWindowProperty = new WindowProperty();




        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        //面板调用——改变天窗的形式
        public void SwitchSkylightForm(int Form)
        {
            SkylightManager.GetComponent<SkylightManager>().SwitchSkylightForm(Form);
        }

        //面板调用——用新的面板属性 设置天窗
        public void SetSkylightPorperty(SkylightProperty NewProperty)
        {
            SkylightManager.GetComponent<SkylightManager>().SetSkylightPorperty(NewProperty);
        }

        //天窗调用——更新面板上的属性值
        public void UpdateSkylightPropertyValue(SkylightProperty NewProperty)
        {
            DisplayProperty = NewProperty;
            SetValue(SkylightWidth, NewProperty.Width);
            SetValue(SkylightHeight, NewProperty.Height);
            SetValue(SkylightOffset, NewProperty.Offset);
            SetValue(SkylightGroundHeight, NewProperty.GroundHeight);
        }

        private void SetValue(GameObject Property , float Value)
        {
            Property.GetComponentInChildren<InputField>().text = (Mathf.Floor(100 * Value)).ToString();
            Property.GetComponentInChildren<Slider>().value = 100*Value;
        }
  

        //面板调用——用户输入天窗的属性值
        public void InputPropertyValue()
        {
            float NewValue = 0;
           // SkylightProperty NewProperty = DisplayProperty;
            NewValue = float.Parse(SkylightWidth.GetComponentInChildren<InputField>().text);
            SkylightWidth.GetComponentInChildren<Slider>().value = NewValue;
            DisplayProperty.Width = NewValue / 100;
            SetSkylightPorperty(DisplayProperty);

        }

        public void InputPropertySliderValue()
        {
            float NewValue = 0;
           // SkylightProperty NewProperty = DisplayProperty;
            NewValue = SkylightWidth.GetComponentInChildren<Slider>().value;
            SkylightWidth.GetComponentInChildren<InputField>().text = NewValue.ToString();
            DisplayProperty.Width = NewValue / 100;
            SetSkylightPorperty(DisplayProperty);

        }


        public void InputPropertyValueOffset()
        {
            float NewValue = 0;
          //  SkylightProperty NewProperty = DisplayProperty;
            NewValue = float.Parse(SkylightOffset.GetComponentInChildren<InputField>().text);
            SkylightOffset.GetComponentInChildren<Slider>().value = NewValue;
            DisplayProperty.Offset = NewValue / 100;
            SetSkylightPorperty(DisplayProperty);

        }

        public void InputPropertySliderValueOffset()
        {
            float NewValue = 0;
            //SkylightProperty NewProperty = DisplayProperty;
            NewValue = SkylightOffset.GetComponentInChildren<Slider>().value;
            SkylightOffset.GetComponentInChildren<InputField>().text = NewValue.ToString();
            DisplayProperty.Offset = NewValue / 100;
            SetSkylightPorperty(DisplayProperty);
        }





        //面板调用——改变侧窗的形式
        public void SwitchWindowForm(int Form)
        {
            WindowManager.GetComponent<WindowManager>().SwitchWindowForm(Form);
        }

        //面板调用——用新的面板属性 设置侧窗
        public void SetWindowPorperty(WindowProperty NewProperty)
        {
            WindowManager.GetComponent<WindowManager>().SetWindowPorperty(NewProperty);
        }

        //侧窗调用——更新面板上的属性值
        public void UpdateWindowPropertyValue(WindowProperty NewProperty)
        {
            DisplayWindowProperty = NewProperty;
            SetValue(SideWindowWidth, NewProperty.Width);
            SetValue(SideWindowHeight, NewProperty.Height);
            SetValue(SideWindowGroundHeight, NewProperty.GroundHeight);
        }

        //面板调用——用户输入天窗的属性值
        public void InputWindowPropertyValue()
        {
            float NewValue = 0;
            // SkylightProperty NewProperty = DisplayProperty;
            NewValue = float.Parse(SkylightWidth.GetComponentInChildren<InputField>().text);
            SkylightWidth.GetComponentInChildren<Slider>().value = NewValue;
            DisplayProperty.Width = NewValue / 100;
            SetSkylightPorperty(DisplayProperty);

        }



        //面板调用——用户输入天窗的属性值
        public void InputWindowWidthValue()
        {
            float NewValue = 0;
            // SkylightProperty NewProperty = DisplayProperty;
            NewValue = float.Parse(SideWindowWidth.GetComponentInChildren<InputField>().text);
            SideWindowWidth.GetComponentInChildren<Slider>().value = NewValue;
            DisplayWindowProperty.Width = NewValue / 100;
            SetWindowPorperty(DisplayWindowProperty);

        }

        //面板调用——侧窗——滑条
        public void InputWindowWidthSliderValue()
        {
            float NewValue = 0;
            // SkylightProperty NewProperty = DisplayProperty;
            NewValue = SideWindowWidth.GetComponentInChildren<Slider>().value;
            SideWindowWidth.GetComponentInChildren<InputField>().text = NewValue.ToString();
            DisplayWindowProperty.Width = NewValue / 100;
            SetWindowPorperty(DisplayWindowProperty);

        }


        public void InputWindowHeightValue()
        {
            float NewValue = 0;
            // SkylightProperty NewProperty = DisplayProperty;
            NewValue = float.Parse(SideWindowHeight.GetComponentInChildren<InputField>().text);
            SideWindowHeight.GetComponentInChildren<Slider>().value = NewValue;
            DisplayWindowProperty.Height = NewValue / 100;
            SetWindowPorperty(DisplayWindowProperty);

        }

        //面板调用——滑条——
        public void InputWindowHeightSliderValue()
        {
            float NewValue = 0;
            //SkylightProperty NewProperty = DisplayProperty;
            NewValue = SideWindowHeight.GetComponentInChildren<Slider>().value;
            SideWindowHeight.GetComponentInChildren<InputField>().text = NewValue.ToString();
            DisplayWindowProperty.Height = NewValue / 100;
            SetWindowPorperty(DisplayWindowProperty);
        }

        //面板调用——输入——GroundHeight
        public void InputWindowGHeightValue()
        {
            float NewValue = 0;
            // SkylightProperty NewProperty = DisplayProperty;
            NewValue = float.Parse(SideWindowGroundHeight.GetComponentInChildren<InputField>().text);
            SideWindowGroundHeight.GetComponentInChildren<Slider>().value = NewValue;
            DisplayWindowProperty.GroundHeight = NewValue / 100;
            SetWindowPorperty(DisplayWindowProperty);

        }

        //面板调用——滑条——GroundHeight
        public void InputWindowGHeightSliderValue()
        {
            float NewValue = 0;
            //SkylightProperty NewProperty = DisplayProperty;
            NewValue = SideWindowGroundHeight.GetComponentInChildren<Slider>().value;
            SideWindowGroundHeight.GetComponentInChildren<InputField>().text = NewValue.ToString();
            DisplayWindowProperty.GroundHeight = NewValue / 100;
            SetWindowPorperty(DisplayWindowProperty);
        }

    }
}

