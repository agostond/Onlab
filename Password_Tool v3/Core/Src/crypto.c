#include "crypto.h"
#include "chacha.h"

#include "stdlib.h"


/**
* @brief ADC for (pseudo) RNG
*
*/
extern ADC_HandleTypeDef hadc1;

/**
 * @brief The chacha structure for encrypting/decrypting
*/
struct chacha20_context chacha;

/**
* @brief This function is used for ChaCha encryption/decryption
*
* @param key: the key value for the ChaCha
*
* @param nonce: the nonce used in ChaCha
*
* @param bytes: the byte stream for the encryption/decryption operation
*
* @param n_bytes: the number of bytes in the bytes array
*/
void EncryptDecrypt(uint8_t *key, uint8_t* nonce, uint8_t* bytes, size_t n_bytes)
{
	chacha20_init_context(&chacha, key, nonce, 0);
	chacha20_xor(&chacha, bytes, n_bytes);
}

/**
* @brief	This function is used for RNG, because the STM32f103C8T6 doesn't have any TRNG.
* 			We use the built-in temperature sensor for entropy source.
*
* @param randomBytes: the buffer where the function saves the random values
*
* @param size: the size of the buffer
*
*/
void GenerateRandom_ADC(uint8_t* randomBytes, unsigned size)
{
	HAL_ADC_PollForConversion(&hadc1, HAL_MAX_DELAY);
	uint8_t temp = 0;
	uint8_t value = 0;

	for(int i = 0; i < size; i++)
	{
		temp = HAL_ADC_GetValue(&hadc1);
		value = temp<<4;
		temp = HAL_ADC_GetValue(&hadc1);
		value |= (temp)&(0x0F);
		randomBytes[i] = value;
	}

}

/**
* @brief This function is used as the key generator of ChaCha
*
* @param key: the buffer where the function saves the generated random bytes used as key
* @param n_bytes: the number of bytes in the key array
*/
void GenerateKey(uint8_t* key, size_t n_bytes)
{
	GenerateRandom_ADC(key, n_bytes);
}

/**
* @brief This function is used as the initial value generator of ChaCha
*
* @param nonce: the buffer where the function saves the generated random bytes used as initial value
* @param n_bytes: the number of bytes in the nonce array
*/
void GenerateNonce(uint8_t* nonce, size_t n_bytes)
{
	GenerateRandom_ADC(nonce, n_bytes);
}
