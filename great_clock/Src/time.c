#include "time.h"
#include "ws2812b.h"
#include "user_data.h"
/*
ʱ�����ʼ1970��1��1��0ʱ0��0�� ������
���꣺4�ı����Ҳ���100�ı�����400�ı���
����366�죬ƽ����365��
����31�죬������1��3��5��7��8��10��12
С��30�죬������4��6��9��11
������2�£�����29�죬ƽ����28��
*/

DateTime_TypeDef set_datetime;
DateTime_TypeDef current_datetime;
//****************************************************����Ϊʱ����㲿��*************************************************
/*

4��һ���ڣ�100��һ������400���ٽ���

����˵����������ȥ��������ʱ��
������year_offset  ��ʼ���
������timestamp    ��ȥ����
���أ���ȥ������Ӧʱ��
*/
DateTime_TypeDef PastSeconds2Time(uint32_t start_year,uint64_t past_seconds)
{
    DateTime_TypeDef date_time = {0, 0, 0, 0};
    date_time.second = past_seconds % 60u;
    past_seconds /= 60u;
    date_time.minute = past_seconds % 60u;
    past_seconds /= 60u;
    date_time.hour = past_seconds % 24u;
    past_seconds /= 24u;
    date_time.days = past_seconds + 1;
    long days = date_time.days;
    date_time.year = days / 365;
    days %= 365;
    //��ݽ���
    for (int i = 0; i < date_time.year; i++)
    {
        if (((start_year + i) % 4 == 0 && (start_year + i) % 100 != 0) //4�ı����Ҳ���100�ı���������
            || (start_year + i) % 400 == 0)                             //400�ı���������
        {
            if (--days <= 0)
            {
                date_time.year--;
                days = date_time.days - date_time.year * 365;
                i = 0;
            }
        }
    }
    //�����·�
    for (int i = 1; i <= 12; i++)
    {
        date_time.month = i;
        if (i == 4 || i == 6 || i == 9 || i == 11)
        {
            if (days - 30 > 0)
            {
                days -= 30;
            }
            else
            {
                break;
            }
        }
        else if (i == 2)
        {
            //�ж�����
            if (((start_year + date_time.year) % 4 == 0 && (start_year + date_time.year) % 100 != 0) //4�ı����Ҳ���100�ı���������
                || (start_year + date_time.year) % 400 == 0)                                     //400�ı���������
            {
                if (days - 29 > 0)
                {
                    days -= 29;
                }
                else
                {
                    break;
                }
            }
            else
            {
                if (days - 28 > 0)
                {
                    days -= 28;
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            if (days - 31 > 0)
            {
                days -= 31;
            }
            else
            {
                break;
            }
        }
    }
    //��������
    date_time.date = days;
    //��ݼ���ƫ��
    date_time.year += start_year;
    return date_time;
}

/*
����˵������ȡ��ȥ������
������year_offset  ��ʼ���
������date_time    ��ֹʱ��
���أ�ʱ�����ڶ�Ӧʱ���
*/
uint64_t Time2PastSeconds(uint32_t start_year, DateTime_TypeDef date_time)
{
    uint64_t timestamp = 0;
    uint64_t day=0;
    //��������
    day = (date_time.year - start_year) * 365;
    for (int i = start_year; i < date_time.year; i++)
    {
        if ((i % 4 == 0 && i % 100 != 0) || i % 400 == 0)
        {
            day++;
        }
    }
    //����ʣ������
    for (int i = 1; i < date_time.month; i++)
    {
        if (i == 4 || i == 6 || i == 9 || i == 11)
        {
            day += 30;
        }
        else if (i == 2)
        {
            if ((date_time.year % 4 == 0 && date_time.year % 100 != 0) || date_time.year % 400 == 0)
            {
                day += 29;
            }
            else
            {
                day += 28;
            }
        }
        else
        {
            day += 31;
        }
    }
    //����ʣ������
    day += (date_time.date - 1);
    timestamp = day * 24 * 60 * 60 + date_time.hour * 60 * 60 + date_time.minute * 60 + date_time.second - TIMEZONE * 60 * 60;
    return timestamp;
}

extern RTC_HandleTypeDef hrtc;
extern uint32_t RTC_ReadTimeCounter(RTC_HandleTypeDef *hrtc);
extern HAL_StatusTypeDef RTC_WriteTimeCounter(RTC_HandleTypeDef *hrtc, uint32_t TimeCounter);

/*
����˵������ȡʱ����ۼƴ�����ÿ��Ϊ100��
*/
uint16_t Get_Accumulative_Times(void)
{
    uint16_t times=HAL_RTCEx_BKUPRead(&hrtc,TIME_ADD_COUNT_REG);
    return times;
}

/*
����˵��������ʱ����ۼƴ�����ÿ��Ϊ100��
*/
void Set_Accumulative_Times(uint16_t times)
{
    HAL_RTCEx_BKUPWrite(&hrtc,TIME_ADD_COUNT_REG,times);
}

/*
����˵������ȡRTCʱ��
*/
DateTime_TypeDef Get_DateTime(void)
{
    uint32_t counter=RTC_ReadTimeCounter(&hrtc);
    uint32_t year_offset=YEAR_OFFSET+100*Get_Accumulative_Times();
    return PastSeconds2Time(year_offset,counter+TIMEZONE*3600);
}

/*
����˵��������RTCʱ��
*/
void Set_DateTime(DateTime_TypeDef datetime)
{
    uint16_t accumulative_times=(datetime.year-YEAR_OFFSET)/100u;
    Set_Accumulative_Times(accumulative_times);
    RTC_WriteTimeCounter(&hrtc,Time2PastSeconds(YEAR_OFFSET+accumulative_times*100u,datetime));
}

/*
����˵����ʱ��ת�ַ���
��׼��ʽ��"2019 1 1 0 12 00 00"����"�� �� �� ���� ʱ �� ��"
*/
void Time2String(DateTime_TypeDef input_datetime,char* out_string)
{
    //out_string[0]='\0';
    sprintf(out_string,"%d %d %d %d %d %d %d\n",input_datetime.year,input_datetime.month,input_datetime.date,
            input_datetime.weekday,input_datetime.hour,input_datetime.minute,input_datetime.second);
}

/*
����˵�����ַ���תʱ��
��׼��ʽ��"2019 1 1 0 12 00 00"����"�� �� �� ���� ʱ �� ��"
*/
void String2Time(char* input,DateTime_TypeDef* out_datetime)
{
    int i=0,last_delimiter_pos=0,pos=0;
    const char delimiter=' ';
    uint32_t buf=0;
    while(1){
        if(input[i]==delimiter||input[i]=='\0'){
            for(int j=last_delimiter_pos;j<i;j++){
                buf=buf*10+(input[j]-0x30);
            }
            switch(pos){
              case 0:
                out_datetime->year=buf;
                break;
              case 1:
                out_datetime->month=buf;
                break;
              case 2:
                out_datetime->date=buf;
                break;
              case 3:
                out_datetime->weekday=buf;
                break;
              case 4:
                out_datetime->hour=buf;
                break;
              case 5:
                out_datetime->minute=buf;
                break;
              case 6:
                out_datetime->second=buf;
                break;
            }
            if(input[i]=='\0')
            {
                break;
            }
            pos++;
            buf=0;
            last_delimiter_pos=i+1;
        }
        i++;
    }
}

//*****************************************����Ϊʱ����ʾ����********************************************
extern uint16_t count;

/*
����˵������ʾʱ��
*/
void show_time()
{
    Load_Num(5,current_datetime.second%10,Get_Value(data.RGB[Second_Low],count));
    Load_Num(4,current_datetime.second/10,Get_Value(data.RGB[Second_High],count));
    Load_Num(3,current_datetime.minute%10,Get_Value(data.RGB[Minute_Low],count));
    Load_Num(2,current_datetime.minute/10,Get_Value(data.RGB[Minute_High],count));
    Load_Num(1,current_datetime.hour%10,Get_Value(data.RGB[Hour_Low],count));
    Load_Num(0,current_datetime.hour/10,Get_Value(data.RGB[Hour_High],count));
    Show_RGB();
}

/*
����˵������ʾ����
*/
void show_date()
{
    Load_Num(5,current_datetime.date%10,Get_Value(data.RGB[Date_Low],count));
    Load_Num(4,current_datetime.date/10,Get_Value(data.RGB[Date_High],count));
    Load_Num(3,current_datetime.month%10,Get_Value(data.RGB[Month_Low],count));
    Load_Num(2,current_datetime.month/10,Get_Value(data.RGB[Month_High],count));
    Load_Num(1,current_datetime.year%10,Get_Value(data.RGB[Year_Low],count));
    Load_Num(0,current_datetime.year%100/10,Get_Value(data.RGB[Year_High],count));
    Show_RGB();
}

/*
����˵������ʾ������
*/
void show_weekday()
{

}

//****************************************����ΪRGBЧ������****************************************
uint32_t RGB_POS[6]={0,0,0,0,0,0};        //��Ӧ����λ����RGBֵ
/*
����˵������ȡflash��ָ��λ��rgbֵ
*/
uint32_t Get_RGB(uint8_t pos)
{
    return *(__IO uint32_t*)(0x0800FC00+pos*4);
}

/*
����˵��������flashָ��λ��rgbֵ
*/
void Set_RGB(uint32_t rgb[6])
{
    FLASH_EraseInitTypeDef f;
    f.TypeErase=FLASH_TYPEERASE_PAGES;
    f.PageAddress=0x0800FC00;
    f.NbPages=63;
    uint32_t PageError=0;
    HAL_FLASH_Unlock();
    HAL_FLASHEx_Erase(&f,&PageError);
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,f.PageAddress+0*4,rgb[0]);
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,f.PageAddress+1*4,rgb[1]);
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,f.PageAddress+2*4,rgb[2]);
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,f.PageAddress+3*4,rgb[3]);
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,f.PageAddress+4*4,rgb[4]);
    HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD,f.PageAddress+5*4,rgb[5]);
    HAL_FLASH_Lock();
}
