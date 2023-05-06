
#ifndef INC_USER_H_
#define INC_USER_H_

#include "flash_handler.h"
#include "crypto.h"
#include "Communication.h"

void UserInit();
int CreateUser(char* password, size_t size);
/*int Authenticate(char* password, size_t size);*/
int Authenticate(char* response, char* salt);
int UserExists();
void LoginLoop();

#endif /* INC_USER_H_ */
