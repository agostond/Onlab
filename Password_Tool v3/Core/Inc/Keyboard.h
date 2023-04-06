#ifndef __KB_H
#define __KB_H

#include "usb_device.h"
#include "usbd_customhid.h"

#define NumberOfKeys 158

#define MOD_ALT_RIGHT       (1<<6)
#define MOD_SHIFT_LEFT      (1<<1)
#define MOD_NO_MODIFIER		0

	typedef struct
		{
			char ASCII;
			uint8_t usage_id;
			uint8_t modifier;
		}HunKeys;


void KeyBoardPrint(char *data, uint16_t length);

void InitKeyboard(void (*WritePointerParam)(uint8_t, uint8_t));


#endif
