#include"User.h"
#include "sha1.h"
#include "main.h"
#include "Communication.h"
#include "Keyboard.h"
#include "PasswordManager.h"
#include "flash_handler.h"


extern uint8_t flag_rx; 						  //Flag which sets if a feature report arrives. Manual reset needed!

extern uint8_t authenticated; 				     // Variable for user status

extern uint8_t report_buffer[USB_REPORT_SIZE];  // Array which stores arrived feature reports.

/**
  * @brief Determine that user exists or not
  *
  *
  * @retval true (1) if user exists, false(0) otherwise
  */
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




/**
* @brief Authenticates the user with Challenge Response
*
* @param response: the response from the client
*
* @param challenge: the challenge to solve (12 random bytes)
*
* @retval true(1) if success, false(0) otherwise
*/

int Authenticate(char* response, char* challenge)
{
	// read the master hash from flash
		char hashFromFlash[20];
		memcpy(hashFromFlash, (uint32_t*)FLASH_USER_DATA_ADDRESS, sizeof(hashFromFlash));

		char HMAC[32];
		int i = 0;
		for(i = 0; i < sizeof(hashFromFlash); i++)
		{
			HMAC[i] = hashFromFlash[i];
		}
		for(int j = 0; j < 12; j++, i++)
		{
			HMAC[i] = challenge[j];
		}
		char HMAChashed[20];
		SHA1(HMAChashed, HMAC, sizeof(HMAC));


	// check the response of the client
		for(i = 0; i < 20; i++)
		{
			if(response[i] != HMAChashed[i])
			{
				return 0;
			}
		}

	// return value if auth. was successful
		return 1;
}

/**
* @brief Create a master user on the device
*
* @param password: the master password from client
*
* @param size: the size of the password
*
* @retval the flash operation status
*/
int CreateUser(char* password, size_t size)
{


	char passwordHash[20];
	SHA1(passwordHash, password, size);

	// create nonce
	uint8_t nonce[12] = {0};
	uint8_t key[32] = {0};
	GenerateNonce(nonce, sizeof(nonce));
	GenerateKey(key, sizeof(key));

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


/**
  * @brief In first launch or after logout / power reset this function grants acces to use the device.
  *
  * @note The device will stay in this function until a new user creation or succes login happening.
  *
  */
void LoginLoop()
{

	uint8_t masterPassword[USB_REPORT_SIZE] = {0};

	uint8_t salt[12];
	GenerateRandom_ADC(salt, 12);
	SendRandomLoop(salt, sizeof(salt), AUTHENTICATE);


	while(1)
	{
		if(UserExists())
		{
			if(1 == AuthenticateFromFeature((char*)masterPassword, AUTHENTICATE, salt))
			{

				if(Authenticate((char*)masterPassword, (char*)salt))
				{
					SendStatus(SUCCES, report_buffer[CHECK_SUM_PLACE]);
					flag_rx = 0;
					authenticated = 1;
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
			if(1 == AuthenticateFromFeature((char*)masterPassword, CREATE, salt))
			{

				if(CreateUser((char*)masterPassword, strlen((char*)masterPassword)) == F_SUCCESS)
				{
					SendStatus(SUCCES, report_buffer[CHECK_SUM_PLACE]);
					flag_rx = 0;
					authenticated = 1;
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


