#include "main.h"
#include "stdio.h"
//���䱸����Ĵ���
#define TIME_ADD_COUNT_REG      RTC_BKP_DR1     //ʱ����ۼӼ�����32bit RTC Timer����ʱ135�꣬������չ��ʱ��Χ����λ��/���꣩
#define YEAR_OFFSET             1970u
#define TIMEZONE                8               //ʱ��

typedef struct
{
  uint8_t month;
  uint8_t date;
  uint8_t weekday;
  uint8_t hour;
  uint8_t minute;
  uint8_t second;
  uint16_t year;
  uint32_t days;
}DateTime_TypeDef;      //ʱ�����͡�Size:12 Bytes

extern DateTime_TypeDef set_datetime;
extern DateTime_TypeDef current_datetime;

DateTime_TypeDef PastSeconds2Time(uint32_t start_year,uint64_t past_seconds);
uint64_t Time2PastSeconds(uint32_t start_year, DateTime_TypeDef date_time);
void Set_DateTime(DateTime_TypeDef datetime);
DateTime_TypeDef Get_DateTime(void);
void String2Time(char* input,DateTime_TypeDef* out_datetime);
void Time2String(DateTime_TypeDef input_datetime,char* out_string);
void show_time(void);
void show_date(void);
void show_weekday(void);
uint32_t Get_RGB(uint8_t pos);
void Set_RGB(uint32_t rgb[6]);

