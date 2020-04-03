#include "main.h"

void WS2812B_OneBuffer_Finish(TIM_HandleTypeDef *htim);
void RGB_To_Data(uint32_t rgb_color,uint8_t* data);
void LoadDataIntoBuffer(void);
void Load_Num(uint8_t pos,uint8_t num,uint32_t rgb);
void Show_RGB(void);
void Clear_RGB(void);
