#include "ws2812b.h"
#include "main.h"

#define Logic_Zero              21u     //逻辑“0”对应占空比
#define Logic_One               57u     //逻辑“1”对应占空比
#define Buffer_LED_Count        120     //缓存多少个灯的数据

#define LED_Count_Per_Num       2       //每个数字点亮所用LED数量

#if(LED_Count_Per_Num==2)
#define Right_Index(Left_Index) Left_Index+10   //若每个数字由两个LED点亮，则有左右侧LED索引换算关系
#endif

extern TIM_HandleTypeDef htim3;

uint8_t Data_DMA2Timer[24*Buffer_LED_Count];  //DMA缓存一次         TODO:预留多个零占空比，清除残余波形（效果待测）
uint8_t Light_Once=0;                   //点灯标识
uint32_t RGB[Buffer_LED_Count]={
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0,
  0,0,0,0,0,0
};


//通过需要显示数字的位置及值获取数据缓存中对应单个索引
uint16_t Get_Index_By_Num_Pos(uint8_t pos,uint8_t num)
{
  //偏移量pos*10*LED_Count_Per_Num
  //偶数偏移为对应值除以2，奇数偏移为对应值除以2加5
  uint16_t buf=pos*10*LED_Count_Per_Num;      //加入位置偏移
  uint8_t new_num=num%10;
  buf+=((new_num%2)?new_num/2+5:new_num/2);   //加入数字偏移
  return buf;
}

void Show_RGB()
{
  HAL_TIM_PWM_Start_DMA(&htim3,TIM_CHANNEL_1,(uint32_t*)Data_DMA2Timer,24*Buffer_LED_Count);
}

void Clear_RGB()
{
  for(uint32_t i=0;i<Buffer_LED_Count;i++){
    RGB[i]=0x000000;
  }
  LoadDataIntoBuffer();
}

void Load_Num(uint8_t pos,uint8_t num,uint32_t rgb)
{
  for(uint32_t i=pos*10*LED_Count_Per_Num;i<LED_Count_Per_Num*10*(pos+1);i++){
    RGB[i]=0x000000;
  }
  if(num<=9)
  {
    RGB[Get_Index_By_Num_Pos(pos,9-num)]=rgb;
#if(LED_Count_Per_Num==2)
    RGB[Right_Index(Get_Index_By_Num_Pos(pos,9-num))]=rgb;
#endif
  }
  LoadDataIntoBuffer();
}

//将RGB数据转换为ws2812格式数据，最终转换为PWM占空比
void RGB_To_Data(uint32_t rgb_color,uint8_t* data)
{
  for(int i=0;i<8;i++){
    data[i]=(rgb_color>>15-i&0x01)?Logic_One:Logic_Zero;
  }
  for(int i=8;i<16;i++){
    data[i]=(rgb_color>>31-i&0x01)?Logic_One:Logic_Zero;
  }
  for(int i=16;i<24;i++){
    data[i]=(rgb_color>>23-i&0x01)?Logic_One:Logic_Zero;
  }
}

//更新一帧的RGB数据
void LoadDataIntoBuffer(void)
{
  for(int i=0;i<Buffer_LED_Count;i++){
    RGB_To_Data(RGB[i],Data_DMA2Timer+i*24);
  }
}
