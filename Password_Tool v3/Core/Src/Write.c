
#include "Keyboard.h"
#include "Communication.h"
#include "PasswordManager.h"
#include "Write.h"

//Basic keyboard struct for typing with usb reports
//elements 0: modifier, 1: reserved, 2-8: keycodes
subKeyBoard keyBoardHIDsub = {0,0,0,0,0,0,0,0};


/**
  * @brief Type via USB report functions.
  *
  * @param key: keycode of a key.
  *
  * @param modifier: Modifier for a key (e.g.: shift, alt, ...). You can find these in the Write.h file
  *
  */
void write (uint8_t key, uint8_t modifier)
{

	keyBoardHIDsub.KEYCODE1=key;
	keyBoardHIDsub.MODIFIER=modifier;
	//feture press the key down
	USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,(uint8_t*)&keyBoardHIDsub,sizeof(keyBoardHIDsub));
	HAL_Delay(15);
	keyBoardHIDsub.MODIFIER=0x00;
	keyBoardHIDsub.KEYCODE1=0x00;
	//feture release the key
	USBD_CUSTOM_HID_SendReport(&hUsbDeviceFS,(uint8_t*)&keyBoardHIDsub,sizeof(keyBoardHIDsub));

}


/**
  * @brief Types an Alt+Tab via USB report functions.
  *
  *
  */
void writeAltTab(){
	write(43,MOD_ALT_LEFT);
}
