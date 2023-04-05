
#include "Keyboard.h"
#include "Communication.h"
#include "PasswordManager.h"
#include "Write.h"

//test

void (*WritePointer)(uint8_t, uint8_t) = 0;

HunKeys keys[NumberOfKeys] = {
	{'0',53, MOD_NO_MODIFIER},
	{'1',30, MOD_NO_MODIFIER},
	{'2',31, MOD_NO_MODIFIER},
	{'3',32, MOD_NO_MODIFIER},
	{'4',33, MOD_NO_MODIFIER},
	{'5',34, MOD_NO_MODIFIER},
	{'6',35, MOD_NO_MODIFIER},
	{'7',36, MOD_NO_MODIFIER},
	{'8',37, MOD_NO_MODIFIER},
	{'9',38, MOD_NO_MODIFIER},
	{'ö',39, MOD_NO_MODIFIER},
	{'ü',45, MOD_NO_MODIFIER},
	{'ó',46, MOD_NO_MODIFIER},
	{'§',53, MOD_SHIFT_LEFT},
	{'\'',30, MOD_SHIFT_LEFT},
	{'"',31, MOD_SHIFT_LEFT},
	{'+',32, MOD_SHIFT_LEFT},
	{'!',33, MOD_SHIFT_LEFT},
	{'%',34, MOD_SHIFT_LEFT},
	{'/',35, MOD_SHIFT_LEFT},
	{'=',36, MOD_SHIFT_LEFT},
	{'(',37, MOD_SHIFT_LEFT},
	{')',38, MOD_SHIFT_LEFT},
	{'Ö',39, MOD_SHIFT_LEFT},
	{'Ü',45, MOD_SHIFT_LEFT},
	{'Ó',46, MOD_SHIFT_LEFT},
	{'~',30, MOD_ALT_RIGHT},
	{'ˇ',31, MOD_ALT_RIGHT},
	{'ˇ',44, MOD_NO_MODIFIER},
	{'^',32, MOD_ALT_RIGHT},
	{'^',44, MOD_NO_MODIFIER},
	{'˘',33, MOD_ALT_RIGHT},
	{'˘',44, MOD_NO_MODIFIER},
	{'°',34, MOD_ALT_RIGHT},
	{'°',44, MOD_NO_MODIFIER},
	{'˛',35, MOD_ALT_RIGHT},
	{'˛',44, MOD_NO_MODIFIER},
	{'`',36, MOD_ALT_RIGHT},
	{'˙',37, MOD_ALT_RIGHT},
	{'˙',44, MOD_NO_MODIFIER},
	{'´',38, MOD_ALT_RIGHT},
	{'´',44, MOD_NO_MODIFIER},
	{'˝',39, MOD_ALT_RIGHT},
	{'˝',44, MOD_NO_MODIFIER},
	{'¨',45, MOD_ALT_RIGHT},
	{'¨',44, MOD_NO_MODIFIER},
	{'¸',46, MOD_ALT_RIGHT},
	{'¸',44, MOD_NO_MODIFIER},
	{'\t',43, MOD_NO_MODIFIER},
	{'\n',40, MOD_NO_MODIFIER},
	{' ',44, MOD_NO_MODIFIER},
	{'a',4, MOD_NO_MODIFIER},
	{'s',22, MOD_NO_MODIFIER},
	{'d',7, MOD_NO_MODIFIER},
	{'f',9, MOD_NO_MODIFIER},
	{'g',10, MOD_NO_MODIFIER},
	{'h',11, MOD_NO_MODIFIER},
	{'j',13, MOD_NO_MODIFIER},
	{'k',14, MOD_NO_MODIFIER},
	{'l',15, MOD_NO_MODIFIER},
	{'é',51, MOD_NO_MODIFIER},
	{'á',52, MOD_NO_MODIFIER},
	{'A',4, MOD_SHIFT_LEFT},
	{'S',22, MOD_SHIFT_LEFT},
	{'D',7, MOD_SHIFT_LEFT},
	{'F',9, MOD_SHIFT_LEFT},
	{'G',10, MOD_SHIFT_LEFT},
	{'H',11, MOD_SHIFT_LEFT},
	{'J',13, MOD_SHIFT_LEFT},
	{'K',14, MOD_SHIFT_LEFT},
	{'L',15, MOD_SHIFT_LEFT},
	{'É',51, MOD_SHIFT_LEFT},
	{'Á',52, MOD_SHIFT_LEFT},
	{'ä',4, MOD_ALT_RIGHT},
	{'đ',22, MOD_ALT_RIGHT},
	{'Đ',7, MOD_ALT_RIGHT},
	{'[',9, MOD_ALT_RIGHT},
	{']',10, MOD_ALT_RIGHT},
	{'ł',14, MOD_ALT_RIGHT},
	{'Ł',15, MOD_ALT_RIGHT},
	{'$',51, MOD_ALT_RIGHT},
	{'ß',52, MOD_ALT_RIGHT},
	{'q',20, MOD_NO_MODIFIER},
	{'w',26, MOD_NO_MODIFIER},
	{'e',8, MOD_NO_MODIFIER},
	{'r',21, MOD_NO_MODIFIER},
	{'t',23, MOD_NO_MODIFIER},
	{'z',28, MOD_NO_MODIFIER},
	{'u',24, MOD_NO_MODIFIER},
	{'i',12, MOD_NO_MODIFIER},
	{'o',18, MOD_NO_MODIFIER},
	{'p',19, MOD_NO_MODIFIER},
	{'ő',47, MOD_NO_MODIFIER},
	{'ú',48, MOD_NO_MODIFIER},
	{'ű',49, MOD_NO_MODIFIER},
	{'Q',20, MOD_SHIFT_LEFT},
	{'W',26, MOD_SHIFT_LEFT},
	{'E',8, MOD_SHIFT_LEFT},
	{'R',21, MOD_SHIFT_LEFT},
	{'T',23, MOD_SHIFT_LEFT},
	{'Z',28, MOD_SHIFT_LEFT},
	{'U',24, MOD_SHIFT_LEFT},
	{'I',12, MOD_SHIFT_LEFT},
	{'O',18, MOD_SHIFT_LEFT},
	{'P',19, MOD_SHIFT_LEFT},
	{'Ő',47, MOD_SHIFT_LEFT},
	{'Ú',48, MOD_SHIFT_LEFT},
	{'Ű',49, MOD_SHIFT_LEFT},
	{'\\',20, MOD_ALT_RIGHT},
	{'|',26, MOD_ALT_RIGHT},
	{'Ä',8, MOD_ALT_RIGHT},
	{'€',24, MOD_ALT_RIGHT},
	{'÷',47, MOD_ALT_RIGHT},
	{'×',48, MOD_ALT_RIGHT},
	{'¤',49, MOD_ALT_RIGHT},
	{'y',29, MOD_NO_MODIFIER},
	{'x',27, MOD_NO_MODIFIER},
	{'c',6, MOD_NO_MODIFIER},
	{'v',25, MOD_NO_MODIFIER},
	{'b',5, MOD_NO_MODIFIER},
	{'n',17, MOD_NO_MODIFIER},
	{'m',16, MOD_NO_MODIFIER},
	{',',54, MOD_NO_MODIFIER},
	{'.',55, MOD_NO_MODIFIER},
	{'-',56, MOD_NO_MODIFIER},
	{'Y',29, MOD_SHIFT_LEFT},
	{'X',27, MOD_SHIFT_LEFT},
	{'C',6, MOD_SHIFT_LEFT},
	{'V',25, MOD_SHIFT_LEFT},
	{'B',5, MOD_SHIFT_LEFT},
	{'N',17, MOD_SHIFT_LEFT},
	{'M',16, MOD_SHIFT_LEFT},
	{'?',54, MOD_SHIFT_LEFT},
	{':',55, MOD_SHIFT_LEFT},
	{'_',56, MOD_SHIFT_LEFT},
	{'>',29, MOD_ALT_RIGHT},
	{'#',27, MOD_ALT_RIGHT},
	{'&',6, MOD_ALT_RIGHT},
	{'@',25, MOD_ALT_RIGHT},
	{'{',5, MOD_ALT_RIGHT},
	{'}',17, MOD_ALT_RIGHT},
	{'<',16, MOD_ALT_RIGHT},
	{';',54, MOD_ALT_RIGHT},
	{'>',55, MOD_ALT_RIGHT},
	{'*',56, MOD_ALT_RIGHT},
	{'í',100, MOD_NO_MODIFIER},
	{'Í',100, MOD_SHIFT_LEFT},
	{'<',100, MOD_ALT_RIGHT},
	{'\0',0, MOD_NO_MODIFIER}, //END OF STRUCT
};


