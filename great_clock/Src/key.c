#include "key.h"
#include "time.h"

#define LONG_DOWN_TIME 400u     //����ʱ��uint:ms
#define DOUBLE_CLICK_SPAN 200u  //˫��ʱ����uint:ms

/*
������Ӧ����
GPIO_PIN_13-Next;
GPIO_PIN_14-Menu;
GPIO_PIN_15-Previous;
*/

Key_TypeDef keys[3]={
    {Key_Next_GPIO_Port,Key_Next_Pin,0,0,0,0},          //����һ��������
    {Key_Menu_GPIO_Port,Key_Menu_Pin,0,0,0,0},          //���˵�������
    {Key_Previous_GPIO_Port,Key_Previous_Pin,0,0,0,0}}; //����һ��������

Operating_Mode Global_Mode=DisplayMode; //��ǰģʽ
//Display Mode
uint8_t display_time_or_date=0; //0:time;1:date
//Setting Mode
uint8_t setting_time_or_date=0; //0:time;1:date
uint8_t setting_edit_position=0;
uint32_t setting_select_glitter_count=0;

void Key_Next_Event(Press_Type way);
void Key_Menu_Event(Press_Type way);
void Key_Previous_Event(Press_Type way);

//�����¼���Ӧ
void Key_Event(Press_Type way,Key_TypeDef* key)
{
    if(key->Pin==Key_Next_Pin)
    {
        Key_Next_Event(way);
    }
    else if(key->Pin==Key_Menu_Pin)
    {
        Key_Menu_Event(way);
    }
    else if(key->Pin==Key_Previous_Pin)
    {
        Key_Previous_Event(way);
    }
}

//����һ����������Ӧ
void Key_Next_Event(Press_Type way)
{
    switch(way)
    {
      case Normal_Press:
        switch(Global_Mode)
        {
          case DisplayMode:
            break;
          case SettingMode:
            if(setting_time_or_date)
            {
                switch(setting_edit_position)
                {
                  case 0:
                    break;
                  case 1:
                    (set_datetime.month+1<=12)?(set_datetime.month++):(set_datetime.month=1);
                    break;
                  case 2:
                    (set_datetime.date+1<=31)?(set_datetime.date++):(set_datetime.date=1);
                    break;
                }
            }
            else
            {
                switch(setting_edit_position)
                {
                  case 0:
                    (set_datetime.hour+1<=23)?(set_datetime.hour++):(set_datetime.hour=0);
                    break;
                  case 1:
                    (set_datetime.minute+1<=59)?(set_datetime.minute++):(set_datetime.minute=0);
                    break;
                  case 2:
                    (set_datetime.second+1<=59)?(set_datetime.second++):(set_datetime.second=0);
                    break;
                }
            }
            break;
          case CountMode:
            break;
        }
        //SEGGER_RTT_printf(0,"Next:Normal\n");
        break;
      case Long_Press:

        //SEGGER_RTT_printf(0,"Next:Long\n");
        break;
      case Double_Click:
        switch(Global_Mode)
        {
          case DisplayMode:
            break;
          case SettingMode:
            if(setting_time_or_date)
            {
                switch(setting_edit_position)
                {
                  case 0:
                    break;
                  case 1:
                    (set_datetime.month+5<=12)?(set_datetime.month+=5):(set_datetime.month+=(5-12));
                    break;
                  case 2:
                    (set_datetime.date+5<=31)?(set_datetime.date+=5):(set_datetime.date+=(5-31));
                    break;
                }
            }
            else
            {
                switch(setting_edit_position)
                {
                  case 0:
                    (set_datetime.hour+5<=24)?(set_datetime.hour+=5):(set_datetime.hour+=(5-24));
                    break;
                  case 1:
                    (set_datetime.minute+5<=59)?(set_datetime.minute+=5):(set_datetime.minute+=(5-59));
                    break;
                  case 2:
                    (set_datetime.second+5<=59)?(set_datetime.second+=5):(set_datetime.second+=(5-59));
                    break;
                }
            }
            break;
          case CountMode:
            break;
        }
        //SEGGER_RTT_printf(0,"Next:Double\n");
        break;
    }
}

