#include "main.h"

typedef struct
{
    GPIO_TypeDef*       Port;                   //�������ڶ˿���
    uint16_t            Pin;                    //��������
    uint8_t             Key_Down;               //�������±�־
    uint32_t            Key_Down_Time;          //��������ʱ��uint:ms
    uint32_t            Last_Click_Span;        //���ϴε���ʱ��uint:ms
    uint8_t             Double_Click_Wait;      //˫���жϵȴ���־
}Key_TypeDef;

typedef enum
{
    Normal_Press,       //����
    Long_Press,         //����
    Double_Click        //˫��
}Press_Type;            //������������

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
