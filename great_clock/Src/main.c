/* USER CODE BEGIN Header */
/**
******************************************************************************
* @file           : main.c
* @brief          : Main program body
******************************************************************************
* @attention
*
* <h2><center>&copy; Copyright (c) 2019 STMicroelectronics.
* All rights reserved.</center></h2>
*
* This software component is licensed by ST under BSD 3-Clause license,
* the "License"; You may not use this file except in compliance with the
* License. You may obtain a copy of the License at:
*                        opensource.org/licenses/BSD-3-Clause
*
******************************************************************************
*/
/* USER CODE END Header */

/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "usb_device.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include "time.h"
#include "key.h"
#include "commander.h"
#include "ws2812b.h"
#include "user_data.h"
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */
//#define RTT_DEBUG
/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
RTC_HandleTypeDef hrtc;

TIM_HandleTypeDef htim2;
TIM_HandleTypeDef htim3;
TIM_HandleTypeDef htim4;
DMA_HandleTypeDef hdma_tim3_ch1_trig;

/* USER CODE BEGIN PV */

//Count Mode

//Common

char buf[100];  //缓存字符串

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_RTC_Init(void);
static void MX_DMA_Init(void);
static void MX_TIM3_Init(void);
static void MX_TIM4_Init(void);
static void MX_TIM2_Init(void);
/* USER CODE BEGIN PFP */

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */

//PWM生成完成中断
void HAL_TIM_PWM_PulseFinishedCallback(TIM_HandleTypeDef *htim)
{
  __HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_1,0);
  HAL_TIM_PWM_Stop_DMA(htim,TIM_CHANNEL_1);
}

//RTC秒中断
void HAL_RTCEx_RTCEventCallback(RTC_HandleTypeDef *hrtc)
{
  current_datetime=Get_DateTime();
}

void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin)
{
  for(int i=0;i<3;i++)
  {
    if(GPIO_Pin==keys[i].Pin)
    {
      keys[i].Key_Down=1u;
    }
  }
}


//定时器周期中断
uint16_t count=0;
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
  if(htim==&htim4)
  {
    //1ms
    Key_Method_Judge(&keys[1]);
    Key_Method_Judge(&keys[0]);
    Key_Method_Judge(&keys[2]);
  }
  else if(htim==&htim2)
  {
    //10ms
    switch(Global_Mode)
    {
    case SettingMode:
      if(setting_time_or_date)
      {
        if(setting_select_glitter_count%80>40)
        {
          switch(setting_edit_position)
          {
          case 0:
            Load_Num(1,set_datetime.year%10,0x000000);
            Load_Num(0,set_datetime.year%100/10,0x000000);
            break;
          case 1:
            Load_Num(3,set_datetime.month%10,0x000000);
            Load_Num(2,set_datetime.month/10,0x000000);
            break;
          case 2:
            Load_Num(5,set_datetime.date%10,0x000000);
            Load_Num(4,set_datetime.date/10,0x000000);
            break;
          }
        }
        else
        {
          Load_Num(5,set_datetime.date%10,0xffff00);
          Load_Num(4,set_datetime.date/10,0xffff00);
          Load_Num(3,set_datetime.month%10,0xffff00);
          Load_Num(2,set_datetime.month/10,0xffff00);
          Load_Num(1,set_datetime.year%10,0xffff00);
          Load_Num(0,set_datetime.year%100/10,0xffff00);
        }
        Show_RGB();
      }
      else
      {
        if(setting_select_glitter_count%80>40)
        {
          switch(setting_edit_position)
          {
          case 0:
            Load_Num(1,set_datetime.hour%10,0x000000);
            Load_Num(0,set_datetime.hour/10,0x000000);
            break;
          case 1:
            Load_Num(3,set_datetime.minute%10,0x000000);
            Load_Num(2,set_datetime.minute/10,0x000000);
            break;
          case 2:
            Load_Num(5,set_datetime.second%10,0x000000);
            Load_Num(4,set_datetime.second/10,0x000000);
            break;
          }
        }
        else
        {
          Load_Num(5,set_datetime.second%10,0x00fffff);
          Load_Num(4,set_datetime.second/10,0x00fffff);
          Load_Num(3,set_datetime.minute%10,0x00fffff);
          Load_Num(2,set_datetime.minute/10,0x00fffff);
          Load_Num(1,set_datetime.hour%10,0x00fffff);
          Load_Num(0,set_datetime.hour/10,0x00fffff);
        }
        Show_RGB();
      }
      break;
    case CountMode:
      Load_Num(5,set_datetime.second%10,0x00ff00);
      Load_Num(4,set_datetime.second/10,0x00ff00);
      Load_Num(3,set_datetime.minute%10,0x00ff00);
      Load_Num(2,set_datetime.minute/10,0x00ff00);
      Load_Num(1,set_datetime.hour%10,0x00ff00);
      Load_Num(0,set_datetime.hour/10,0x00ff00);
      Show_RGB();
      break;
    case DisplayMode:
      if(data.others[0]*60+data.others[1]<data.others[2]*60+data.others[3])
      {
        if(current_datetime.hour*60+current_datetime.minute>data.others[0]*60+data.others[1]&&
           current_datetime.hour*60+current_datetime.minute<data.others[2]*60+data.others[3])
        {
          if(display_time_or_date)
          {
            show_date();
            count++;
          }
          else
          {
            show_time();
            count++;
          }
          break;
          
        }
      }
      else
      {
        if(current_datetime.hour*60+current_datetime.minute>data.others[0]*60+data.others[1]||
           current_datetime.hour*60+current_datetime.minute<data.others[2]*60+data.others[3])
        {
          if(display_time_or_date)
          {
            show_date();
            count++;
          }
          else
          {
            show_time();
            count++;
          }
          break;

        }
      }
      Clear_RGB();
      Show_RGB();
    }
    (setting_select_glitter_count<0xffffff)?(setting_select_glitter_count++):(setting_select_glitter_count=0);
  }
}
/* USER CODE END 0 */

