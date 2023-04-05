#ifndef __KBW_H
#define __KBW_H


#include "usb_device.h"
#include "usbd_customhid.h"


extern USBD_HandleTypeDef hUsbDeviceFS;

typedef struct
	{
		uint8_t MODIFIER;
		uint8_t RESERVED;
		uint8_t KEYCODE1;
		uint8_t KEYCODE2;
		uint8_t KEYCODE3;
		uint8_t KEYCODE4;
		uint8_t KEYCODE5;
		uint8_t KEYCODE6;
	}subKeyBoard;

void write (uint8_t key, uint8_t modifier);


#endif
