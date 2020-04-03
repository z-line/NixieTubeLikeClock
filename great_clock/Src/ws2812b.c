#include "ws2812b.h"
#include "main.h"

#define Logic_Zero              21u     //�߼���0����Ӧռ�ձ�
#define Logic_One               57u     //�߼���1����Ӧռ�ձ�
#define Buffer_LED_Count        120     //������ٸ��Ƶ�����

#define LED_Count_Per_Num       2       //ÿ�����ֵ�������LED����

#if(LED_Count_Per_Num==2)
#define Right_Index(Left_Index) Left_Index+10   //��ÿ������������LED�������������Ҳ�LED���������ϵ
#endif

extern TIM_HandleTypeDef htim3;

uint8_t Data_DMA2Timer[24*Buffer_LED_Count];  //DMA����һ��         TODO:Ԥ�������ռ�ձȣ�������ನ�Σ�Ч�����⣩
uint8_t Light_Once=0;                   //��Ʊ�ʶ
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


//ͨ����Ҫ��ʾ���ֵ�λ�ü�ֵ��ȡ���ݻ����ж�Ӧ��������
uint16_t Get_Index_By_Num_Pos(uint8_t pos,uint8_t num)
{
  //ƫ����pos*10*LED_Count_Per_Num
  //ż��ƫ��Ϊ��Ӧֵ����2������ƫ��Ϊ��Ӧֵ����2��5
  uint16_t buf=pos*10*LED_Count_Per_Num;      //����λ��ƫ��
  uint8_t new_num=num%10;
  buf+=((new_num%2)?new_num/2+5:new_num/2);   //��������ƫ��
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

//��RGB����ת��Ϊws2812��ʽ���ݣ�����ת��ΪPWMռ�ձ�
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

//����һ֡��RGB����
void LoadDataIntoBuffer(void)
{
  for(int i=0;i<Buffer_LED_Count;i++){
    RGB_To_Data(RGB[i],Data_DMA2Timer+i*24);
  }
}
