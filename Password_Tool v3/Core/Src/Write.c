
#include "Keyboard.h"
#include "Communication.h"
#include "PasswordManager.h"
#include "Write.h"

subKeyBoard keyBoardHIDsub = {0,0,0,0,0,0,0,0};

void write (uint8_t key, uint8_t modifier){

	keyBoardHIDsub.KEYCODE1=key;
	keyBoardHIDsub.MODIFIER=modifier;
	USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));
	HAL_Delay(15);
	keyBoardHIDsub.MODIFIER=0x00;
	keyBoardHIDsub.KEYCODE1=0x00;
	USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,&keyBoardHIDsub,sizeof(keyBoardHIDsub));

}
