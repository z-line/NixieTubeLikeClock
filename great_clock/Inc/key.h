#include "main.h"

typedef struct
{
    GPIO_TypeDef*       Port;                   //按键所在端口组
    uint16_t            Pin;                    //按键引脚
    uint8_t             Key_Down;               //按键按下标志
    uint32_t            Key_Down_Time;          //按键按下时长uint:ms
    uint32_t            Last_Click_Span;        //到上次单击时间uint:ms
    uint8_t             Double_Click_Wait;      //双击判断等待标志
}Key_TypeDef;

typedef enum
{
    Normal_Press,       //单击
    Long_Press,         //长按
    Double_Click        //双击
}Press_Type;            //按键动作类型

typedef enum
{
    DisplayMode,
    SettingMode,
    CountMode
}Operating_Mode;

void Key_Method_Judge(Key_TypeDef* key);

extern Key_TypeDef keys[3];
extern Operating_Mode Global_Mode;
extern uint8_t display_time_or_date;
extern uint8_t setting_time_or_date;
extern uint8_t setting_edit_position;
extern uint32_t setting_select_glitter_count;