void InitKeyboard(){
 WritePointer = write;
}

void KeyBoardPrint(char *data, uint16_t length){
	uint8_t i = 0;
	uint8_t fail = 1;
	while(data[i] != '\0' && i < length){
		for(uint8_t k = 0; k < NumberOfKeys; k++){

			if(data[i] == keys[k].ASCII){
					WritePointer(keys[k].usage_id, keys[k].modifier);
				if(data[i] == keys[k+1].ASCII){
					WritePointer(keys[k+1].usage_id, keys[k+1].modifier);
				}
				fail = 0;
				HAL_Delay(5);
				break;
			}


		}
		if(fail == 1){
			failCom();
			return;
		}

		i++;
	}

}

/*
void KeyBoardPrint(char *data,uint16_t length){

	for(uint16_t count=0;count<length;count++)
	{
		if(data[count] == '\0'){
			return;
		}
		//A-Z Y-Z inverted
		if(data[count]>=0x41 && data[count]<=0x5A)
		{
			keyBoardHIDsub.MODIFIER=0x02;
			if(data[count] == 0x5A){
				keyBoardHIDsub.KEYCODE1 = 0x1C;
			}
			else if(data[count]==0x59){
				keyBoardHIDsub.KEYCODE1 =  0x1D;
			}
			else{
				keyBoardHIDsub.KEYCODE1=data[count]-0x3D;
			}
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
			HAL_Delay(15);
			keyBoardHIDsub.MODIFIER=0x00;
			keyBoardHIDsub.KEYCODE1=0x00;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
		}
		//a-z y-z inverted
		else if(data[count]>=0x61 && data[count]<=0x7A)
		{
			if(data[count] == 0x7A){
				keyBoardHIDsub.KEYCODE1 = 0x1C;
			}
			else if(data[count] == 0x79){
				keyBoardHIDsub.KEYCODE1 = 0x1D;
			}
			else{
				keyBoardHIDsub.KEYCODE1=data[count]-0x5D;
			}
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
			HAL_Delay(15);
			keyBoardHIDsub.KEYCODE1=0x00;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
		}
		//space
		else if(data[count]==0x20)
		{
			keyBoardHIDsub.KEYCODE1=0x2C;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
			HAL_Delay(15);
			keyBoardHIDsub.KEYCODE1=0x00;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
		}
		//line feed
		else if(data[count]=='\n')
		{
			keyBoardHIDsub.KEYCODE1=0x28;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
			HAL_Delay(15);
			keyBoardHIDsub.KEYCODE1=0x00;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
		}
		//horizontal tab
		else if(data[count]==0x09)
		{
			keyBoardHIDsub.KEYCODE1=0x2B;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
			HAL_Delay(15);
			keyBoardHIDsub.KEYCODE1=0x00;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
		}
		//0-9
		if(data[count]>=0x30 && data[count]<=0x39)
		{
			keyBoardHIDsub.KEYCODE1=data[count]-19;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
			HAL_Delay(15);
			keyBoardHIDsub.KEYCODE1=0x00;
			USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
		}
		HAL_Delay(25);
	}
}

*/

