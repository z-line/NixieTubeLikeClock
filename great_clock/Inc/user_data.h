#include "main.h"
#define Points_Count_Limit      13

typedef enum
{
    Linear,
    sin,
    cos
}Function;

typedef struct
{
    uint8_t Fun;
    uint8_t R;
    uint8_t G;
    uint8_t B;
    uint16_t X;
}Inflection_Point;      //RGB�仯���߹յ����͡�Size:6 Bytes

typedef struct
{
  
    Inflection_Point points[Points_Count_Limit];
    uint16_t point_count;
}Points_Group;          //RGB�仯�������͡�Siz:2+6*13 Bytes

typedef struct
{
    Points_Group RGB[12];       //RGB�仯����
    uint8_t others[64];         //��������
}Data_Page;             //�û�����ҳ���͡�

typedef enum
{
    Hour_High,          //ʱ��λ��ʮλ
    Hour_Low,           //ʱ��λ����λ
    Minute_High,        //�ָ�λ��ʮλ
    Minute_Low,         //�ֵ�λ����λ
    Second_High,        //���λ��ʮλ
    Second_Low,         //���λ����λ
    Year_High,          //���λ��ʮλ
    Year_Low,           //���λ����λ
    Month_High,         //�¸�λ��ʮλ
    Month_Low,          //�µ�λ����λ
    Date_High,          //�ո�λ��ʮλ
    Date_Low            //�յ�λ����λ
}Group_Index;

extern Data_Page data;

void Get_Data(void);
HAL_StatusTypeDef Set_Data(void);
uint32_t Get_Value(Points_Group group,uint16_t t);
void Reset_RGB(void);