/**
* @brief  The application entry point.
* @retval int
*/
int main(void)
{
  /* USER CODE BEGIN 1 */
  __enable_irq();
  /* USER CODE END 1 */
  
  
  /* MCU Configuration--------------------------------------------------------*/
  
  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();
  
  /* USER CODE BEGIN Init */
  
  /* USER CODE END Init */
  
  /* Configure the system clock */
  SystemClock_Config();
  
  /* USER CODE BEGIN SysInit */
  
  /* USER CODE END SysInit */
  
  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_RTC_Init();
  MX_USB_DEVICE_Init();
  MX_DMA_Init();
  MX_TIM3_Init();
  MX_TIM4_Init();
  MX_TIM2_Init();
  /* USER CODE BEGIN 2 */
  __HAL_RCC_BKP_CLK_ENABLE();
  HAL_RTCEx_SetSecond_IT(&hrtc);
  //读取RGB设置，显示时间
  Get_Data();
  current_datetime=Get_DateTime();
  show_time();
  HAL_TIM_Base_Start_IT(&htim4);
  HAL_TIM_Base_Start_IT(&htim2);
  /* USER CODE END 2 */
  
  
  
  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
    Wait_For_Commander();
    /* USER CODE END WHILE */
    
    /* USER CODE BEGIN 3 */
  }
  /* USER CODE END 3 */
}

