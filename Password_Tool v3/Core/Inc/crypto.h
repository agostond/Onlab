

#ifndef INC_CRYPTO_H_
#define INC_CRYPTO_H_

#include "stm32f1xx_hal.h"
#include "chacha.h"
#include "sha1.h"


void InitContext();

void EncryptDecrypt(uint8_t *key, uint8_t* nonce, uint8_t* bytes, size_t n_bytes);
void GenerateRandom_ADC(uint8_t* randomBytes, size_t size);
void GenerateKey(uint8_t* key, size_t n_bytes);
void GenerateNonce(uint8_t* nonce, size_t n_bytes);




#endif /* INC_CRYPTO_H_ */
