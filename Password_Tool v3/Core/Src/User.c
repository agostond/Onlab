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

/*int Authenticate(char* password, size_t size)
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
}*/

/*
 * response: a kliens által visszaküldött válasz (20 bájtnyi érték)
 * salt: a random, amit generálunk, mindig 12 bájt
 */
int Authenticate(char* response, char* salt)
{
	// jelszo hash beolvasasa flash-bol
		// a hash kimenete a hasznalt sha1 eseten minden bemenetre 20byte
		// tehat 20 byte adatot olvasunk be
		char hashFromFlash[20];
		memcpy(hashFromFlash, (uint32_t*)FLASH_USER_DATA_ADDRESS, sizeof(hashFromFlash));

	// a salt mindig 12byte-nyi lesz
		char HMAC[32];
		int i = 0;
		for(i = 0; i < sizeof(hashFromFlash); i++)
		{
			HMAC[i] = hashFromFlash[i];
		}
		for(int j = 0; j < 12; j++, i++)
		{
			HMAC[i] = salt[j];
		}
		char HMAChashed[20];
		SHA1(HMAChashed, HMAC, sizeof(HMAC));


	// annak eldontese, hogy a ket hash egyezik-e
		for(i = 0; i < 20; i++)
		{
			if(response[i] != HMAChashed[i])
			{
				return 0;
			}
		}

	// visszateres, ha egyezik
		return 1;
}

int CreateUser(char* password, size_t size)
{
/*	RNG_Init();
	RNG_Function RandomGenerator = RNG_Get();*/

	char passwordHash[20];
	SHA1(passwordHash, password, size);

	// create nonce
	uint8_t nonce[12] = {0};
	uint8_t key[32] = {0};
	GenerateNonce(nonce, sizeof(nonce));
	GenerateKey(key, sizeof(key));

	//RandomGenerator(nonce, sizeof(nonce));
	//RandomGenerator(key, sizeof(key));

	// concatenate H(pass) and nonce to get a 32 byte value
	uint8_t result[32] = {0};
	int i = 0;
	for(i = 0; i < sizeof(passwordHash); i++)
	{
		result[i] = passwordHash[i];
	}
	for(int j = 0; j < sizeof(nonce); j++, i++)
	{
		result[i] = nonce[j];
	}
	// encrypt the random (key)
	EncryptDecrypt(result, nonce, key, sizeof(key));
	// save the encrypted random (after the user credentials)
	if(WriteDataToFlash((uint8_t*)result, sizeof(result), FLASH_USER_DATA_ADDRESS) != F_SUCCESS)
	{
		return F_WRITING_ERROR;
	}
	return WriteDataToFlash((uint8_t*)key, sizeof(key), FLASH_USER_DATA_ADDRESS + sizeof(result));

}

//return 1 if logged in, otherwise 0

void LoginLoop(){

	uint8_t masterPassword[USB_REPORT_SIZE] = {0};

	//uint8_t salt[12] = {0x4a, 0x2f, 0x7e, 0x9d, 0x01, 0x8b, 0x3c, 0x6a, 0xaf, 0x5e, 0x71, 0x0b};
	uint8_t salt[12] = {0};

	//RNG_Init();
	//RNG_Function RandomGenerator = RNG_Get();
	GenerateRandom_ADC(salt, sizeof(salt));
	//RandomGenerator(salt, sizeof(salt));
	//while(flag_rx == 0);
	SendRandomLoop(salt, sizeof(salt), AUTHENTICATE);


	while(1)
	{
		if(UserExists())
		{


			if(1 == AuthenticateFromFeature((char*)masterPassword, AUTHENTICATE))
			{

				if(Authenticate((char*)masterPassword, (char*)salt))
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

				if(CreateUser((char*)masterPassword, strlen(masterPassword)) == F_SUCCESS)
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
