#include "crypto.h"
#include "chacha.h"

#include "stdlib.h"


	extern ADC_HandleTypeDef hadc1;

	struct chacha20_context chacha;


	int pbCounter = 0;

	void EncryptDecrypt(uint8_t *key, uint8_t* nonce, uint8_t* bytes, size_t n_bytes)
	{
		chacha20_init_context(&chacha, key, nonce, 0);
		chacha20_xor(&chacha, bytes, n_bytes);
	}

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


	void GenerateKey(uint8_t* key, size_t n_bytes)
	{
		GenerateRandom_ADC(key, n_bytes);
	}

	void GenerateNonce(uint8_t* nonce, size_t n_bytes)
	{
		GenerateRandom_ADC(nonce, n_bytes);
	}
