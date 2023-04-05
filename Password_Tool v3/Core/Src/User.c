#include"User.h"
#include "sha1.h"
#include "main.h"
#include "Communication.h"
#include "Keyboard.h"
#include "PasswordManager.h"
#include "flash_handler.h"

extern uint8_t flag_rx;

extern uint8_t report_buffer[USB_REPORT_SIZE];

int UserExists()
{
	uint32_t wordFromFlash;
	memcpy(&wordFromFlash, (uint32_t*)FLASH_USER_DATA_ADDRESS, sizeof(wordFromFlash));
	if(wordFromFlash == EMPTY_FLASH_WORD)
	{
		return 0;
	}
	else
	{
		return 1;
	}
}

int Authenticate(char* password, size_t size)
{
	// jelszo hash beolvasasa flash-bol
		// a hash kimenete a hasznalt sha1 eseten minden bemenetre 20byte
		// tehat 20 byte adatot olvasunk be
		char hashFromFlash[20];
		memcpy(hashFromFlash, (uint32_t*)FLASH_USER_DATA_ADDRESS, sizeof(hashFromFlash));

	// megadott jelszo hashelese
		char passwordHash[20];
		SHA1(passwordHash, password, size);


	// annak eldontese, hogy a ket hash egyezik-e
		for(int i = 0; i < 20; i++)
		{
			if(hashFromFlash[i] != passwordHash[i])
			{
				return 0;
			}
		}

	// visszateres, ha egyezik
		return 1;
}

int CreateUser(char* password, size_t size)
{
	char passwordHash[20];
	SHA1(passwordHash, password, size);
	return WriteDataToFlash((uint8_t*)passwordHash, size, FLASH_USER_DATA_ADDRESS);
}

//return 1 if logged in, otherwise 0

void LoginLoop(){

	uint8_t masterPassword[USB_REPORT_SIZE] = {0};

	while(1)
	{
		if(UserExists())
		{
			if(1 == AuthenticateFromFeature((char*)masterPassword, AUTHENTICATE))
			{
				if(Authenticate((char*)masterPassword, sizeof(masterPassword)) == 1)
				{
					SendStatus(SUCCES, report_buffer[CHECK_SUM_PLACE]);
					flag_rx = 0;
					return;
				}
				else
				{
					flag_rx = 0;
					while(flag_rx == 0);
					SendStatus(FAIL, report_buffer[CHECK_SUM_PLACE]);
				}

			}

		}
		else
		{
			if(1 == AuthenticateFromFeature((char*)masterPassword, CREATE))
			{

				if(CreateUser((char*)masterPassword, sizeof(masterPassword)) == F_SUCCESS)
				{
					SendStatus(SUCCES, report_buffer[CHECK_SUM_PLACE]);
					flag_rx = 0;
					return;
				}
				else
				{
					flag_rx = 0;
					while(flag_rx == 0);
					SendStatus(FAIL, report_buffer[CHECK_SUM_PLACE]);
				}
			}

		}

	}
}

/*void UserInit()
{
	// Megnézzük, hogy létezik-e user
	// ha létezik, akkor kiküldjük a challenge-t
	// ha nem létezik, akkor felkészülünk a user felvételére

	char password[32];
	if(UserExists() == 0)
	{

	}
	else
	{

	}


}*/
