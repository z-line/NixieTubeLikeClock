#include "commander.h"
#include "usbd_cdc_if.h"
#include "user_data.h"
#include "time.h"

//字符串相等判断
uint8_t equal(uint8_t *a,uint8_t *b,uint32_t count)
{
  for(int i=0;i<count;i++)
  {
    if(a[i]!=b[i])
    {
      return 0;
    }
  }
  return 1;
}

uint16_t cdc_rx_count=0;
extern uint8_t UserRxBufferFS[1000];

void Wait_For_Commander()
{
  while(1)
  {
    if(cdc_rx_count!=0)
    {
      Operate_Commander(UserRxBufferFS,cdc_rx_count);
      cdc_rx_count=0;
    }
  }
}

void Operate_Commander(uint8_t* commander,uint32_t length)
{
  switch(length)
  {
  case 2:
    if(equal(commander,"ID",2))
    {
      //receive:"ID"
      //transmit:"Clock\n"
      CDC_Transmit_FS("Clock\n",6);
    }
    break;
  case 3:
    if(equal(commander,"GTI",3))
    {
      //receive:"GTI"
      //transmit:"year month date weekday hour minute second"
      char a[50];
      Time2String(current_datetime,a);
      CDC_Transmit_FS((uint8_t*)a,strlen(a));
    }
    else if(equal(commander,"GST",3))
    {
      //receive:"GST"
      //transmit:"hour:minute-hour:minute"
      char a[12];
      sprintf(a,"%d:%d-%d:%d\n",data.others[0],data.others[1],data.others[2],data.others[3]);
      CDC_Transmit_FS((uint8_t*)a,strlen(a));
      
    }
    break;
  case 4:
    if(equal(commander,"Save",4))
    {
      if(Set_Data()==HAL_OK)
      {
        CDC_Transmit_FS("Successed\n",10);
      }
      else
      {
        CDC_Transmit_FS("Failed\n",7);
      }
    }
  default:
    if(equal(commander,"STI",3))
    {
      //receive:"STIyear month date weekday hour minute second"
      String2Time((char*)(&commander[3]),&set_datetime);
      Set_DateTime(set_datetime);
    }
    else if(equal(commander,"SCO",3))
    {
      //"SCO group_index point_index Fun-R-G-B-X"
      //"SCO group_index point_count"
      uint8_t group_index=0;
      uint8_t point_index=0;
      if(commander[3]==' '&& commander[5]==' ')
      {
        group_index=commander[4];
        point_index=commander[6];
        if(length==7)
        {
          data.RGB[group_index].point_count=point_index;
        }
        else
        {
          data.RGB[group_index].points[point_index].Fun=commander[8];
          data.RGB[group_index].points[point_index].R=commander[10];
          data.RGB[group_index].points[point_index].G=commander[12];
          data.RGB[group_index].points[point_index].B=commander[14];
          data.RGB[group_index].points[point_index].X=commander[16]+(commander[17]<<8);
        }
      }
    }
    else if(equal(commander,"GCO",3))
    {
      //receive:"GCO group_index point_index"    如果point_index大于最大拐点数，则返回该组的拐点数目，否则返回指定数据
      //transmit:"Fun-R-G-B-X"
      if(commander[3]==' '&&commander[5]==' ')
      {
        uint8_t group_index=commander[4];
        uint8_t point_index=commander[6];
        uint8_t buf[10];
        if(point_index>=Points_Count_Limit)
        {
          buf[0]=data.RGB[group_index].point_count;
          CDC_Transmit_FS(buf,1);
        }
        else
        {
          buf[0]=(uint8_t)data.RGB[group_index].points[point_index].Fun;
          buf[1]='-';
          buf[2]=(uint8_t)data.RGB[group_index].points[point_index].R;
          buf[3]='-';
          buf[4]=(uint8_t)data.RGB[group_index].points[point_index].G;
          buf[5]='-';
          buf[6]=(uint8_t)data.RGB[group_index].points[point_index].B;
          buf[7]='-';
          buf[8]=(uint8_t)(data.RGB[group_index].points[point_index].X&0xff);
          buf[9]=(uint8_t)(data.RGB[group_index].points[point_index].X>>8&0xff);
          CDC_Transmit_FS(buf,10);
        }
      }
    }
    else if(equal(commander,"SST",3))
    {
      //receive:"SSThour:minute-hour:minute"
      int j=0;uint8_t buf=0;
      for(int i=3;i<=length;i++)
      {
        if(commander[i]<=0x39&&commander[i]>=0x30)
        {
          buf=buf*10+(commander[i]-0x30);
        }
        else
        {
          data.others[j]=buf;
          j++;
          buf=0;
        }
      }
    }
  }
}