/**
* @brief System Clock Configuration
* @retval None
*/
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};
  RCC_PeriphCLKInitTypeDef PeriphClkInit = {0};
  
  /** Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE|RCC_OSCILLATORTYPE_LSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.HSEPredivValue = RCC_HSE_PREDIV_DIV1;
  RCC_OscInitStruct.LSEState = RCC_LSE_ON;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL9;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }
  /** Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
    |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;
  
  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
  {
    Error_Handler();
  }
  PeriphClkInit.PeriphClockSelection = RCC_PERIPHCLK_RTC|RCC_PERIPHCLK_USB;
  PeriphClkInit.RTCClockSelection = RCC_RTCCLKSOURCE_LSE;
  PeriphClkInit.UsbClockSelection = RCC_USBCLKSOURCE_PLL_DIV1_5;
  if (HAL_RCCEx_PeriphCLKConfig(&PeriphClkInit) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
* @brief RTC Initialization Function
* @param None
* @retval None
*/
static void MX_RTC_Init(void)
{
  
  /* USER CODE BEGIN RTC_Init 0 */
  
  /* USER CODE END RTC_Init 0 */
  
  RTC_TimeTypeDef sTime = {0};
  RTC_DateTypeDef DateToUpdate = {0};
  
  /* USER CODE BEGIN RTC_Init 1 */
  
  /* USER CODE END RTC_Init 1 */
  /** Initialize RTC Only 
  */
  hrtc.Instance = RTC;
  hrtc.Init.AsynchPrediv = RTC_AUTO_1_SECOND;
  hrtc.Init.OutPut = RTC_OUTPUTSOURCE_ALARM;
  if (HAL_RTC_Init(&hrtc) != HAL_OK)
  {
    Error_Handler();
  }
  
  /* USER CODE BEGIN Check_RTC_BKUP */
  if(HAL_RTCEx_BKUPRead(&hrtc,RTC_BKP_DR10)==0xabcdu)
  {
    return;
  }
  /* USER CODE END Check_RTC_BKUP */
  
  /** Initialize RTC and set the Time and Date 
  */
  sTime.Hours = 12;
  sTime.Minutes = 0;
  sTime.Seconds = 0;
  
  if (HAL_RTC_SetTime(&hrtc, &sTime, RTC_FORMAT_BIN) != HAL_OK)
  {
    Error_Handler();
  }
  DateToUpdate.WeekDay = RTC_WEEKDAY_TUESDAY;
  DateToUpdate.Month = RTC_MONTH_OCTOBER;
  DateToUpdate.Date = 1;
  DateToUpdate.Year = 19;
  
  if (HAL_RTC_SetDate(&hrtc, &DateToUpdate, RTC_FORMAT_BIN) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN RTC_Init 2 */
  HAL_RTCEx_BKUPWrite(&hrtc,RTC_BKP_DR10,0xabcdu);
  /* USER CODE END RTC_Init 2 */
  
}

/**
* @brief TIM2 Initialization Function
* @param None
* @retval None
*/
static void MX_TIM2_Init(void)
{
  
  /* USER CODE BEGIN TIM2_Init 0 */
  
  /* USER CODE END TIM2_Init 0 */
  
  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  
  /* USER CODE BEGIN TIM2_Init 1 */
  
  /* USER CODE END TIM2_Init 1 */
  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 7200-1;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 100-1;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM2_Init 2 */
  
  /* USER CODE END TIM2_Init 2 */
  
}

/**
* @brief TIM3 Initialization Function
* @param None
* @retval None
*/
static void MX_TIM3_Init(void)
{
  
  /* USER CODE BEGIN TIM3_Init 0 */
  
  /* USER CODE END TIM3_Init 0 */
  
  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_OC_InitTypeDef sConfigOC = {0};
  
  /* USER CODE BEGIN TIM3_Init 1 */
  
  /* USER CODE END TIM3_Init 1 */
  htim3.Instance = TIM3;
  htim3.Init.Prescaler = 0;
  htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim3.Init.Period = 80-1;
  htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim3.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim3) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim3, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_Init(&htim3) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 0;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
  if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM3_Init 2 */
  
  /* USER CODE END TIM3_Init 2 */
  HAL_TIM_MspPostInit(&htim3);
  
}

/**
* @brief TIM4 Initialization Function
* @param None
* @retval None
*/
static void MX_TIM4_Init(void)
{
  
  /* USER CODE BEGIN TIM4_Init 0 */
  
  /* USER CODE END TIM4_Init 0 */
  
  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  
  /* USER CODE BEGIN TIM4_Init 1 */
  
  /* USER CODE END TIM4_Init 1 */
  htim4.Instance = TIM4;
  htim4.Init.Prescaler = 72-1;
  htim4.Init.CounterMode = TIM_COUNTERMODE_DOWN;
  htim4.Init.Period = 1000-1;
  htim4.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim4.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim4) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim4, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim4, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM4_Init 2 */
  
  /* USER CODE END TIM4_Init 2 */
  
}

/** 
* Enable DMA controller clock
*/
static void MX_DMA_Init(void) 
{
  
  /* DMA controller clock enable */
  __HAL_RCC_DMA1_CLK_ENABLE();
  
  /* DMA interrupt init */
  /* DMA1_Channel6_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Channel6_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Channel6_IRQn);
  
}

/**
* @brief GPIO Initialization Function
* @param None
* @retval None
*/
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};
  
  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOD_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  
  /*Configure GPIO pins : Key_Previous_Pin Key_Menu_Pin Key_Next_Pin */
  GPIO_InitStruct.Pin = Key_Previous_Pin|Key_Menu_Pin|Key_Next_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_FALLING;
  GPIO_InitStruct.Pull = GPIO_PULLUP;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);
  
  /* EXTI interrupt init*/
  HAL_NVIC_SetPriority(EXTI3_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI3_IRQn);
  
  HAL_NVIC_SetPriority(EXTI4_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI4_IRQn);
  
  HAL_NVIC_SetPriority(EXTI9_5_IRQn, 3, 0);
  HAL_NVIC_EnableIRQ(EXTI9_5_IRQn);
  
}

/* USER CODE BEGIN 4 */

/* USER CODE END 4 */

/**
* @brief  This function is executed in case of error occurrence.
* @retval None
*/
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
* @brief  Reports the name of the source file and the source line number
*         where the assert_param error has occurred.
* @param  file: pointer to the source file name
* @param  line: assert_param error line source number
* @retval None
*/
void assert_failed(uint8_t *file, uint32_t line)
{ 
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
  tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