//���˵���������Ӧ
void Key_Menu_Event(Press_Type way)
{
    switch(way)
    {
      case Normal_Press:
        switch(Global_Mode)
        {
          case DisplayMode:
            display_time_or_date=!display_time_or_date;
            break;
          case SettingMode:
            if(setting_edit_position<2)
            {
                setting_edit_position++;
            }
            else
            {
                setting_edit_position=0;
                setting_time_or_date=!setting_time_or_date;
            }
            break;
        }
        //SEGGER_RTT_printf(0,"Menu:Normal\n");
        break;
      case Long_Press:
        if(Global_Mode==SettingMode)
        {
            Set_DateTime(set_datetime);
            setting_time_or_date=0;
            Global_Mode=DisplayMode;
        }
        //SEGGER_RTT_printf(0,"Menu:Long\n");
        break;
      case Double_Click:
        if(Global_Mode==DisplayMode)
        {
            set_datetime=current_datetime;
            Global_Mode=SettingMode;
        }
        else if(Global_Mode==SettingMode)
        {
            Global_Mode=CountMode;
        }
        else
        {
            Global_Mode=DisplayMode;
        }
        //SEGGER_RTT_printf(0,"Menu:Double\n");
        break;
    }
}

//����һ����������Ӧ
void Key_Previous_Event(Press_Type way)
{
    switch(way)
    {
      case Normal_Press:
        switch(Global_Mode)
        {
          case DisplayMode:
            break;
          case SettingMode:
            if(setting_time_or_date)
            {
                switch(setting_edit_position)
                {
                  case 0:
                    break;
                  case 1:
                    (set_datetime.month-1>=1)?(set_datetime.month--):(set_datetime.month+=(12-1));
                    break;
                  case 2:
                    (set_datetime.date-1>=1)?(set_datetime.date--):(set_datetime.date+=(31-1));
                    break;
                }
            }
            else
            {
                switch(setting_edit_position)
                {
                  case 0:
                    (set_datetime.hour-1>=0)?(set_datetime.hour--):(set_datetime.hour=23-set_datetime.hour);
                    break;
                  case 1:
                    (set_datetime.minute-1>=0)?(set_datetime.minute--):(set_datetime.minute=59+1-set_datetime.minute);
                    break;
                  case 2:
                    (set_datetime.second-1>=0)?(set_datetime.second--):(set_datetime.second=59+1-set_datetime.second);
                    break;
                }
            }
            break;
          case CountMode:
            break;
        }
        //SEGGER_RTT_printf(0,"Previous:Normal\n");
        break;
      case Long_Press:
        //SEGGER_RTT_printf(0,"Previous:Long\n");
        break;
      case Double_Click:
        switch(Global_Mode)
        {
          case DisplayMode:
            break;
          case SettingMode:
            if(setting_time_or_date)
            {
                switch(setting_edit_position)
                {
                  case 0:
                    break;
                  case 1:
                    (set_datetime.month-5>=1)?(set_datetime.month-=5):(set_datetime.month+=(12-5));
                    break;
                  case 2:
                    (set_datetime.date-5>=1)?(set_datetime.date-=5):(set_datetime.date+=(31-5));
                    break;
                }
            }
            else
            {
                switch(setting_edit_position)
                {
                  case 0:
                    (set_datetime.hour-5>=0)?(set_datetime.hour-=5):(set_datetime.hour+=(24-5));
                    break;
                  case 1:
                    (set_datetime.minute-5>=0)?(set_datetime.minute-=5):(set_datetime.minute+=(59-5));
                    break;
                  case 2:
                    (set_datetime.second-5>=0)?(set_datetime.second-=5):(set_datetime.second+=(59-5));
                    break;
                }
            }
            break;
          case CountMode:
            break;
        }
        //SEGGER_RTT_printf(0,"Previous:Double\n");
        break;
    }
}

//������Ϊ�ж�
void Key_Method_Judge(Key_TypeDef* key)
{
    if(key->Key_Down==1u)
    {
        if(HAL_GPIO_ReadPin(key->Port,key->Pin)==GPIO_PIN_RESET)
        {
            //����
            if(key->Key_Down_Time<LONG_DOWN_TIME)
            {
                //��ס��ʱ
                key->Key_Down_Time++;
            }
            else
            {
                //��סʱ�����ǳ���Ҫ��
                key->Key_Down=0;
                key->Key_Down_Time=0;
                Key_Event(Long_Press,key);
            }
        }
        else
        {
            key->Key_Down_Time=0;
            key->Key_Down=0;
            key->Double_Click_Wait=1;
            if(key->Last_Click_Span<DOUBLE_CLICK_SPAN&&key->Last_Click_Span>50)
            {
                key->Double_Click_Wait=0;
                key->Last_Click_Span=0;
                Key_Event(Double_Click,key);
            }
        }
    }
    else
    {
        if(key->Double_Click_Wait==1)
        {
            if(key->Last_Click_Span<DOUBLE_CLICK_SPAN)
            {
                //˫���ȴ���ʱ
                key->Last_Click_Span++;
            }
            else
            {
                //˫���ȴ���ʱ
                key->Last_Click_Span=0;
                key->Double_Click_Wait=0;
                Key_Event(Normal_Press,key);
            }
        }
    }
}
