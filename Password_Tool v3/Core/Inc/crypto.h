

#ifndef INC_CRYPTO_H_
#define INC_CRYPTO_H_


#include "chacha.h"



void InitContext();

void Encrypt(uint8_t* bytes, size_t n_bytes);

void Decrypt(uint8_t* bytes, size_t n_bytes);




#endif /* INC_CRYPTO_H_ */
