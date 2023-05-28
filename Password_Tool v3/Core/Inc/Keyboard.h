#ifndef __KB_H
#define __KB_H

#include "usb_device.h"
#include "usbd_customhid.h"

//defines how many characters can be writen
#define NumberOfKeys 124

/***********************
 * KEYBOARD MODIFERS
 ***********************/
#define MOD_ALT_RIGHT       (1<<6)
#define MOD_SHIFT_LEFT      (1<<1)
#define MOD_NO_MODIFIER		0


	//struct to store characters and keyboard codes
	typedef struct
		{
			uint8_t ASCII;
			uint8_t usage_id;
			uint8_t modifier;
		}HunKeys;


void KeyBoardPrint(char *data, uint16_t length);

void InitKeyboard(void (*WritePointerParam)(uint8_t, uint8_t));


#endif
