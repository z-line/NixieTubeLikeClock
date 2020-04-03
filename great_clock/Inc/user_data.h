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
}Inflection_Point;      //RGB变化曲线拐点类型。Size:6 Bytes

typedef struct
{
  
    Inflection_Point points[Points_Count_Limit];
    uint16_t point_count;
}Points_Group;          //RGB变化曲线类型。Siz:2+6*13 Bytes

typedef struct
{
    Points_Group RGB[12];       //RGB变化数据
    uint8_t others[64];         //其它数据
}Data_Page;             //用户数据页类型。

typedef enum
{
    Hour_High,          //时高位，十位
    Hour_Low,           //时低位，个位
    Minute_High,        //分高位，十位
    Minute_Low,         //分低位，个位
    Second_High,        //秒高位，十位
    Second_Low,         //秒低位，个位
    Year_High,          //年高位，十位
    Year_Low,           //年低位，个位
    Month_High,         //月高位，十位
    Month_Low,          //月低位，个位
    Date_High,          //日高位，十位
    Date_Low            //日低位，个位
}Group_Index;

extern Data_Page data;

void Get_Data(void);
HAL_StatusTypeDef Set_Data(void);
uint32_t Get_Value(Points_Group group,uint16_t t);
void Reset_RGB(void);
