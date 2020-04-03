#include "user_data.h"

/*
64KBytes Flash最后1KByte用于数据储存
1KByte分为两部分：RGB变化数据，其它数据
RGB变化数据：分为12组，对应时间的6个数字和日期的6个数字
*/

#define BASE_ADDRESS    0x0800FC00      //数据页基础地址

Data_Page data;

/*
函数说明：获取数据页数据
*/
void Get_Data()
{
  for(int i=0;i<12;i++)
  {
    uint32_t offset_address=BASE_ADDRESS+(6*13+2)*i;
    data.RGB[i].point_count=*(uint16_t*)(Points_Count_Limit*6+offset_address);
    for(int j=0;j<data.RGB[i].point_count;j++)
    {
      data.RGB[i].points[j].B=*(uint8_t*)(offset_address+j*6);
      data.RGB[i].points[j].G=*(uint8_t*)(offset_address+1+j*6);
      data.RGB[i].points[j].R=*(uint8_t*)(offset_address+2+j*6);
      data.RGB[i].points[j].Fun=*(uint8_t*)(offset_address+3+j*6);
      data.RGB[i].points[j].X=*(uint16_t*)(offset_address+4+j*6);
    }
  }
  data.others[0]=*(uint8_t*)(BASE_ADDRESS+(6*13+2)*12+0);
  data.others[1]=*(uint8_t*)(BASE_ADDRESS+(6*13+2)*12+1);
  data.others[2]=*(uint8_t*)(BASE_ADDRESS+(6*13+2)*12+2);
  data.others[3]=*(uint8_t*)(BASE_ADDRESS+(6*13+2)*12+3);
  
}

/*
函数说明：储存数据
*/
HAL_StatusTypeDef Set_Data()
{
  HAL_StatusTypeDef status=HAL_OK;
  FLASH_EraseInitTypeDef f;
  f.TypeErase=FLASH_TYPEERASE_PAGES;
  f.PageAddress=BASE_ADDRESS;
  f.NbPages=(f.PageAddress-0x08000000)/1024;
  uint32_t PageError=0;
  status=HAL_FLASH_Unlock();
  status=HAL_FLASHEx_Erase(&f,&PageError);
  uint32_t buf;
  for(int i=0;i<12;i++)
  {
    for(int j=0;j<data.RGB[i].point_count;j++)
    {
      buf=(data.RGB[i].points[j].Fun<<24)+(data.RGB[i].points[j].R<<16)+(data.RGB[i].points[j].G<<8)+(data.RGB[i].points[j].B<<0);
      status=HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,BASE_ADDRESS+i*(2+6*13)+j*6,buf);
      status=HAL_FLASH_Program(FLASH_TYPEPROGRAM_HALFWORD,BASE_ADDRESS+i*(2+6*13)+j*6+4,data.RGB[i].points[j].X);
    }
    status=HAL_FLASH_Program(FLASH_TYPEPROGRAM_HALFWORD,BASE_ADDRESS+i*(2+6*13)+Points_Count_Limit*6,data.RGB[i].point_count);
  }
  status=HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,BASE_ADDRESS+(6*13+2)*12,data.others[0]<<0|data.others[1]<<8|data.others[2]<<16|data.others[3]<<24);
  status=HAL_FLASH_Lock();
  return status;
}

extern uint8_t count;
/*
函数说明：根据RGB变化函数计算指定时刻RGB值
*/
uint32_t Get_Value(Points_Group group,uint16_t t)
{
  uint8_t R;
  uint8_t G;
  uint8_t B;
  uint16_t buf_t=t%group.points[group.point_count-1].X;
  for(int i=0;i<group.point_count-1;i++)
  {
    if(buf_t<=group.points[i+1].X)
    {
      R=(group.points[i].R-group.points[i+1].R)*(buf_t-group.points[i].X)/(group.points[i].X-group.points[i+1].X)+group.points[i].R;
      G=(group.points[i].G-group.points[i+1].G)*(buf_t-group.points[i].X)/(group.points[i].X-group.points[i+1].X)+group.points[i].G;
      B=(group.points[i].B-group.points[i+1].B)*(buf_t-group.points[i].X)/(group.points[i].X-group.points[i+1].X)+group.points[i].B;
      return (R<<16)+(G<<8)+(B<<0);
    }
  }
  return 0xFFFFFF;
}

void Reset_RGB()
{
  for(int i=0;i<12;i++)
  {
    data.RGB[i].point_count=9;
    
    data.RGB[i].points[0].Fun=Linear;
    data.RGB[i].points[0].R = 255;
    data.RGB[i].points[0].G = 0;
    data.RGB[i].points[0].B = 0;
    data.RGB[i].points[0].X = 0/(12-i);
    
    data.RGB[i].points[1].Fun=Linear;
    data.RGB[i].points[1].R = 255;
    data.RGB[i].points[1].G = 165;
    data.RGB[i].points[1].B = 0;
    data.RGB[i].points[1].X = 400/(12-i);
    
    data.RGB[i].points[2].Fun=Linear;
    data.RGB[i].points[2].R = 255;
    data.RGB[i].points[2].G = 255;
    data.RGB[i].points[2].B = 0;
    data.RGB[i].points[2].X = 800/(12-i);
    
    data.RGB[i].points[3].Fun=Linear;
    data.RGB[i].points[3].R = 0;
    data.RGB[i].points[3].G = 255;
    data.RGB[i].points[3].B = 0;
    data.RGB[i].points[3].X = 1200/(12-i);
    
    data.RGB[i].points[4].Fun=Linear;
    data.RGB[i].points[4].R = 0;
    data.RGB[i].points[4].G = 127;
    data.RGB[i].points[4].B = 255;
    data.RGB[i].points[4].X = 1600/(12-i);
    
    data.RGB[i].points[5].Fun=Linear;
    data.RGB[i].points[5].R = 0;
    data.RGB[i].points[5].G = 0;
    data.RGB[i].points[5].B = 255;
    data.RGB[i].points[5].X = 2000/(12-i);
    
    data.RGB[i].points[6].Fun=Linear;
    data.RGB[i].points[6].R = 139;
    data.RGB[i].points[6].G = 0;
    data.RGB[i].points[6].B = 255;
    data.RGB[i].points[6].X = 2400/(12-i);
    
    data.RGB[i].points[7].Fun=Linear;
    data.RGB[i].points[7].R = 255;
    data.RGB[i].points[7].G = 0;
    data.RGB[i].points[7].B = 0;
    data.RGB[i].points[7].X = 2800/(12-i);
    
    data.RGB[i].points[8].Fun=Linear;
    data.RGB[i].points[8].R = 255;
    data.RGB[i].points[8].G = 0;
    data.RGB[i].points[8].B = 0;
    data.RGB[i].points[8].X = 3200/(12-i);
  }
  data.others[0]=0;
  data.others[1]=0;
  data.others[2]=0;
  data.others[3]=0;
  
}
